using static Compressao.ConstruirArvore;

namespace Compressao;

//TODO: DEBUGAR, ESTUDAR E REESCREVER

public static class GerarCodigosBinarios
{
    
    public static Dictionary<char, string> gerarTodosOsCodigos(NoArvore noRaiz)
    {
         if (noRaiz == null)
            throw new ArgumentNullException(nameof(noRaiz));

        var dicCodigoCaractere = new Dictionary<char, string>(); //vai ser o negócio do caractere/código

         if (noRaiz.Simbolo.HasValue && noRaiz.Esquerda == null && noRaiz.Direita == null) // caso especial: só um símbolo na árvore
        {
            dicCodigoCaractere[noRaiz.Simbolo.Value] = "0";
            return dicCodigoCaractere;
        }
        
        gerarCodigosPorNoRecursivo(noRaiz, "",dicCodigoCaractere); 
        return dicCodigoCaractere;
    }


    private static void gerarCodigosPorNoRecursivo(NoArvore no, string prefixo, Dictionary<char, string> dicCodigoCaractere)
    {
        if (no == null) return;

        // nó folha == nó com valor?
        if (no.Simbolo.HasValue && no.Esquerda == null && no.Direita == null)
        {
            dicCodigoCaractere[no.Simbolo.Value] = prefixo.Length > 0 ? prefixo : "0"; // se o prefixo estiver vazio (árvore com um símbolo só), força "0"
            return;
        }

        gerarCodigosPorNoRecursivo(no.Esquerda, prefixo + "0", dicCodigoCaractere);
        gerarCodigosPorNoRecursivo(no.Direita, prefixo + "1", dicCodigoCaractere);

    }
   
}