namespace Compressao;

public static class DicionarioFrequencia
{

    public static void testeGerarDicionarioFrequencia(string caminhoArquivo)
    {
        
        var dicionarioDeFrequencias = construirDicionarioDeFrequências(caminhoArquivo);
        lerDicionarioDeFrequências(dicionarioDeFrequencias);
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

        return frequencyDic;
    }

    public static void lerDicionarioDeFrequências(Dictionary <char, long> dic)
    {
        foreach(var item in dic)
            Console.WriteLine($"Caractere: '{item.Key}'; Ocorrências: ${item.Value}");
    }
}