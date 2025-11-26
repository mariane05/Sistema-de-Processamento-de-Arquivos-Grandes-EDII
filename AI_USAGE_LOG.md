# Relatório de Uso de Inteligência Artificial Generativa

Este documento registra todas as interações significativas com ferramentas de IA generativa (como Gemini, ChatGPT, Copilot, etc.) durante o desenvolvimento deste projeto. O objetivo é promover o uso ético e transparente da IA como ferramenta de apoio, e não como substituta para a compreensão dos conceitos fundamentais.

## Política de Uso
O uso de IA foi permitido para as seguintes finalidades:
- Geração de ideias e brainstorming de algoritmos.
- Explicação de conceitos complexos.
- Geração de código boilerplate (ex: estrutura de classes, leitura de arquivos).
- Sugestões de refatoração e otimização de código.
- Debugging e identificação de causas de erros.
- Geração de casos de teste.

É proibido submeter código gerado por IA sem compreendê-lo completamente e sem adaptá-lo ao projeto. Todo trecho de código influenciado pela IA deve ser referenciado neste log.

## Registro de Interações

### Interação 1

- **Data:** 22/10/2025

- **Etapa do Projeto:** 0 - Criação do Projeto

- **Ferramenta de IA Utilizada:** Chat GPT (Free License)

- **Objetivo da Consulta:** Apesar de ter concordado em realizar o trabalho em C#, eu não tinha conhecimento de como eram estruturados projetos em C#, ainda mais para a forma em que precisávamos trabalhar (3 módulos separados que rodariam independentemente).

- **Prompt(s) Utilizado(s):**
  0. *(prompts anteriores sobre instalação do ambiente no Linux)*
  1. "Agora eu quero criar um novo projeto, que vai ser um programa CLI, separado em 3 módulos principais. Como eu faço isso?"
  2. "Então, na real que o programa não vai ter um módulo principal, tipo uma main. Cada programa vai fazer sua própria execução. Alguns vão usar arquivos deixados por outros, mas não vão precisar interagir com os módulos rodando em tempo real. Dá uma olhada na descrição das etapas. Eu quero separar cada etapa em um módulo, já que cada um vai precisar ter seu próprio comando cli para rodar (preste atenção nisso ao ler a especificação do projeto)."
  <br>
  [copiei e colei o enunciado do projeto aqui, das linhas 31 até 72 (ln 31 ~ 72)](https://gitlab.com/ds143-alexkutzke/project-02-2025/-/blob/main/README.md?ref_type=heads&plain=1)
  <br>

- **Resumo da Resposta da IA:**
  Ela explicou quais comandos rodar para uma aplicação, começando com a ideia de ter um módulo que coordenava outros, como em um projeto Java. Depois de eu ter explicado mais específicamente o que eu precisava, ela me mostrou os comandos para criar uma solution (aparentemente é o nome de um projeto no C#), criar sub módulos e adicionar esses submódulos à aplicação sln. 
  Ela também comentou de criar um `wrapper script` para gerenciar os comandos cli para rodar cada módulo separadamente, e.g., `meu_programa compactar <arquivo_original> <arquivo_compactado>`. Essa ideia será analisada mais futuramente.

- **Análise e Aplicação:**
  Isso ajudou á estruturar a parte inicial do projeto e separar eles em pastas e módulos bem definidos, de modo que os projetos de cada um não entrassem em conflito diretamente.

- **Referência no Código:**
  Nenhuma. Apenas os exemplos de comandos executados (não foram exatamente esses):
  - `mkdir` MeuProjeto
  - `cd` MeuProjeto
  - `dotnet` new sln -n MeuProjetoEmCSharp
  - `dotnet` new console -o Etapa1_Compressao
  - `dotnet` new console -o Etapa2_BuscaSimples
  - `dotnet` new console -o Etapa3_BuscaNoCompactado
  - `dotnet` sln add Etapa1_Compressao Etapa2_BuscaSimples Etapa3_BuscaCompactado


### Interação 2

- **Data:** 23/10/2025

- **Etapa do Projeto:** 0 - Criação do Projeto

- **Ferramenta de IA Utilizada:** Chat GPT (Free License)

- **Objetivo da Consulta:** Enquanto tentava saber como fazer pra ler um arquivo via parâmetro do console, descobri que a estrutura de projeto que o gpt me mandou criar não funcionava bem, principalmente com as requisições passadas pelo professor de que, no terminal, seria passado o nome da aplicação + parâmetros para execução da mesma. Para fazer isso, eu não podia ter 3 projetos como console, mas sim, apenas um console e o resto teria que ser transformado em classLib. Fui concertar o problema.

- **Prompt(s) Utilizado(s):**
  1. "Tá, então vamos com calma. o meu programa não tem nenhuma forma cli ainda de funcionar. Como eu faço pra ele receber, via terminal, parâmetros? E como eu lido com eles?"
  2. "Tá, mas não faz setindo eu fazer esse switch aí pro primeiro argumento, sendo que eu to executando na pasta do meu projeto solo. Se for pra usar isso dessa forma, é melhor eu criar um arquivo de execução na raiz da solution e que daí desse arquivo, serão chamados os módulos, não concorda?"
  3. "Você tem noção de que você me fez dar voltas e voltas, não me explicou nada disso e fez eu criar o projeto inteiramente errado?"
  4. "Tá... e vou fazer o seguinte então: vou criar um novo sln e um novo projeto de console, que vai orquestrar os outros 3. Então planejo recriar os outros 3 como libs em vez de apps. De acordo com as melhores práticas de desenvolvimento em C#, isso procede?"
  5. "Tá, me explica o que esse código aqui que você mostrou quer dizer: dotnet add Orchestra/Orchestra.csproj reference Compressao/Compressao.csproj Esse 'Orchestra' seria esse app principal, que vai chamar cada uma das libs? Ou é um nome específico que eu tenho que seguir?"
  6. "Tá, e esses módulos que serão libs... seria recomendado, de acordo com as melhores práticas de desenvolvimento, colocar os 3 aninhados dentro de uma pasta? ou deixo eles 'lado a lado' com o projeto principal/orquestrador?"
  7. "Certo, o que eu fiz foi: Na raiz do projeto (mas na pasta sistema-de-processamento-de-arquivos-grandes, que é a pasta onde os módulos originais foram criados), eu criei um novo consoleApp chamado 'AppSpag'. Depois eu criei uma pasta chamada 'Modules' e dentro dela eu criei, dessa vez como classlib, cada um dos novos módulos. Agora eu tenho que associar esses arquivos/projetos ao meu sln total, ao mesmo tempo que eu tenho que remover os projetos antigos, porque, imagino que se eu for rodar o C# e um projeto que tá listado dentro do .sln, vai dar erro de compilação não é?"

- **Resumo da Resposta da IA:**
  Ela pediu desculpas por ter me feito errar no projeto e foi me guiando passo a passo em como eu deveria criar e modificar a estrutura atual do projeto para concertar o problema.

- **Análise e Aplicação:**
  Foi interessante ver em como confiamos tão cegamente em IA's as vezes. De toda forma, foi muito útil eu poder debater e discutir como criar um projeto em C# do zero, coisa que sozinho, pesquisando por aí na web, eu ia demorar um bom tempo pra reunir todas as informações específicas que eu precisava para o meu caso. A IA também deu algumas recomendações muito simplistas e algumas verbosas demais. Optei por um meio termo e, analisando a pilha de comandos que ela me mandou executar, reuní esses que foram os realmente úteis, em ordem de implementação pra resolver o problema.
  ```bash
      # Cria o projeto principal/orquestrador
      dotnet new console -o AppSpag

      # Cria os módulos, desta vez como classlib
      mkdir Modules
      cd Modules
      dotnet new classlib -o Compressao
      dotnet new classlib -o BuscaArquivoGrande
      dotnet new classlib -o BuscaArquivoCompactado

      # Lista os projetos associados à solution do projeto
      dotnet sln SistemaProcessamentoArquivosGrandes.sln list

      # Remove os projetos criados anteriormente
      dotnet sln SistemaProcessamentoArquivosGrandes.sln remove BuscaArquivoCompactado/BuscaArquivoCompactado.csproj
      dotnet sln SistemaProcessamentoArquivosGrandes.sln remove BuscaArquivoGrande/BuscaArquivoGrande.csproj
      dotnet sln SistemaProcessamentoArquivosGrandes.sln remove Compressao/Compressao.csproj 

      # Adiciona o novo projeto principal junto com os novos módulos
      dotnet sln SistemaProcessamentoArquivosGrandes.sln add AppSpag/AppSpag.csproj
      dotnet sln SistemaProcessamentoArquivosGrandes.sln add Modules/BuscaArquivoCompactado/BuscaArquivoCompactado.csproj
      dotnet sln SistemaProcessamentoArquivosGrandes.sln add Modules/BuscaArquivoGrande/BuscaArquivoGrande.csproj
      dotnet sln SistemaProcessamentoArquivosGrandes.sln add Modules/Compressao/Compressao.csproj 

      # Adiciona os módulos ao projeto principal
      dotnet add AppSpag/AppSpag.csproj reference Modules/BuscaArquivoCompactado/BuscaArquivoCompactado.csproj
      dotnet add AppSpag/AppSpag.csproj reference Modules/BuscaArquivoGrande/BuscaArquivoGrande.csproj
      dotnet add AppSpag/AppSpag.csproj reference Modules/Compressao/Compressao.csproj

      # testa pra ver se deu tudo certo
      dotnet build
    ```

- **Referência no Código:**
  Nenhuma. Porém a organização das pastas é um reflexo disso aí.

### Iteração 3

- **Data:** 11/11/2025
- **Etapa do Projeto:** Arquitetura
- **Ferramenta de IA Utilizada:** Gemini Advanced
- **Objetivo da Consulta:** Descobrir qual linguagem de programação seria mais ideal para o trabalho, considerando a perfomance do codigo

- **Prompt(s) Utilizado(s):**
  1. "Preciso fazer um trabalho sobre compactacao e busca de substring. Qual linguagem é ideial para esse trabalho e por quê?"


- **Resumo da Resposta da IA:**
A IA utilizou como referência conversas anteriores sobre tecnologias que eu gostaria de estudar e recomendou C# e Java, apresentando pontos relevantes como desempenho e a existência de classes prontas para manipulação de arquivos.

- **Análise e Aplicação:**
  A resposta da IA foi útil para esclarecer as opções. Conversei com o grupo e apresentei a análise da IA, incluindo uma tabela comparativa entre Java, Python e C#.

- **Referência no Código:**
  Sem refencia no codigo


### Iteração 4

- **Data:** 24/11/2025
- **Etapa do Projeto:** Escolha entre os algoritmos KMP E Boyer-Moore
- **Ferramenta de IA Utilizada:** Gemini Advanced
- **Objetivo da Consulta:** O trabalho envolvia recomendações de código, então a pesquisa foi direcionada à performance dos algoritmos e às possíveis dificuldades na implementação.
- **Prompt(s) Utilizado(s):**
  1. "Gemini, preciso desenvolver uma função que faz busca de string em um arquivo grande. Recomenda usar Knuth-Morris-Pratt (KMP) ou Boyer-Moore? Faça uma análise de cada um e mostre as dificuldades na implementação de cada um."

- **Resumo da Resposta da IA:**
  A IA explicou a diferença entre os algoritmos e simulou a criação de uma tabela-verdade para cada código, verificando por meio de perguntas se eu estava entendendo o algoritmo.

- **Análise e Aplicação:**
  A resposta da IA foi util para entender a diferença entre os algoritmos.

- **Referência no Código:**
  sem referencia no código


### Iteração 5

- **Data:** 26/11/2025
- **Etapa do Projeto:** Implementação de funcoes prontas para leitura de arquivos grandes em blocos
- **Ferramenta de IA Utilizada:** Gemini Advanced
- **Objetivo da Consulta:** O trabalho exigia um requisito em que a função não ultrapassasse a quantidade de memória RAM disponível, evitando travamentos durante a execução.
  1. "Gemini, no momento estou usando a função File.ReadAllText, porém o uso dessa função não é recomendado. Mostre funções prontas para leitura de arquivo que não corram o risco de travar o sistema."

- **Resumo da Resposta da IA:**
  A IA explicou listou 6 funcoes prontas do c# mostrando uso e vantangens de cada um.

- **Análise e Aplicação:**
  A resposta da IA foi util para implementar um dos requisitos do trabalho.

- **Referência no Código:**
trecho do código 
```
try
{
    using FileStream fs = new FileStream(
        caminhoArquivo,
        FileMode.Open,
        FileAccess.Read,
        FileShare.Read,
        tamanhoBuffer,
        FileOptions.SequentialScan
    );

    int bytesLidos;
    while ((bytesLidos = await fs.ReadAsync(bufferLeitura, 0, bufferLeitura.Length)) > 0)
    {
        string texto = sobra + Encoding.UTF8.GetString(bufferLeitura, 0, bytesLidos);
        resultadosBusca.AddRange(Buscar(texto.ToLower(), padraoBusca));

        if (texto.Length >= padraoBusca.Length - 1)
            sobra = texto[^(padraoBusca.Length - 1)..];
        else
            sobra = texto;
    }
}
catch (Exception ex)
{
    Console.WriteLine("Erro ao ler arquivo: " + ex.Message);
    return;
}
```


### Interação 06

* **Data:** 22/11/2025

* **Etapa do Projeto:** Compressão

* **Ferramenta de IA Utilizada:** Gemini

* **Objetivo da Consulta:** Definir um tamanho ideal para o bloco de compressão Huffman, equilibrando desempenho de I/O e consumo de memória RAM.

* **Prompt(s) Utilizado(s):**

  1. "tô fazendo compressão Huffman em C#, que tamanho de bloco você recomenda pra lidar com arquivo grande e ainda conseguir fazer busca depois?"
  2. "faz sentido usar 64KB ou 256KB como tamanho de bloco? qual é a lógica por trás desses valores?"

* **Resumo da Resposta da IA:**
  A IA sugeriu que tamanhos entre 64 KB e 256 KB são bons *trade-offs*. 64 KB alinha-se bem com a otimização de I/O de sistemas operacionais e mantém o overhead de memória da árvore de Huffman por bloco baixo.

* **Análise e Aplicação:**
  O valor de **64 * 1024** foi adotado como constante `TAMANHO_BLOCO` para a classe de compressão. Isso limita o consumo máximo de RAM durante a descompressão seletiva na Etapa 3.

* **Referência no Código:**
  `private const int TAMANHO_BLOCO = 64 * 1024;` (Classe `CompressaoApp`)


### Interação 07

* **Data:** 22/11/2025

* **Etapa do Projeto:** Compressão (Cálculo de Metadados)

* **Ferramenta de IA Utilizada:** Gemini

* **Objetivo da Consulta:** Obter a fórmula correta para calcular o número total de blocos a partir do tamanho do arquivo, garantindo que o último bloco parcial seja contado (função teto em inteiros).

* **Prompt(s) Utilizado(s):**

  1. "como que eu calculo o número de blocos só com aritmética inteira em C#, tipo fazendo teto(tamanhoTotal / tamanhoBloco)?"
  2. "pode explicar por que a fórmula (A + B - 1) / B funciona como teto na divisão inteira?"

* **Resumo da Resposta da IA:**
  A IA forneceu a fórmula `(tamanhoTotal + tamanhoBloco - 1) / tamanhoBloco`, explicando que o termo `-1` é necessário para neutralizar o arredondamento para baixo (truncamento) da divisão de inteiros.

* **Análise e Aplicação:**
  A fórmula foi implementada no início da compactação para determinar a contagem exata de blocos a serem processados e para alocar o espaço correto para o Índice de Blocos.

* **Referência no Código:**
  `int numeroBlocos = (int)((tamanhoOriginalTotalBytes + TAMANHO_BLOCO - 1) / TAMANHO_BLOCO);` (Classe `CompressaoApp`)


### Interação 08

* **Data:** 22/11/2025

* **Etapa do Projeto:** Busca (Implementação do Janelamento)

* **Ferramenta de IA Utilizada:** Gemini

* **Objetivo da Consulta:** Criar a lógica para tratar ocorrências de substrings que atravessam a fronteira de dois blocos, usando o conceito de Janelamento.

* **Prompt(s) Utilizado(s):**

  1. "tô descompactando o arquivo em blocos e procurando uma substring em cada um; como faço o janelamento pra não perder quando o padrão começa num bloco e termina no outro? se puder, mostra em C#."
  2. "se o padrão tem tamanho L, qual é exatamente o pedaço do bloco anterior que eu preciso guardar pra usar junto com o próximo bloco?"

* **Resumo da Resposta da IA:**
  A IA sugeriu manter um **sufixo** do bloco anterior de tamanho igual a $L-1$ (tamanho do padrão - 1). Este sufixo é concatenado ao novo bloco para criar uma "janela" de busca. O código de *substring* para a janela e a atualização do sufixo (`sufixoAnterior = textoBloco;`) foram fornecidos.

* **Análise e Aplicação:**
  A lógica de janelamento foi implementada na função `BuscarNoArquivoComprimido`. A variável `sufixoAnterior` foi usada para armazenar o bloco descompactado inteiro, e o cálculo do sufixo foi feito via `Substring` com `tamanhoPadrao - 1`.

* **Referência no Código:**

  ```
  // Lógica influenciada pela IA na função BuscarNoArquivoComprimido
  string sufixo = tamanhoSufixo > 0 
      ? sufixoAnterior.Substring(sufixoAnterior.Length - tamanhoSufixo, tamanhoSufixo) 
      : string.Empty;
  string janela = sufixo + textoBloco;
  // ...
  sufixoAnterior = textoBloco; // Atualização do sufixo
  ```

### Interação 09

* **Data:** 22/11/2025

* **Etapa do Projeto:** Busca (Conversão de Posição Global)

* **Ferramenta de IA Utilizada:** Gemini

* **Objetivo da Consulta:** Ajustar as posições das ocorrências encontradas dentro da janela de busca local para posições globais no arquivo original, considerando o *offset* e o sufixo.

* **Prompt(s) Utilizado(s):**

  1. "como que eu transformo essa posição local na posição global do arquivo original usando o OffsetOriginal do bloco e o tamanho do sufixo?"

* **Resumo da Resposta da IA:**
  A IA explicou que a posição global é o `OffsetOriginal` do bloco, ajustado pelo início do sufixo e mais a posição local. A fórmula básica é: `posGlobal = (bloco.OffsetOriginal - tamanhoSufixo) + posLocal`.

* **Análise e Aplicação:**
  A fórmula de conversão de posição foi implementada no loop de resultados locais. Isso garante que as ocorrências que caem na área de sobreposição (`sufixo`) sejam mapeadas corretamente para o início do bloco anterior.

* **Referência no Código:**

  ```
  // Lógica de conversão influenciada pela IA na função BuscarNoArquivoComprimido
  foreach (int posLocal in ocorrenciasLocal)
  {
      long posGlobal = (bloco.OffsetOriginal - tamanhoSufixo) + posLocal;
      resultadosGlobais.Add(posGlobal);
  }
  ```


### Interação 10

* **Data:** 22/11/2025

* **Etapa do Projeto:** Debugging (Descompressão Huffman)

* **Ferramenta de IA Utilizada:** Gemini

* **Objetivo da Consulta:** Investigar por que a Etapa 2 (busca simples) funcionava corretamente, enquanto a Etapa 1 (compressão/descompressão Huffman) estava gerando tamanhos errados e menos ocorrências de substring do que o esperado.

* **Prompt(s) Utilizado(s):**

  1. "a parte 2 (busca simples) está funcionando, mas a parte 1 (compressão/descompressão Huffman) parece estar com problema na descompressão; você consegue me ajudar a identificar onde está o bug?"

* **Resumo da Resposta da IA:**
  A IA apontou dois problemas principais: um descasamento na ordem dos bits entre o BitWriter e o leitor de Huffman, e erros no cabeçalho ao gravar/ler caracteres Unicode (uso de byte para char). Também sugeriu ajustar o tipo do contador de símbolos para evitar estouro.

* **Análise e Aplicação:**
  A partir da resposta, reescrevi a lógica do BitWriter para preencher os bits na mesma ordem usada pelo leitor, corrigi o cabeçalho para gravar os caracteres como ushort e aumentei o tipo do contador de símbolos. Depois de recompilar e gerar novamente os arquivos .comp, os tamanhos descompactados passaram a bater com o arquivo original e a contagem de ocorrências ficou alinhada com a Etapa 2.

* **Referência no Código:**

* Classe BitWriter em Compressao/BitWriter.cs (reescrita da rotina de escrita de bits).

* Ajustes em CabecalhoArquivo.cs para gravar símbolos como ushort e contador como int.

* Ajustes simétricos de leitura em BuscarArquivoCompactadoApp.DescomprimirBlocoHuffman().



### `Interação Exemplo (APAGAR NO FINAL)`

- **Data:** 20/10/2025
- **Etapa do Projeto:** 1 - Compressão de Arquivos
- **Ferramenta de IA Utilizada:** Gemini Advanced
- **Objetivo da Consulta:** Eu estava com dificuldades para entender como gerenciar o dicionário do algoritmo LZW quando ele atinge o tamanho máximo. Precisava de uma estratégia para lidar com isso.

- **Prompt(s) Utilizado(s):**
  1. "No algoritmo de compressão LZW, o que acontece quando o dicionário atinge o tamanho máximo? Quais são as estratégias mais comuns para lidar com isso?"
  2. "Pode me dar um exemplo em Python de como implementar a estratégia de 'resetar o dicionário' no LZW?"

- **Resumo da Resposta da IA:**
  A IA explicou três estratégias: 1) parar de adicionar novas entradas, 2) resetar o dicionário para o estado inicial, e 3) usar uma política de descarte, como LRU (Least Recently Used), que é mais complexa. A IA forneceu um pseudocódigo para a estratégia de reset, que parecia a mais simples e eficaz para este projeto.

- **Análise e Aplicação:**
  A resposta da IA foi extremamente útil para clarear as opções. Optei por implementar a estratégia de resetar o dicionário. O código fornecido pela IA não foi usado diretamente, pois estava muito simplificado e não se encaixava na minha arquitetura de classes. No entanto, a lógica de verificar o tamanho do dicionário e invocar uma função `reset_dictionary()` foi a base para a minha implementação. Isso me poupou tempo de pesquisa em artigos e livros.

- **Referência no Código:**
  A lógica inspirada por esta interação foi implementada no arquivo `compressor/lzw.py`, especificamente na função `compress()`, por volta da linha 85.

---

