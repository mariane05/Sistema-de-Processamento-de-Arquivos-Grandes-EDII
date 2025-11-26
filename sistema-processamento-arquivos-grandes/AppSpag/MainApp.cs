using BuscaArquivoCompactado;
using BuscaArquivoGrande;
using Compressao;
using System.Diagnostics;


public class MainApp ()
{
    public static void Main(string[] args)
    {

        

        if(args == null || args.Length == 0)
        {
            Console.WriteLine("Nenhum argumento foi passado. Escolha um programa para executar");
            return;
        }

        switch (args[0])
        {
            case "compactar":
                validaParamsCompressao(args);
                break;
            case "buscar_simples":
                validaParamsBuscaArquivoGrande(args);
                break;
            case "buscar_compactado":
                validaParamsBuscaArquivoCompactado(args);
                break;
            default:
                Console.WriteLine("Programa não encontrado");
                break;

        }
        
    }

    public static void validaParamsCompressao(string[] args)
    {
        //TODO: adicionar validações dos parâmetros <arquivo_original> && <arquivo_compactado> ao chamar o módulo
        
        CompressaoApp.InitApp();
    }

    public static void validaParamsBuscaArquivoGrande(string[] args)
    {
        // tempo de execução
        Stopwatch tempoExecucao = new Stopwatch();
        tempoExecucao.Start();

        // memoria ram utilizada
        Process processo = Process.GetCurrentProcess();
        long memoria = processo.WorkingSet64;

        //TODO: adicionar validações dos parâmetros <arquivo_original> && "<substring>" ao chamar o módulo
        if(args == null || args.Length < 2)
        {
            Console.WriteLine("Parâmetros insuficientes para busca em arquivo compactado.");
            return;
        }

        Console.WriteLine("Iniciando busca");
        Console.WriteLine($"Arquivo: {args[1]}");
        Console.WriteLine($"Padrão de busca: {args[2]}");
    

        BuscaArquivoGrandeApp.InitApp(args);

        tempoExecucao.Stop();
        Console.WriteLine($"Tempo de execução: {tempoExecucao.ElapsedMilliseconds}");
        Console.WriteLine($"Uso de RAM: {memoria / 1024 / 1024} MB");
    }

    public static void validaParamsBuscaArquivoCompactado(string[] args)
    {
        //TODO: adicionar validações dos parâmetros <arquivo_compactado> && "<substring>" ao chamar o módulo

    
        BuscaArquivoCompactadoApp.InitApp();
    }
    

}
