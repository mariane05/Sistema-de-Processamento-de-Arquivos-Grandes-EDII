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
                CompressaoApp.InitApp();
                break;
            case "buscar_simples":
                BuscaArquivoGrandeApp.InitApp();
                break;
            case "buscar_compactado":
                BuscaArquivoCompactadoApp.InitApp();
                break;
            default:
                Console.WriteLine("Programa não encontrado");
                break;

        }
        
    }

}
