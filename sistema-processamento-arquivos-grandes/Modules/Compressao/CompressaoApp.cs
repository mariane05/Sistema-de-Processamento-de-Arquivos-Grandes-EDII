using System.Text;

namespace Compressao;

public class CompressaoApp
{
    // tamanho do bloco em bytes
    private const int TAMANHO_BLOCO = 64 * 1024;

    public static void InitApp(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Uso: meu_programa compactar <arquivo_original> <arquivo_compactado>");
            return;
        }

        string caminhoArquivoOriginal = args[1];
        string caminhoArquivoCompactado = args[2];

        if (!File.Exists(caminhoArquivoOriginal))
        {
            Console.WriteLine($"Arquivo de entrada '{caminhoArquivoOriginal}' não encontrado.");
            return;
        }

        CompactarEmBlocos(caminhoArquivoOriginal, caminhoArquivoCompactado);

        Console.WriteLine("Finalizada a compressão em blocos com índice.");
    }

    private static void CompactarEmBlocos(string caminhoEntrada, string caminhoSaida)
    {
        using FileStream fsEntrada = new FileStream(caminhoEntrada, FileMode.Open, FileAccess.Read);
        long tamanhoOriginalTotalBytes = fsEntrada.Length;

        // calcula número de blocos em bytes para saber quantos blocos teremos
        int numeroBlocos = (int)((tamanhoOriginalTotalBytes + TAMANHO_BLOCO - 1) / TAMANHO_BLOCO);

        using FileStream fsSaida = new FileStream(caminhoSaida, FileMode.Create, FileAccess.Write);
        using BinaryWriter writer = new BinaryWriter(fsSaida, Encoding.UTF8);

        // 1) escreve cabeçalho geral (tamanho total + número de blocos)
        writer.Write(tamanhoOriginalTotalBytes);
        writer.Write(numeroBlocos);

        // 2) reserva espaço para o índice
        long posInicioIndice = fsSaida.Position;
        int tamanhoEntradaIndice = sizeof(long) + sizeof(int) + sizeof(long) + sizeof(int); // 8+4+8+4 = 24
        long tamanhoIndiceTotal = (long)numeroBlocos * tamanhoEntradaIndice;

        writer.Write(new byte[tamanhoIndiceTotal]); // reserva com zeros

        // 3) processa os blocos
        var indiceBlocos = new EntradaIndiceBlocos[numeroBlocos];

        long offsetOriginalEmChars = 0; // mede em caracteres
        byte[] buffer = new byte[TAMANHO_BLOCO];
        int indice = 0;
        int bytesLidos;
        bool primeiroBloco = true;

        while ((bytesLidos = fsEntrada.Read(buffer, 0, TAMANHO_BLOCO)) > 0)
        {
            // converte os bytes do bloco em string
            string textoBloco = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

            if (primeiroBloco)
            {
                if (textoBloco.Length > 0 && textoBloco[0] == '\uFEFF')
                {
                    textoBloco = textoBloco.Substring(1);
                }
                primeiroBloco = false;
            }

            int tamanhoOriginalBlocoEmChars = textoBloco.Length;

            // dicionário de frequências para o bloco atual
            Dictionary<char, long> dicionarioDeFrequencias = ConstruirDicionarioDeFrequenciasParaTexto(textoBloco);

            // constrói árvore de Huffman
            var noRaiz = ConstruirArvore.ArvoreBuilder.BuildArvore(dicionarioDeFrequencias);

            // códigos binários por caractere
            Dictionary<char, string> dicionarioCodigoCaractere =
                GerarCodigosBinarios.gerarTodosOsCodigos(noRaiz);

            // compacta o bloco em bytes
            var (bytesComprimidos, ultimosBits) =
                BitWriter.CompactarEDevolverBytes(textoBloco, dicionarioCodigoCaractere);

            // posição onde começa este bloco no arquivo compactado
            long offsetComprimido = fsSaida.Position;

            // cabeçalho do bloco (frequências)
            CabecalhoArquivo.GerarCabecalho(writer, dicionarioDeFrequencias);

            // bytes comprimidos
            writer.Write(bytesComprimidos);

            // quantos bits do último byte são válidos
            writer.Write((byte)ultimosBits);

            long posDepoisBloco = fsSaida.Position;
            int tamanhoComprimido = (int)(posDepoisBloco - offsetComprimido);

            indiceBlocos[indice] = new EntradaIndiceBlocos
            {
                OffsetOriginal = offsetOriginalEmChars,        // em caracteres
                TamanhoOriginal = tamanhoOriginalBlocoEmChars, // em caracteres
                OffsetComprimido = offsetComprimido,
                TamanhoComprimido = tamanhoComprimido
            };

            offsetOriginalEmChars += tamanhoOriginalBlocoEmChars;
            indice++;
        }

        // 4) volta no começo do índice e escreve as entradas reais
        fsSaida.Seek(posInicioIndice, SeekOrigin.Begin);

        foreach (var bloco in indiceBlocos)
        {
            writer.Write(bloco.OffsetOriginal);
            writer.Write(bloco.TamanhoOriginal);
            writer.Write(bloco.OffsetComprimido);
            writer.Write(bloco.TamanhoComprimido);
        }
    }


    // mesma ideia do DicionarioFrequencia, mas para um texto já em memória
    private static Dictionary<char, long> ConstruirDicionarioDeFrequenciasParaTexto(string texto)
    {
        var dic = new Dictionary<char, long>();

        foreach (char c in texto)
        {
            if (dic.TryGetValue(c, out long freq))
            {
                dic[c] = freq + 1;
            }
            else
            {
                dic[c] = 1;
            }
        }

        return dic;
    }
}

public class EntradaIndiceBlocos
{
    public long OffsetOriginal { get; set; }      // posição no arquivo original
    public int TamanhoOriginal { get; set; }      // tamanho descomprimido do bloco
    public long OffsetComprimido { get; set; }    // posição no arquivo compactado
    public int TamanhoComprimido { get; set; }    // bytes do bloco compactado (inclui cabeçalho Huffman + dados + ultimosBits)
}
