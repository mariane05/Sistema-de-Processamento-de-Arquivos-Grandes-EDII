namespace Compressao;

public class CompressaoApp
{
    public static void InitApp(string[] args)
    {
        string caminhoArquivo = args[1]; //dado que args[0] é o nome do programa, args[1] vai ser o programa pra ler
        testeLerArquivo(caminhoArquivo);
    }
    

    public static void testeLerArquivo(string caminhoArquivo)
    {
        
        string primeiraLinhaLida = lerPrimeiraLinhaArquivo(caminhoArquivo);
        Console.WriteLine(primeiraLinhaLida);
    }


    static string lerPrimeiraLinhaArquivo(string caminhoArquivo)
    {
        string? primeiraLinha;

        if(File.Exists(caminhoArquivo))
        {
            using (StreamReader reader = new StreamReader(caminhoArquivo))
            {
                primeiraLinha = reader.ReadLine();

                if(primeiraLinha == null) return "O arquivo está vazio";
            }

            return primeiraLinha;    
        } else {

            return $"Arquivo {caminhoArquivo} não encontrado";
        }
        
        
    } 


    // public static Dictionary <char, long> construirTabelaDeFrequências
    
}