namespace BuscaArquivoGrande;

public class BuscaArquivoGrandeApp
{
    public static void InitApp(string[] args)
    {
        string caminhoArquivo = args[1].ToLower(); //padronizar  texto inteiro e padrao para minisculo
        string padraoBusca = args[2].ToLower();

        // TODO: Implementar leitura eficiente de arquivos grandes
        string textoArquivo = File.ReadAllText(caminhoArquivo);
        List<int> resultadosBusca = Buscar(textoArquivo, padraoBusca);

        if (resultadosBusca.Count > 0)
        {
            Console.WriteLine($"Padrão encontrado nas posições:");

            for (int i = 0; i < resultadosBusca.Count; i++)
            {
                Console.WriteLine(resultadosBusca[i]);
            }
        }
        else
        {
            Console.WriteLine("Padrão não encontrado no arquivo.");
        }

    }

    public static int[] CriarTabela(string padrao, int tamanhoPadrao)
    {
        int[] tabela = new int[256];

        // Inicializa a tabela com o tamanho do padrão
        for (int i = 0; i < tabela.Length; i++)
        {
            tabela[i] = tamanhoPadrao;
        }
        // Preenche a tabela com os deslocamentos corretos
        for (int j = 0; j < tamanhoPadrao - 1; j++)
        {
            int codigoAsci = (int)padrao[j];
            if (codigoAsci < 255)
            {
                tabela[(int)padrao[j]] = tamanhoPadrao - 1 - j;
            }
            else
            {
                Console.WriteLine(codigoAsci);

            }
        }

        return tabela;
    }

    public static List<int> Buscar(string texto, string padrao)
    {
        int tamanhoTexto = texto.Length;
        int tamanhoPadrao = padrao.Length;
        List<int> ocorrencias_padrao = new List<int>();


        int[] tabela = CriarTabela(padrao, tamanhoPadrao);

        int i = tamanhoPadrao - 1;

        while (i < tamanhoTexto)
        {

            int para_tras_texto = i;
            int para_tras_padrao = tamanhoPadrao - 1;

            // Loop de comparação de trás para frente
            while (para_tras_padrao >= 0 && texto[para_tras_texto] == padrao[para_tras_padrao])
            {
                para_tras_texto--;
                para_tras_padrao--;
            }

            if (para_tras_padrao < 0)
            {
                // Encontrou!
                // O índice inicial é onde o 'i' estava menos o tamanho do padrão (+1 para ajustar)
                ocorrencias_padrao.Add(i - tamanhoPadrao + 1);
            }

            int codigoAsci = (int)texto[i];

            if (codigoAsci < 255)
            {
                
                i += tabela[codigoAsci];
            }
            else
            {
                i += tamanhoPadrao;
            }


        }
            return ocorrencias_padrao;
    }
}

