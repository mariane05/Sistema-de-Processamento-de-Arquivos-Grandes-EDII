namespace Compressao;

public class CompressaoApp
{
    public static void InitApp(string[] args)
    {
        debugaPathArquivo(args);
        // return;

        string caminhoArquivo = args[1]; //dado que args[0] é o nome do programa, args[1] vai ser o programa pra ler
        testeLerArquivo(caminhoArquivo);
    }
    

    public static void testeLerArquivo(string caminhoArquivo)
    {
        
        
        string primeiraLinhaLida = lerPrimeiraLinhaArquivo(caminhoArquivo);
        Console.WriteLine(primeiraLinhaLida);
    }

    public static void debugaPathArquivo(string[] args)
    {
        Console.WriteLine($"Path recebido: >{args[1]}<");

        foreach (var b in System.Text.Encoding.UTF8.GetBytes(args[1]))
            Console.Write($"{b:x2} ");
        Console.WriteLine();
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