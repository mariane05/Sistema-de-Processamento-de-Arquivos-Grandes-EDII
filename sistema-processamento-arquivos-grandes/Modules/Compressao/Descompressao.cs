using static Compressao.ConstruirArvore;

namespace Compressao;

public class NoArvoreDescompressao
{
    public byte CaractereEmByte;
    public NoArvoreDescompressao? Esquerda;
    public NoArvoreDescompressao? Direita;

    public bool EstaNoFimDaArvore => Esquerda == null && Direita == null;
}

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





        ///? não teria que ler para int64?
        // long tamanhoHeader = BitConverter.ToInt64(bytesDoArquivoCompactado, index);
        // Console.WriteLine($"  >>>>>  Tamanho do Cabeçalho lido: {tamanhoHeader}");
        
        //pula p/ próximo byte
        // index += 4;

        // byte[] header = new byte[tamanhoHeader];
        ///? Como funciona esse Array.Copy, quais seus parâmetros e porque ele está sendo usado aqui?
        // Array.Copy(bytesDoArquivoCompactado, index, header, 0, tamanhoHeader);
        
        //avança todo o cabeçalho
        // index += (int)tamanhoHeader;


        int posicao = 0;                        ///? Só pra ter certeza, mas oq faz o ref? é um ponteiro?
        // NoArvoreDescompressao raiz = RecriaNosArvoreRecursivamente(header, ref posicao);
        NoArvoreDescompressao raiz = ReconstruirArvore(dicionarioCaractereQuantidade);

        long numeroBitsValidos = BitConverter.ToInt64(bytesDoArquivoCompactado, index);
        index += 4;

        ///? manipulação dos dados comprimidos. Como funciona isso?
        long bytesComprimidos = bytesDoArquivoCompactado.Length - index;
        byte[] comprimidos = new byte[bytesComprimidos];
        Array.Copy(bytesDoArquivoCompactado, index,comprimidos, 0,  bytesComprimidos);

        using var arquivoDescompactado = new FileStream(nomeArquivoDescompactado, FileMode.Create, FileAccess.Write);


        NoArvoreDescompressao noArvoreAtual = raiz;
        int bitsLidos = 0;


        for (int i = 0; i < comprimidos.Length && bitsLidos < numeroBitsValidos; i++)
        {
            byte byteAtualmenteLido = comprimidos[i];
            for (int bit = 7; bit >= 0 && bitsLidos < numeroBitsValidos; bit --)
            {
                int valorBit = (byteAtualmenteLido >> bit) & 1;
                bitsLidos++;

                /// ?????
                noArvoreAtual = (valorBit == 0) ? noArvoreAtual.Esquerda : noArvoreAtual.Direita;

                if (noArvoreAtual.EstaNoFimDaArvore)
                {
                    arquivoDescompactado.WriteByte(noArvoreAtual.CaractereEmByte);
                    noArvoreAtual = raiz;
                }
            }
        }


        Console.WriteLine("Arquivo descomprimido com sucesso!");

    }

    private static NoArvoreDescompressao ReconstruirArvore(Dictionary<char,long> freq)
{
    var fila = new PriorityQueue<NoArvoreDescompressao, long>();

    foreach (var kv in freq)
    {
        fila.Enqueue(new NoArvoreDescompressao {
            CaractereEmByte = (byte)kv.Key
        }, kv.Value);
    }

    while (fila.Count > 1)
    {
        fila.TryDequeue(out var esq, out var fe1);
        fila.TryDequeue(out var dir, out var fe2);

        var pai = new NoArvoreDescompressao {
            Esquerda = esq,
            Direita = dir
        };

        fila.Enqueue(pai, fe1 + fe2);
    }

    fila.TryDequeue(out var raiz, out _);
    return raiz;
}

}