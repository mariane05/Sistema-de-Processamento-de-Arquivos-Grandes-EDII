namespace Compressao;

public static class CabecalhoArquivo
{
    public static void GerarCabecalho(BinaryWriter writer, Dictionary<char, long> dicionarioFrequencia)
    {
        writer.Write((byte)dicionarioFrequencia.Count);

        foreach (var item in dicionarioFrequencia)
        {
            writer.Write((ushort)item.Key); 
            writer.Write(item.Value);
        }
    }

}