using System.Text;
using static Compressao.ConstruirArvore;

namespace Compressao;

public static class Descompressao
{
    public static void Descomprimir(string caminhoArquivoCompactado, string caminhoArquivoDescompactado)
    {
        if (!File.Exists(caminhoArquivoCompactado))
        {
            throw new FileNotFoundException($"Arquivo compactado não encontrado: {caminhoArquivoCompactado}");
        }

        Console.WriteLine($"Iniciando descompressão de: {caminhoArquivoCompactado}");

        using FileStream fsEntrada = new FileStream(caminhoArquivoCompactado, FileMode.Open, FileAccess.Read);
        using BinaryReader reader = new BinaryReader(fsEntrada, Encoding.UTF8);
        using FileStream fsSaida = new FileStream(caminhoArquivoDescompactado, FileMode.Create, FileAccess.Write);
        using StreamWriter writer = new StreamWriter(fsSaida, Encoding.UTF8);

       
        long tamanhoOriginalTotal = reader.ReadInt64();
        int numeroBlocos = reader.ReadInt32();

        Console.WriteLine($"Tamanho original total: {tamanhoOriginalTotal} bytes");
        Console.WriteLine($"Número de blocos: {numeroBlocos}");

        var blocos = new List<EntradaIndiceBlocos>(numeroBlocos);
        for (int i = 0; i < numeroBlocos; i++)
        {
            var bloco = new EntradaIndiceBlocos
            {
                OffsetOriginal = reader.ReadInt64(),
                TamanhoOriginal = reader.ReadInt32(),
                OffsetComprimido = reader.ReadInt64(),
                TamanhoComprimido = reader.ReadInt32()
            };
            blocos.Add(bloco);
        }

        //descompressao dos blocos
        for (int i = 0; i < blocos.Count; i++)
        {
            var bloco = blocos[i];
            Console.WriteLine($"Descomprimindo bloco {i + 1}/{numeroBlocos}...");
            fsEntrada.Seek(bloco.OffsetComprimido, SeekOrigin.Begin);

            string textoDescomprimido = DescomprimirBlocoHuffman(reader, bloco);

            writer.Write(textoDescomprimido);
        }

        Console.WriteLine("Descompressão concluída com sucesso!");
    }

    private static string DescomprimirBlocoHuffman(BinaryReader reader, EntradaIndiceBlocos bloco)
    {
        long posicaoInicial = reader.BaseStream.Position;

        byte quantidadeSimbolos = reader.ReadByte();
        var dicionarioFrequencias = new Dictionary<char, long>();

        for (int i = 0; i < quantidadeSimbolos; i++)
        {
            ushort simboloCode = reader.ReadUInt16();
            char simbolo = (char)simboloCode;
            long frequencia = reader.ReadInt64();
            dicionarioFrequencias[simbolo] = frequencia;
        }

        var raiz = ArvoreBuilder.BuildArvore(dicionarioFrequencias);

        long totalCaracteres = 0;
        foreach (var freq in dicionarioFrequencias.Values)
        {
            totalCaracteres += freq;
        }

        long posicaoAtual = reader.BaseStream.Position;
        long bytesRestantes = (posicaoInicial + bloco.TamanhoComprimido) - posicaoAtual;
        long bytesDadosComprimidos = bytesRestantes - 1;
        
        if (bytesDadosComprimidos < 0)
        {
            throw new InvalidDataException("Tamanho de dados comprimidos inválido.");
        }

        byte[] dadosComprimidos = reader.ReadBytes((int)bytesDadosComprimidos);
        byte ultimosBitsValidos = reader.ReadByte();
        var resultado = new StringBuilder((int)totalCaracteres);

        if (raiz.EstaNoFimDaArvore)
        {
            char simboloUnico = raiz.Simbolo!.Value;
            for (long i = 0; i < totalCaracteres; i++)
            {
                resultado.Append(simboloUnico);
            }
            return resultado.ToString();
        }

        var noAtual = raiz;
        long caracteresDecodificados = 0;

        for (int byteIndex = 0; byteIndex < dadosComprimidos.Length && caracteresDecodificados < totalCaracteres; byteIndex++)
        {
            byte byteAtual = dadosComprimidos[byteIndex];
            
            int bitsParaProcessar = 8;
            if (byteIndex == dadosComprimidos.Length - 1 && ultimosBitsValidos > 0)
            {
                bitsParaProcessar = ultimosBitsValidos;
            }

            for (int bitIndex = 7; bitIndex >= (8 - bitsParaProcessar) && caracteresDecodificados < totalCaracteres; bitIndex--)
            {
                int bit = (byteAtual >> bitIndex) & 1;
                noAtual = (bit == 0) ? noAtual.Esquerda : noAtual.Direita;

                if (noAtual == null)
                {
                    throw new InvalidDataException("Erro na decodificação: nó nulo encontrado.");
                }

                if (noAtual.EstaNoFimDaArvore)
                {
                    resultado.Append(noAtual.Simbolo!.Value);
                    caracteresDecodificados++;
                    noAtual = raiz;
                }
            }
        }

        if (caracteresDecodificados != totalCaracteres)
        {
            Console.WriteLine($"[AVISO] Esperava decodificar {totalCaracteres} caracteres, mas decodificou {caracteresDecodificados}");
        }

        return resultado.ToString();
    }


}