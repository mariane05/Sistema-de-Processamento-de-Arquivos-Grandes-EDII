namespace Compressao;

public static class BitWriter
{
    public static (byte[] bytes, int lastByte) CompactarEDevolverBytes(
        string texto, 
        Dictionary<char, string> dicionarioCodigoCaractere
    )
    {
        List<byte> arquivoCompactadoBytes = new List<byte>();

        int bitCount = 0;
        byte byteAtual = 0;

        foreach(char caractere in texto)
        {
            string codigoBinDoChar = dicionarioCodigoCaractere[caractere];

            foreach(char bit in codigoBinDoChar)
            {
                byteAtual <<= 1; // abre espaço para o próximo bit

                if(bit == 1) byteAtual |= 1; // coloca 1 no último bit

                bitCount++;
                

                //se tem 1 byte
                if(bitCount == 8)
                {
                    arquivoCompactadoBytes.Add(byteAtual);
                    bitCount = 0;
                    byteAtual = 0;
                }

            }
        }

        //byte incompleto
        if (bitCount > 0)
        {
            byteAtual <<= (8 - bitCount); //enche o resto com zeros
            arquivoCompactadoBytes.Add(byteAtual);
        }

        return (arquivoCompactadoBytes.ToArray(), bitCount);
    }
}