namespace Compressao;

public class CompressaoApp
{
    public static void InitApp(string[] args)
    {
        string caminhoArquivo = LeituraArquivo.separaCaminhoDoArquivo(args);
        var dicionarioDeFrequencias = DicionarioFrequencia.construirDicionarioDeFrequências(caminhoArquivo);
        // DicionarioFrequencia.testeGerarDicionarioFrequencia(caminhoArquivo);
        
        Console.WriteLine("Finalizada a Compressao");
    }
    

    // public static void testeLerArquivo(string caminhoArquivo)
    // {
        
    //     DicionarioFrequencia.testeGerarDicionarioFrequencia(caminhoArquivo);
    // }





    // static string lerPrimeiraLinhaArquivo(string caminhoArquivo)
    // {
    //     string? primeiraLinha;

    //     if(File.Exists(caminhoArquivo))
    //     {
    //         using (StreamReader reader = new StreamReader(caminhoArquivo))
    //         {
    //             primeiraLinha = reader.ReadLine();

    //             if(primeiraLinha == null) return "O arquivo está vazio";
    //         }

    //         return primeiraLinha;    
    //     } else {

    //         return $"Arquivo {caminhoArquivo} não encontrado";
    //     }
        
        
    // } 
    
}