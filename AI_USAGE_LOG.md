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

### Interação 2

- **Data:** ...
- **Etapa do Projeto:** ...
- **Ferramenta de IA Utilizada:** ...
- **Objetivo da Consulta:** ...
- **Prompt(s) Utilizado(s):** ...
- **Resumo da Resposta da IA:** ...
- **Análise e Aplicação:** ...
- **Referência no Código:** ...

---
