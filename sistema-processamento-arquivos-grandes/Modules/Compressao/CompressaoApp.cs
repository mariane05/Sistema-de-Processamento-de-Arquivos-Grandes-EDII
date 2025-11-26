using static Compressao.LeituraArquivo;
using static Compressao.DicionarioFrequencia;
using static Compressao.ConstruirArvore.ArvoreBuilder;
using static Compressao.GerarCodigosBinarios;

namespace Compressao;

public class CompressaoApp
{
    public static void InitApp(string[] args)
    {
        string caminhoArquivo = separaCaminhoDoArquivo(args);
        var dicionarioDeFrequencias = construirDicionarioDeFrequências(caminhoArquivo);
        var noRaiz = BuildArvore(dicionarioDeFrequencias);
        var dicionarioDeCodigos = gerarTodosOsCodigos(noRaiz);
        
        Console.WriteLine("Finalizada a Compressao");
    }
    
}