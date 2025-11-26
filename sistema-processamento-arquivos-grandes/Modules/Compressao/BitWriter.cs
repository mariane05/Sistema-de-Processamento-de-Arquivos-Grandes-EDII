namespace Compressao;

public static class BitWriter
{
    public static (byte[] bytes, int lastByte) CompactarEDevolverBytes(
        string texto,
        Dictionary<char, string> dicionarioCodigoCaractere
    )
    {
        var arquivoCompactadoBytes = new List<byte>();

        int bitCount = 0;    // quantos bits já usamos (0 a 7)
        byte byteAtual = 0;  // byte sendo preenchido

        foreach (char caractere in texto)
        {
            if (!dicionarioCodigoCaractere.TryGetValue(caractere, out string codigo))
            {
                throw new InvalidOperationException($"Não há código para '{caractere}'.");
            }

            foreach (char bitChar in codigo)
            {
                // Se for '1', ligamos o bit na posição correta (7 - bitCount)
                // Se for '0', não precisamos fazer nada, pois o byte começou zerado
                if (bitChar == '1')
                {
                    byteAtual |= (byte)(1 << (7 - bitCount));
                }
                
                bitCount++;

                // Se encheu o byte (0 a 7 preenchidos), grava e reseta
                if (bitCount == 8)
                {
                    arquivoCompactadoBytes.Add(byteAtual);
                    byteAtual = 0;
                    bitCount = 0;
                }
            }
        }

        // Se sobrou algum bit incompleto no final
        if (bitCount > 0)
        {
            arquivoCompactadoBytes.Add(byteAtual);
        }

        int ultimosBitsValidos = bitCount == 0 ? 0 : bitCount;

        return (arquivoCompactadoBytes.ToArray(), ultimosBitsValidos);
    }
}