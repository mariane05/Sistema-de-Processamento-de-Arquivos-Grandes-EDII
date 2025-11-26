using static Compressao.ConstruirArvore;

namespace Compressao;

// public class NoArvoreDescompressao
// {
//     public byte CaractereEmByte;
//     public NoArvoreDescompressao? Esquerda;
//     public NoArvoreDescompressao? Direita;

//     public bool EstaNoFimDaArvore => Esquerda == null && Direita == null;
// }

public static class Descompressao
{

    public static void Descomprimir (string caminhoArquivoCompactado, string nomeArquivoDescompactado)
    {
        byte[] bytesDoArquivoCompactado = File.ReadAllBytes(caminhoArquivoCompactado);
        int index = 0;


        //Lê a quantidade de caracteres do header
        byte quantidadeChars = bytesDoArquivoCompactado[index];
        
        //pula pra ler a árvore no cabeçalho
        index += 1;

        Dictionary<char, long> dicionarioCaractereQuantidade = new();

        for (int i = 0; i < quantidadeChars; i++ )
        {
            //Lê os bytes de um caractere, que está em utf16
            ushort codigoChar = BitConverter.ToUInt16(bytesDoArquivoCompactado, index);
            index += 2; //pula p/ prox seção do header

            //lê os bytes de frequência para aquele caractere
            long frequencia = BitConverter.ToInt64(bytesDoArquivoCompactado, index);
            index += 8; //pula p/ prox seção do header

            dicionarioCaractereQuantidade.Add((char)codigoChar, frequencia);
        }

        // NoArvoreDescompressao raiz = RecriaNosArvoreRecursivamente(header, ref posicao);
        // ReconstruirArvore(dicionarioCaractereQuantidade);


        var raiz = ArvoreBuilder.BuildArvore(dicionarioCaractereQuantidade);

        long numeroBitsValidos = BitConverter.ToInt64(bytesDoArquivoCompactado, index);
        index += 4;

        ///? manipulação dos dados comprimidos. Como funciona isso?
        long bytesComprimidos = bytesDoArquivoCompactado.Length - index;
        byte[] comprimidos = new byte[bytesComprimidos];
        Array.Copy(bytesDoArquivoCompactado, index,comprimidos, 0,  bytesComprimidos);

        using var arquivoDescompactado = new FileStream(nomeArquivoDescompactado, FileMode.Create, FileAccess.Write);


        NoArvore noArvoreAtual = raiz;
        if(raiz.EstaNoFimDaArvore)
        {
            // arquivo com um único caractere repetido
            for (int i = 0; i < numeroBitsValidos; i++)
                arquivoDescompactado.WriteByte((byte)raiz.Simbolo.Value);

            return;
        }


        int bitsLidos = 0;
        for(int i = 0; i < comprimidos.Length && bitsLidos < numeroBitsValidos; i++)
        {
            byte byteAtualmenteLido = comprimidos[i];
            for (int bit = 7; bit >= 0 && bitsLidos < numeroBitsValidos; bit --)
            {
                int valorBit = (byteAtualmenteLido >> bit) & 1;
                bitsLidos++;

                /// ?????
                noArvoreAtual = (valorBit == 0) ? noArvoreAtual.Esquerda : noArvoreAtual.Direita;

                if(noArvoreAtual == null) throw new Exception("Erro. A árvore foi reconstruída incorretamente.");

                if (noArvoreAtual.EstaNoFimDaArvore)
                {
                    arquivoDescompactado.WriteByte((byte)noArvoreAtual.Simbolo.Value);
                    noArvoreAtual = raiz;
                }
            }

        }

        Console.WriteLine("Arquivo descomprimido com sucesso!");

    }


}