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

---

## Registro de Interações

*Copie e preencha o template abaixo para cada interação relevante.*

---

### Interação XPTO

- **Data:** loremipsumdolorsitarmet

- **Etapa do Projeto:** loremipsumdolorsitarmet

- **Ferramenta de IA Utilizada:** loremipsumdolorsitarmet

- **Objetivo da Consulta:** loremipsumdolorsitarmet

- **Prompt(s) Utilizado(s):**
  1. "loremipsumdolorsitarmet"
  2. "loremipsumdolorsitarmet"

- **Resumo da Resposta da IA:**
  loremipsumdolorsitarmet

- **Análise e Aplicação:**
  loremipsumdolorsitarmet

- **Referência no Código:**
  loremipsumdolorsitarmet


---

---

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


---
---

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

---
---

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

---
---


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


---
---

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



---
---

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

