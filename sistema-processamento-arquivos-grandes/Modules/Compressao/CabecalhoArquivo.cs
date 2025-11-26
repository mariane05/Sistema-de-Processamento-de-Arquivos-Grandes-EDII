namespace Compressao;

public static class CabecalhoArquivo
{
    //TODO: Verificar possibilidade de remover writer no param e usar dentro da classe
    public static void GerarCabecalho(BinaryWriter writer, Dictionary<char, long> dicionarioFrequencia)
    {
        writer.Write((byte)dicionarioFrequencia.Count);

        foreach (var item in dicionarioFrequencia)
        {
            char caractere = item.Key;
            long frequencia = item.Value;

            writer.Write((byte) caractere);
            writer.Write(frequencia);
        }
    }

}