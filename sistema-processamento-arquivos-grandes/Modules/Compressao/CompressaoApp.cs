using static Compressao.LeituraArquivo;
using static Compressao.DicionarioFrequencia;
using static Compressao.ConstruirArvore.ArvoreBuilder;
using static Compressao.GerarCodigosBinarios;
using static Compressao.CabecalhoArquivo;
using static Compressao.BitWriter;
using System.Text;

namespace Compressao;

public class CompressaoApp
{
    public static void InitApp(string[] args)
    {
        
        string caminhoArquivo = separaCaminhoDoArquivo(args);
        string todoTexto = File.ReadAllText(caminhoArquivo, Encoding.UTF8);

        
        var dicionarioDeFrequencias = construirDicionarioDeFrequências(caminhoArquivo);
        var noRaiz = BuildArvore(dicionarioDeFrequencias);
        var dicionarioDeCodigos = gerarTodosOsCodigos(noRaiz);
        var (todosBytesGerados, ultimosBits) = CompactarEDevolverBytes(todoTexto, dicionarioDeCodigos);

        //TODO:CRIAR MÉTODO PARA LER CAMINHO DE SAÍDA
        using var fs = new FileStream(args[2], FileMode.Create);
        using var writer = new BinaryWriter(fs);

        GerarCabecalho(writer, dicionarioDeFrequencias);

        // bytes comprimidos
        writer.Write(todosBytesGerados);

        // quantos bits do último byte são válidos
        writer.Write((byte)ultimosBits);



        
        Console.WriteLine("Finalizada a Compressao");
    }
    
}