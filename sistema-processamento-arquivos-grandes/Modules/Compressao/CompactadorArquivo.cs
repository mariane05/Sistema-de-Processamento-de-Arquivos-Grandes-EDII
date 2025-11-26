using System.Text;

namespace Compressao;

//TODO: USAR ESTRUTURA LÓGICA DE CHAMADA DE MÉTODOS NO ARQUIVO COMPRESSAO APP
public static class CompactadorArquivo
{
     public static void Compactar(string caminhoEntrada, string caminhoSaida)
        {
            // 1. Ler o conteúdo (por enquanto, completo)
            string texto = File.ReadAllText(caminhoEntrada, Encoding.UTF8);

            // 2. Frequências
            var freq = HuffmanHelper.ConstruirFrequencias(caminhoEntrada);

            // 3. Árvore
            var raiz = HuffmanBuilder.ConstruirArvore(freq);

            // 4. Códigos
            var codigos = HuffmanCodes.GerarCodigos(raiz);

            // 5. Converter para bytes
            var (bytes, bitsUltimo) = HuffmanBitWriter.GerarBytesCompactados(texto, codigos);

            // 6. Escrever arquivo
            using var fs = new FileStream(caminhoSaida, FileMode.Create);
            using var writer = new BinaryWriter(fs);

            // cabeçalho
            HuffmanArquivo.EscreverCabecalho(writer, freq);

            // bytes comprimidos
            writer.Write(bytes);

            // quantos bits do último byte são válidos
            writer.Write((byte)bitsUltimo);
        }
    }
}