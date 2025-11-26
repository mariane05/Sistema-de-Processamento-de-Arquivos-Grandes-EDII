using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Compressao;
using BuscaArquivoGrande;

namespace BuscaArquivoCompactado
{
    public class EntradaIndiceBlocos
    {
        public long OffsetOriginal { get; set; }      // posição no arquivo original
        public int TamanhoOriginal { get; set; }      // tamanho descomprimido do bloco
        public long OffsetComprimido { get; set; }    // posição no arquivo compactado
        public int TamanhoComprimido { get; set; }    // tamanho em bytes desse bloco compactado
    }

    public class BuscarArquivoComprimidoApp
    {
        public static void InitApp(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Uso: meu_programa buscar_compactado <arquivo_compactado> \"<substring>\"");
                return;
            }

            string caminhoArquivoComprimido = args[1];
            string padraoBusca = args[2];

            int count = Debug_01(caminhoArquivoComprimido, padraoBusca);
            Console.WriteLine($"[DEBUG_01] Ocorrências totais do padrão '{padraoBusca}': {count}");

            List<long> resultados = BuscarNoArquivoComprimido(
                caminhoArquivoComprimido,
                padraoBusca
            );

            if (resultados.Count > 0)
            {
                Console.WriteLine("Padrão encontrado nas posições (arquivo original):");
                foreach (long pos in resultados)
                {
                    Console.WriteLine(pos);
                }
            }
            else
            {
                Console.WriteLine("Padrão não encontrado no arquivo original.");
            }
        }


        public static List<long> BuscarNoArquivoComprimido(string caminhoArquivo, string padrao)
        {
            var resultadosGlobais = new List<long>();

            using FileStream fs = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read);
            using BinaryReader br = new BinaryReader(fs, Encoding.UTF8);

            long tamanhoOriginalTotal;
            List<EntradaIndiceBlocos> blocos = LerCabecalhoEIndice(br, out tamanhoOriginalTotal);

            if (blocos.Count == 0)
            {
                return resultadosGlobais;
            }

            int tamanhoPadrao = padrao.Length;
            if (tamanhoPadrao == 0)
            {
                return resultadosGlobais;
            }

            string sufixoAnterior = string.Empty;

            foreach (var bloco in blocos)
            {
                string textoBloco = DescomprimirBlocoHuffman(br, bloco);

                int tamanhoSufixo = Math.Min(tamanhoPadrao - 1, sufixoAnterior.Length);
                string sufixo = tamanhoSufixo > 0
                    ? sufixoAnterior.Substring(sufixoAnterior.Length - tamanhoSufixo, tamanhoSufixo)
                    : string.Empty;

                string janela = sufixo + textoBloco;

                // Reutiliza o algoritmo de busca da Etapa 2
                List<int> ocorrenciasLocal = BuscaArquivoGrandeApp.Buscar(janela, padrao);

                foreach (int posLocal in ocorrenciasLocal)
                    {
                        long posGlobal = (bloco.OffsetOriginal - tamanhoSufixo) + posLocal;
                        resultadosGlobais.Add(posGlobal);
                    }


                sufixoAnterior = textoBloco;
            }

            return resultadosGlobais;
        }

        public static int Debug_01(string caminhoArquivo, string padrao)
        {
            using FileStream fs = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read);
            using BinaryReader br = new BinaryReader(fs, Encoding.UTF8);

            long tamanhoOriginalTotal;
            // O LerCabecalhoEIndice deve estar disponível na classe ou ter sido movido.
            // Presumindo que ele está na classe ou disponível.
            List<EntradaIndiceBlocos> blocos = LerCabecalhoEIndice(br, out tamanhoOriginalTotal);

            var sbGlobal = new StringBuilder();

            // 1. Descomprimir e Reconstruir o Texto Completo
            for (int i = 0; i < blocos.Count; i++)
            {
                var bloco = blocos[i];
                string textoBloco = DescomprimirBlocoHuffman(br, bloco);
                sbGlobal.Append(textoBloco);
                
                if (textoBloco.Length != bloco.TamanhoOriginal)
                {
                    Console.WriteLine($"[AVISO] Bloco {i} descompactado com {textoBloco.Length} chars, mas índice esperava {bloco.TamanhoOriginal}.");
                }
            }

            string textoReconstruido = sbGlobal.ToString();

            // 2. Realizar a Busca e Obter Apenas a Contagem
            // A função BuscaArquivoGrandeApp.Buscar deve retornar List<int> com as posições.
            List<int> ocorrencias = BuscaArquivoGrandeApp.Buscar(textoReconstruido, padrao);
            
            // Retorna a contagem total
            return ocorrencias.Count;
        }


        private static List<EntradaIndiceBlocos> LerCabecalhoEIndice(BinaryReader br, out long tamanhoOriginalTotal)
        {
            // bate com o que a CompressaoApp escreveu
            tamanhoOriginalTotal = br.ReadInt64();
            int numeroBlocos = br.ReadInt32();

            var blocos = new List<EntradaIndiceBlocos>(numeroBlocos);

            for (int i = 0; i < numeroBlocos; i++)
            {
                var bloco = new EntradaIndiceBlocos
                {
                    OffsetOriginal = br.ReadInt64(),
                    TamanhoOriginal = br.ReadInt32(),
                    OffsetComprimido = br.ReadInt64(),
                    TamanhoComprimido = br.ReadInt32()
                };

                blocos.Add(bloco);
            }

            return blocos;
        }

        private static string DescomprimirBlocoHuffman(BinaryReader br, EntradaIndiceBlocos bloco)
        {
            // lê exatamente os bytes do bloco compactado para um buffer
            br.BaseStream.Seek(bloco.OffsetComprimido, SeekOrigin.Begin);
            byte[] dadosBloco = br.ReadBytes(bloco.TamanhoComprimido);

            using MemoryStream ms = new MemoryStream(dadosBloco);
            using BinaryReader brBloco = new BinaryReader(ms, Encoding.UTF8);

            // mesmo formato que usamos na compressão de cada bloco
            byte quantidadeSimbolos = brBloco.ReadByte();
            var dicionarioFrequencias = new Dictionary<char, long>();

            for (int i = 0; i < quantidadeSimbolos; i++)
            {
                // CORREÇÃO: Ler ushort (2 bytes) em vez de ReadByte
                ushort simboloShort = brBloco.ReadUInt16(); 
                char simbolo = (char)simboloShort;

                long frequencia = brBloco.ReadInt64();

                dicionarioFrequencias[simbolo] = frequencia;
            }

            var noRaiz = ConstruirArvore.ArvoreBuilder.BuildArvore(dicionarioFrequencias);

            // soma das frequências = quantidade de caracteres originais no bloco
            long totalCaracteres = 0;
            foreach (var freq in dicionarioFrequencias.Values)
            {
                totalCaracteres += freq;
            }

            long posDadosInicio = ms.Position;
            long tamanhoBloco = ms.Length;

            // último byte é o "ultimosBits", mas vamos ignorar isso para a lógica de parada
            long posUltimosBits = tamanhoBloco - 1;
            ms.Seek(posUltimosBits, SeekOrigin.Begin);
            byte _ultimosBits = brBloco.ReadByte(); // lido, mas não usado

            long tamanhoDadosComprimidos = posUltimosBits - posDadosInicio;
            if (tamanhoDadosComprimidos < 0)
            {
                throw new InvalidDataException("Tamanho de dados comprimidos inválido no bloco.");
            }

            ms.Seek(posDadosInicio, SeekOrigin.Begin);
            byte[] dadosComprimidos = brBloco.ReadBytes((int)tamanhoDadosComprimidos);

            // decodificação dos bits com a árvore de Huffman
            var sb = new StringBuilder();
            var noAtual = noRaiz;
            long caracteresDecodificados = 0;

            for (int i = 0; i < dadosComprimidos.Length && caracteresDecodificados < totalCaracteres; i++)
            {
                byte b = dadosComprimidos[i];

                // sempre processa os 8 bits; vamos parar pelo total de caracteres, não pelos bits
                for (int bitIndex = 7; bitIndex >= 0 && caracteresDecodificados < totalCaracteres; bitIndex--)
                {
                    int bit = (b >> bitIndex) & 1;

                    noAtual = (bit == 0) ? noAtual.Esquerda : noAtual.Direita;

                    if (noAtual == null)
                    {
                        throw new InvalidDataException("Erro de decodificação Huffman: nulo.");
                    }

                    if (noAtual.Simbolo.HasValue)
                    {
                        sb.Append(noAtual.Simbolo.Value);
                        caracteresDecodificados++;
                        noAtual = noRaiz;
                    }
                }
            }

            if (caracteresDecodificados != totalCaracteres)
            {
                Console.WriteLine($"[WARN] Bloco descompactado com {caracteresDecodificados} caracteres, " +
                                $"mas dicionário indicava {totalCaracteres}.");
            }

            return sb.ToString();
        }
    }
}
