---

````markdown
# Guia Rápido

## Pré-requisitos
- .NET SDK 6.0 ou superior instalado
- Projeto compilado como aplicação de console

---

## Build do Projeto

No diretório raiz:

```bash
dotnet build
````

---

## 1. Compactar um Arquivo

**Uso:**

```bash
dotnet run -- compactar <arquivo_original> <arquivo_compactado>
```

**Exemplo:**

```bash
dotnet run -- compactar dados/entrada.txt dados/entrada.comp
```

---

## 2. Busca Simples em Arquivo Não Compactado

**Uso:**

```bash
dotnet run -- buscar_simples <arquivo_original> "<substring>"
```

**Exemplo:**

```bash
dotnet run -- buscar_simples dados/entrada.txt "erro 404"
```

Saída esperada: lista de offsets onde o padrão aparece no arquivo original.

---

## 3. Busca em Arquivo Compactado

**Uso:**

```bash
dotnet run -- buscar_compactado <arquivo_compactado> "<substring>"
```

**Exemplo:**

```bash
dotnet run -- buscar_compactado dados/entrada.comp "erro 404"
```

Saída esperada: offsets relativos ao arquivo original, calculados com base no índice de blocos.

```

---

Se quiser, posso gerar esse Markdown diretamente como **arquivo `.md` para download** também.
```
