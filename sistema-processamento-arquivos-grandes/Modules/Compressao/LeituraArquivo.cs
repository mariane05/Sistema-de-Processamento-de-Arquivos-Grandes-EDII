namespace Compressao;

public static class LeituraArquivo
{
    public static void testeLerArquivo(string caminhoArquivo)
    {
        
        DicionarioFrequencia.testeGerarDicionarioFrequencia(caminhoArquivo);
        
    }



    public static string separaCaminhoDoArquivo(string[] args)
    {
        if (args.Length < 2 || string.IsNullOrEmpty(args[1]))
        {
            throw new Exception("Não foi encontrado o nome do arquivo para ser lido!");
        }

        return args[1];
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
}