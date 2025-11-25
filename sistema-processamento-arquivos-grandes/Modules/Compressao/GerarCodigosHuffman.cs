namespace Compressao

//TODO: DEBUGAR, ESTUDAR E REESCREVER
{   

    public static class GerarCodigosHuffman
    {
        public static Dictionary<char, string> GerarCodigos(ConstruirArvoreHuffman.NoArvoreHuffman raiz)
        {
            var mapa = new Dictionary<char, string>();
            GerarCodigosRec(raiz, "", mapa);
            return mapa;
        }

        private static void GerarCodigosRec(ConstruirArvoreHuffman.NoArvoreHuffman no, string prefixo, Dictionary<char, string> mapa)
        {
            if (no == null)
                return;

            // folha: símbolo encontrado
            if (no.Simbolo.HasValue)
            {
                mapa[no.Simbolo.Value] = prefixo;
                return;
            }

            GerarCodigosRec(no.Esquerda, prefixo + "0", mapa);
            GerarCodigosRec(no.Direita, prefixo + "1", mapa);
        }
    }
}