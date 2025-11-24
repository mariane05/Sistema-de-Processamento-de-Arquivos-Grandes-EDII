using BuscaArquivoCompactado;
using BuscaArquivoGrande;
using Compressao;

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
        if (args[1] == null || args[1].Length == 0)
        {
            Console.WriteLine("Não foi encontrado o nome do arquivo para ser lido!");
            return;
        }

        /*
        IMPLEMENTAR DEPOIS QUE A COMPRESSÃO ESTIVER CERTINHO
        if (args[2] == null || args[2].Length == 0)
        {
            Console.WriteLine("Não foi encontrado o nome que será atribuido ao arquivo compactado!");
            return;
        }
        */
        

        CompressaoApp.InitApp(args);
    }

    public static void validaParamsBuscaArquivoGrande(string[] args)
    {
        //TODO: adicionar validações dos parâmetros <arquivo_original> && "<substring>" ao chamar o módulo

        BuscaArquivoGrandeApp.InitApp();
    }

    public static void validaParamsBuscaArquivoCompactado(string[] args)
    {
        //TODO: adicionar validações dos parâmetros <arquivo_compactado> && "<substring>" ao chamar o módulo

        BuscaArquivoCompactadoApp.InitApp();
    }

}
