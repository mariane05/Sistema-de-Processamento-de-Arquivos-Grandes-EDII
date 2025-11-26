---

## Etapa 3
**Design da Estrutura Indexada**

Para permitir busca direta em arquivos compactados sem descompactação total, adotamos um formato **indexado em blocos**. O arquivo compactado é estruturado em três partes:

1. **Cabeçalho global**
   - `tamanhoOriginalTotal` (`long`): número total de caracteres do arquivo original.
   - `numeroBlocos` (`int`): quantidade de blocos em que o arquivo foi particionado.

2. **Índice de blocos** (lista de `EntradaIndiceBlocos`)  
   Para cada bloco, armazenamos:
   - `OffsetOriginal` (`long`): posição inicial do bloco no arquivo original.
   - `TamanhoOriginal` (`int`): tamanho do bloco descompactado.
   - `OffsetComprimido` (`long`): posição, no arquivo compactado, onde começam os bytes daquele bloco.
   - `TamanhoComprimido` (`int`): quantos bytes comprimidos pertencem ao bloco.

3. **Dados dos blocos comprimidos**  
   Cada bloco é armazenado com um cabeçalho próprio de Huffman (frequências e símbolos) seguido do stream de bits compactados.

Essa estrutura permite que a busca carregue em memória apenas:
- o **índice de blocos** (tamanho pequeno, proporcional ao número de blocos), e
- o **bloco comprimido atual** (lido por `Seek` direto em `OffsetComprimido`).

A navegação pelo arquivo é feita com *seeks* constantes por bloco, garantindo que o custo de acessar qualquer região do arquivo original seja **O(1)** em termos de saltos de disco.

---

**Funcionalidade da Busca no Arquivo Compactado**

A busca em arquivo compactado é implementada em `BuscarArquivoComprimidoApp.BuscarNoArquivoComprimido` e segue o pipeline abaixo:

1. **Leitura do índice**
   - O programa abre o arquivo compactado com `FileStream`/`BinaryReader`.
   - Invoca `LerCabecalhoEIndice` para recuperar:
     - o `tamanhoOriginalTotal`, e  
     - a lista de `EntradaIndiceBlocos` com os metadados de cada bloco.

2. **Processamento bloco a bloco**  
   Para cada entrada de índice:
   - O programa faz `Seek` até `OffsetComprimido`.
   - Lê exatamente `TamanhoComprimido` bytes daquele bloco.
   - Chama `DescomprimirBlocoHuffman`:
     - Reconstrói o dicionário de frequências (usando o mesmo formato de cabeçalho definido na etapa de compressão).
     - Reconstrói a árvore de Huffman.
     - Percorre os bits até decodificar todos os caracteres do bloco (`totalCaracteres = soma das frequências`).
     - Retorna o `textoBloco` descompactado.

3. **Janela com sufixo para atravessar fronteiras de bloco**
   - Mantemos um `sufixoAnterior` com até `tamanhoPadrao - 1` caracteres do bloco anterior.
   - Para o bloco atual, montamos:
     ```csharp
     janela = sufixoAnteriorFinal + textoBlocoAtual
     ```
   - Rodamos o **mesmo algoritmo de busca da Etapa 2** (`BuscaArquivoGrandeApp.Buscar`, baseado em Boyer–Moore/Horspool) sobre essa `janela`.

4. **Mapeamento das posições locais para offsets globais**
   - Cada posição local `posLocal` encontrada em `janela` é convertida para posição global no arquivo original a partir de:
     ```csharp
     posGlobal = (bloco.OffsetOriginal - tamanhoSufixo) + posLocal
     ```
   - Dessa forma, a saída de `buscar_compactado` é uma lista de offsets relativos ao arquivo original, compatível com a especificação.

O reuso do algoritmo de busca da Etapa 2 garante consistência de comportamento entre `buscar_simples` (arquivo não compactado) e `buscar_compactado` (arquivo indexado em blocos).

---

**Estratégias de Memória e Evolução em Relação à Etapa 2**

A principal preocupação de projeto foi adequar a busca ao cenário de **arquivos maiores do que a RAM disponível**. Houve uma evolução clara da Etapa 2 para a Etapa 3.

**Etapa 2 (conceito base da busca)**

Na Etapa 2, o foco foi implementar um algoritmo de busca eficiente (variação de Boyer–Moore/Horspool). A lógica de comparação é baseada em:

- Pré-processamento da substring para gerar uma tabela de deslocamentos.
- Comparações de trás para frente no texto.
- Saltos no índice `i` do texto com base na tabela.

Essa implementação foi pensada inicialmente sobre um **texto em memória** (string completa), o que é simples para validar a corretude do algoritmo, mas não resolve o problema de arquivos gigantes.

**Etapa 3 (adaptação para arquivos grandes e compactados)**

Na Etapa 3, preservamos o algoritmo de busca, mas mudamos a **forma de alimentar** esse algoritmo com dados:

1. **Leitura em blocos (*streaming*)**  
   - Em vez de trabalhar com o arquivo inteiro, o sistema só descompacta **um bloco por vez**.
   - O consumo de memória passa a ser dominado por:
     - o tamanho do bloco descompactado, e
     - um pequeno sufixo (`tamanhoPadrao - 1`) usado para compor a janela.

2. **Janela com *overlap* mínimo**  
   - A sobreposição entre blocos é limitada a `tamanhoPadrao - 1` caracteres.
   - Isso é o suficiente para capturar qualquer ocorrência de padrão que “cruza” a fronteira entre dois blocos, sem replicar desnecessariamente o conteúdo antigo em memória.

3. **Índice compacto em memória**  
   - O índice de blocos é uma estrutura de tamanho reduzido (quatro campos por bloco).
   - Mesmo para arquivos grandes, o custo de memória do índice é baixo, pois o número de blocos tende a ser muito menor que o número de caracteres.

4. **Separação entre “caminho de produção” e “caminho de debug”**  
   - A função `Debug_01` reconstrói o arquivo completo apenas para validação/depuração.
   - A busca real (`BuscarNoArquivoComprimido`) nunca guarda o arquivo inteiro – apenas o bloco atual e o sufixo.
   - Isso mantém a implementação adequada ao cenário de dados maiores que a RAM.