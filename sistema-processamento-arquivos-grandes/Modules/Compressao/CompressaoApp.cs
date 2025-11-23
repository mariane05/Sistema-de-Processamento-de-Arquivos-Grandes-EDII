namespace Compressao;

public class CompressaoApp
{
    public static void InitApp()
    {
        testeLerArquivo();
    }
    

    public static void testeLerArquivo()
    {
        string caminhoArquivo = "../../livro01.txt";
        string primeiraLinhaLida = lerPrimeiraLinhaArquivo(caminhoArquivo);
        Console.WriteLine(primeiraLinhaLida);
    }

    static string lerPrimeiraLinhaArquivo(string caminhoArquivo)
    {
        string? primeiraLinha;

        if(File.Exists(caminhoArquivo)) return $"Arquivo {caminhoArquivo} não encontrado";
        
        using (StreamReader reader = new StreamReader(caminhoArquivo))
        {
            primeiraLinha = reader.ReadLine();

            if(primeiraLinha == null) return "O arquivo está vazio";
        }

        return primeiraLinha;
    } 
    
}