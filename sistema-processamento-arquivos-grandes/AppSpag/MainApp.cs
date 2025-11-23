using BuscaArquivoCompactado;
using BuscaArquivoGrande;
using Compressao;

public class MainApp ()
{
    public static void Main(string[] args)
    {
        BuscaArquivoCompactadoApp.InitApp(); 
        BuscaArquivoGrandeApp.InitApp(); 
        CompressaoApp.InitApp(); 
    }
}
