
using System.Text;


namespace BuscaArquivoGrande;

public class BuscaArquivoGrandeApp
{
    private static int[] tabela = new int[256];
    public static async Task InitApp(string[] args)
    {

        int tamanhoBuffer = 85 * 1024; //85KB RECOMENDADO
        string caminhoArquivo = args[1];
        string padraoBusca = args[2];
       


        List<int> resultadosBusca = new List<int>();
        byte[] bufferLeitura = new byte[tamanhoBuffer];

        string sobra = "";

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
                resultadosBusca.AddRange(Buscar(texto, padraoBusca));

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

        if (resultadosBusca.Count > 0)
        {
            Console.WriteLine("Padrão encontrado nas posições:");
            for (int i = 0; i < resultadosBusca.Count; i++)
                Console.WriteLine($"{i + 1}º {resultadosBusca[i]}");
        }
        else
        {
            Console.WriteLine("Padrão não encontrado no arquivo.");
        }
    }

    public static int[] CriarTabela(string padrao, int tamanhoPadrao)
    {
        
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
        }

        return tabela;
    }

    public static List<int> Buscar(string texto, string padrao)
    {
        int tamanhoTexto = texto.Length;
        int tamanhoPadrao = padrao.Length;
        List<int> ocorrencias_padrao = new List<int>();
        tabela = CriarTabela(padrao, padrao.Length);

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

            if (codigoAsci < 256)
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

