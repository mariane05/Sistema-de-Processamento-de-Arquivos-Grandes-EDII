using System;
using BuscaArquivoCompactado;
using BuscaArquivoGrande;
using Compressao;

public class MainApp
{
    public static void Main(string[] args)
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
                ValidaParamsBuscaArquivoGrande(args);
                break;

            case "buscar_compactado":
                ValidaParamsBuscaArquivoCompactado(args);
                break;

            default:
                Console.WriteLine("Programa não encontrado");
                break;
        }
    }

    private static void ValidaParamsCompressao(string[] args)
    {
        // Esperado: compactar <arquivo_original> <arquivo_compactado>
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para compactar. Uso: compactar <arquivo_original> <arquivo_compactado>");
            return;
        }

        CompressaoApp.InitApp(args);
    }

    private static void ValidaParamsBuscaArquivoGrande(string[] args)
    {
        // Esperado: buscar_simples <arquivo_original> "<substring>"
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo grande. Uso: buscar_simples <arquivo_original> \"<substring>\"");
            return;
        }

        Console.WriteLine("Iniciando busca em arquivo grande (não compactado)...");
        Console.WriteLine($"Arquivo: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");

        BuscaArquivoGrandeApp.InitApp(args);
    }

    private static void ValidaParamsBuscaArquivoCompactado(string[] args)
    {
        // Esperado: buscar_compactado <arquivo_compactado> "<substring>"
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo compactado. Uso: buscar_compactado <arquivo_compactado> \"<substring>\"");
            return;
        }

        Console.WriteLine("Iniciando busca em arquivo compactado...");
        Console.WriteLine($"Arquivo compactado: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");

        // aqui é importante passar args!
        BuscarArquivoComprimidoApp.InitApp(args);
    }
}
