namespace Compressao;

public static class DicionarioFrequencia
{

    public static void testeGerarDicionarioFrequencia(string caminhoArquivo)
    {
        
        var dicionarioDeFrequencias = construirDicionarioDeFrequências(caminhoArquivo);
        lerDicionarioDeFrequências(dicionarioDeFrequencias);

        Console.WriteLine("Finalizado o teste de geração de Dicionário");
    }


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

        Console.WriteLine("Finalizada a criação do dicionário de frequências");

        return frequencyDic;
    }

    public static void lerDicionarioDeFrequências(Dictionary <char, long> dic)
    {
        Console.WriteLine("Iniciando a leitura do dicionário de Frequências");

        foreach(var item in dic)
            Console.WriteLine($"Caractere: '{item.Key}'; Ocorrências: ${item.Value}");
    }
}