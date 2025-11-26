using System;
using System.Diagnostics;
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
    private static void ValidaParamsBuscaArquivoGrande(string[] args)
    {
        // Esperado: buscar_simples <arquivo_original> "<substring>"
        if (args.Length < 3)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo grande. Uso:");
            Console.WriteLine("buscar_simples <arquivo_original> \"<substring>\"");
            return;
        }

        // Medição de desempenho
        Stopwatch tempoExecucao = new Stopwatch();
        tempoExecucao.Start();

        Process processo = Process.GetCurrentProcess();
        long memoriaAntes = processo.WorkingSet64;

        Console.WriteLine("Iniciando busca em arquivo grande (não compactado)...");
        Console.WriteLine($"Arquivo: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");

        BuscaArquivoGrandeApp.InitApp(args);

        tempoExecucao.Stop();
        processo.Refresh();
        long memoriaDepois = processo.WorkingSet64;

        Console.WriteLine($"Tempo de execução: {tempoExecucao.ElapsedMilliseconds} ms");
        Console.WriteLine($"Uso de RAM: {(memoriaDepois - memoriaAntes) / 1024 / 1024} MB");
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
