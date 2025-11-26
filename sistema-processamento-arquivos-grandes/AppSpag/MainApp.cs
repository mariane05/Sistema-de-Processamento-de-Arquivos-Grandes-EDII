
using System.Diagnostics;
using BuscaArquivoCompactado;
using BuscaArquivoGrande;
using Compressao;

public class MainApp
{
    public static async Task Main(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            Console.WriteLine("Nenhum argumento foi passado. Escolha um programa para executar.");
            return;
        }

        switch (args[0])
        {
            case "compactar":
                ValidaParamsCompressao(args);
                break;

            case "buscar_simples":
                await ValidaParamsBuscaArquivoGrande(args);
                break;

            case "buscar_compactado":
                ValidaParamsBuscaArquivoCompactado(args);
                break;

            default:
                Console.WriteLine("Programa não encontrado");
                break;
        }
    }

    //COMPACTAÇÃO
    private static void ValidaParamsCompressao(string[] args)
    {
        // Esperado: compactar <arquivo_original> <arquivo_compactado>
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para compactar. Uso:");
            Console.WriteLine("compactar <arquivo_original> <arquivo_compactado>");
            return;
        }

        CompressaoApp.InitApp(args);
    }

    //BUSCA SIMPLES
    private static async Task ValidaParamsBuscaArquivoGrande(string[] args)
    {
        // Esperado: buscar_simples <arquivo_original> "<substring>"
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo grande. Uso:");
            Console.WriteLine("buscar_simples <arquivo_original> \"<substring>\"");
            return;
        }

        Console.WriteLine("Iniciando busca em arquivo grande (não compactado)...");
        Console.WriteLine($"Arquivo: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");

        await BuscaArquivoGrandeApp.InitApp(args);

    

    }

    //BUSCA EM ARQUIVO COMPACTADO

    private static void ValidaParamsBuscaArquivoCompactado(string[] args)
    {
        // Esperado: buscar_compactado <arquivo_compactado> "<substring>"
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo compactado. Uso:");
            Console.WriteLine("buscar_compactado <arquivo_compactado> \"<substring>\"");
            return;
        }

        Console.WriteLine("Iniciando busca em arquivo compactado...");
        Console.WriteLine($"Arquivo compactado: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");

        // Chamada correta com args
        BuscarArquivoComprimidoApp.InitApp(args);
    }
}
