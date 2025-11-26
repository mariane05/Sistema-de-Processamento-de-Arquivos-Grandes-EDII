## Projeto
### Linguagem
A linguagem escolhida para o desenvolvimento do projeto foi o C#. A discussão inicial da equipe foi usar python, principalmente pela facilidade e velocidade de desenvolvimento. Mas dado os requisitos técnicos do trabalho, de performance e gerenciamento eficaz de memória, junto de uma rápida pesquisa, o grupo viu que essa não era a linguagem ideal para isso. 

Ainda se mantendo dentro dos parâmetros de facilidade e velocidade de desenvolvimento, ficamos entre C++ e C#. Apesar de C++ ter um maior despenho, a equipe viu que a sintaxe da linguagem, junto do gerenciamento de memória mais manual, não seria o melhor em tempos de "facilidade e velocidade". 

Desta forma, foi escolhido C#, por conta da sua sintaxe muito semelhante ao Java, linguagem amplamente abordada na faculdade e por conta do C# ter um Garbage Collector (GC) próprio, mas que não era tão rígido quanto o Java, permitindo assim que pudéssemos fazer alterações para melhoria e desempenho onde fosse necessário. 

### Estrutura de pastas/arquitetura de projeto
A ideia principal foi separar cada etapa (que foi dividida para cada um dos membros da equipe) em módulos, para evitar o máximo possível de conflito de código ou qualquer outro problema possível disso.

Inicialmente foram criados 3 módulos separados, cada um sendo um projeto de console diferente. Mas diante de uma revisão das especificações do trabalho, a parte que mostrou os comandos que seriam usados (e.g. `meu_programa compactar <arquivo_original> <arquivo_compactado>`) evidenciou que seria melhor ter um módulo que atuaria como "Gateway", redirecionando o uso para os 3 outros módulos. No entanto, para que isso ocorresse, era necessário que os módulos deixassem de ser aplicações de console, tornando-se libs internas do projeto, e assim foi feito.

Logo, a estrutura do projeto ficou separada da seguinte forma:

      - AppSpag
      - Modules
         ∟ BuscaArquivoCompactado
         ∟ BuscaArquivoGrande
         ∟ Compressao

Onde:
- **`AppSpag`** (Spag == Sistema de Processamento de Arquivos Grandes)
   - É a aplicação console principal do projeto, com o código fonte contido em `MainApp`
   - Recebe os parâmetros do terminal e redireciona para cada módulo
   - Realiza a validação de argumentos vindos do terminal antes de redirecionar para os módulos

- **`Modules/BuscaArquivoCompactado`**
   - Lib referente à [Etapa 3](#etapa-3)
- **`Modules/BuscaArquivoGrande`**
   - Lib referente à [Etapa 2](#etapa-2)
- **`Modules/Compressao`**
   - Lib referente à [Etapa 1](#etapa-1)

Em cada módulo foi criado um método chamado estático chamado `InitApp`, que é o ponto de entrada da chamada do `MainApp`

## Etapa 1
### Título etapa 1

## Etapa 2
###Design da Busca Simples

A implementação da busca em arquivos grandes é centralizada na classe BuscaArquivosGrandeApp, que encapsula três componentes principais:

1. **Pré-processamento do padrão — CriarTabela**

      -Para otimizar o processo de busca, é construída uma tabela de deslocamentos, usada pelo algoritmo Horspool:
      -Para cada caractere presente no padrão, armazena-se sua distância em relação ao final do padrão.
      -Para todos os demais caracteres — aqueles que não ocorrem no padrão — atribui-se um deslocamento igual ao tamanho total do padrão.
      -Essa tabela permite que a busca avance rapidamente em caso de falhas, evitando retrabalho sobre regiões já descartadas.

2. **Algoritmo de busca — Buscar**
      - A rotina de busca segue o algoritmo Horspool, uma simplificação direta do Boyer–Moore:
      - A comparação é feita da direita para a esquerda, começando pelo final do padrão.Essa estratégia acelera a detecção de discrepâncias, já que diferenças no final das palavras são mais frequentes em textos naturais.
      - Ao identificar uma falha, o deslocamento aplicado é obtido diretamente da tabela gerada em CriarTabela.
      - O algoritmo é eficiente e simples, mantendo apenas a heurística de caractere ruim, sem utilizar a heurística de sufixo do Boyer–Moore completo.

3. **Controle de execução — InitApp**
      - A função principal da classe coordena a criação da tabela, a execução da busca e a leitura do arquivo em modo streaming: Leitura em blocos (Buffered I/O)
      - Em vez de carregar o arquivo inteiro na memória, utiliza-se um FileStream com buffer de aproximadamente 85 KB.
      - Essa abordagem evita exceções como OutOfMemoryException, comuns quando se tenta carregar arquivos grandes integralmente com ReadAllText.
      - Tratamento de fronteiras entre blocos (Overlap)
      - Como o padrão de busca pode estar dividido entre o final de um bloco e o início do próximo, é mantido um mecanismo de sobreposição.
      - A lógica funciona assim:
            - São guardados os últimos N−1 caracteres do bloco corrente (onde N é o tamanho do padrão).
            - Esses caracteres são concatenados ao início do próximo bloco lido.
            - Dessa forma, qualquer ocorrência que atravesse a fronteira entre buffers pode ser detectada corretamente pelo algoritmo.

## Etapa 3
### Design da Estrutura Indexada

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

### Funcionalidade da Busca no Arquivo Compactado

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

### Estratégias de Memória e Evolução em Relação à Etapa 2

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
