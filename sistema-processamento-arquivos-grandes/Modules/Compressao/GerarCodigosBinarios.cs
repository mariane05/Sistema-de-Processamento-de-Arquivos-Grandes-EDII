using static Compressao.ConstruirArvore;

namespace Compressao;

//TODO: DEBUGAR, ESTUDAR E REESCREVER

public static class GerarCodigosBinarios
{
    
    public static Dictionary<char, string> gerarTodosOsCodigos(NoArvore noRaiz)
    {
        var dicCodigoCaractere = new Dictionary<char, string>(); //vai ser o negócio do caractere/código
        gerarCodigosPorNoRecursivo(noRaiz, "",dicCodigoCaractere);
        
        return dicCodigoCaractere;
    }


    private static void gerarCodigosPorNoRecursivo(NoArvore no, string prefixo, Dictionary<char, string> dicCodigoCaractere)
    {
        if (no == null) return;

        // nó folha == nó com valor?
        if(no.Simbolo.HasValue)
        {
            dicCodigoCaractere[no.Simbolo.Value] = prefixo;
            return;
        }

        gerarCodigosPorNoRecursivo(no.Esquerda, prefixo + "0", dicCodigoCaractere);
        gerarCodigosPorNoRecursivo(no.Direita, prefixo + "0", dicCodigoCaractere);

    }
   
}





