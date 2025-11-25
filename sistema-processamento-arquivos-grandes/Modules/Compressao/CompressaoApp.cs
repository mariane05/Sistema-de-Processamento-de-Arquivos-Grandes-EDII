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
        
        var dicionarioDeFrequencias = construirDicionarioDeFrequências(caminhoArquivo);
        lerDicionarioDeFrequências(dicionarioDeFrequencias);
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






    //todo:validar o quanto isso demora de fato
    public static Dictionary <char, long> construirDicionarioDeFrequências(string pathToFile)
    {
        var frequencyDic = new Dictionary<char, long>();

        using ( var reader = new StreamReader(pathToFile))
        {
            int caractereSendoLido;

            while((caractereSendoLido = reader.Read()) != -1)
            {
                char c = (char)caractereSendoLido;
                if (frequencyDic.ContainsKey(c))
                    frequencyDic[c] ++;
                else
                    frequencyDic[c] = 1;
                
            }
        }

        return frequencyDic;
    }

    public static void lerDicionarioDeFrequências(Dictionary <char, long> dic)
    {
        foreach(var item in dic)
            Console.WriteLine($"Caractere: '{item.Key}'; Ocorrências: ${item.Value}");
    }
    
}