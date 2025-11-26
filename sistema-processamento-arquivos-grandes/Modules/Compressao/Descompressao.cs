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
            index += 2;

            //lê os bytes de frequência para aquele caractere
            long frequencia = BitConverter.ToInt64(bytesDoArquivoCompactado, index);
            index += 8;

            //TODO: CONTINUAR

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


        // int posicao = 0;                        ///? Só pra ter certeza, mas oq faz o ref? é um ponteiro?
        // NoArvoreDescompressao raiz = RecriaNosArvoreRecursivamente(header, ref posicao);

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
                int valorBit = (bit >> bit) & 1;
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
                                                            //são real os dados comprimidos?
    private static NoArvoreDescompressao RecriaNosArvoreRecursivamente(byte[] dadosComprimidos, ref int posicao)
    {
        var dadoSendoLido = dadosComprimidos[posicao++];
        //tipo doq?
        char tipo = (char)dadoSendoLido;
        NoArvoreDescompressao noDeDescompressao = new();

        if (tipo == 'L') ///? donde saiu esse L?
        {
            noDeDescompressao.CaractereEmByte = dadoSendoLido;
        } else //I      ///? donde saiu esse I?
        {
            noDeDescompressao.Esquerda = RecriaNosArvoreRecursivamente(dadosComprimidos, ref posicao);
            noDeDescompressao.Direita = RecriaNosArvoreRecursivamente(dadosComprimidos, ref posicao);
        }

        return noDeDescompressao;
    }
}