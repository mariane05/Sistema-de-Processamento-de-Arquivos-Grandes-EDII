namespace Compressao;

public static class ConstruirArvore
{
    public class NoArvore
    {
        public char? Simbolo { get; set; }
        public long Frequencia { get; set; }

        public NoArvore? Esquerda { get; set; }
        public NoArvore? Direita  { get; set; }
    }

    public static class ArvoreBuilder
    {
        public static NoArvore BuildArvore(Dictionary<char, long> dicionarioFrequencia)
        {
            if (dicionarioFrequencia == null || dicionarioFrequencia.Count == 0)
                throw new ArgumentException("Dicionário de frequências vazio.");

            if (dicionarioFrequencia.Count == 1)
            {
                var kvp = dicionarioFrequencia.First();
                return new NoArvore
                {
                    Simbolo    = kvp.Key,
                    Frequencia = kvp.Value
                };
            }

            var nodes = dicionarioFrequencia
                .OrderBy(k => k.Value)
                .ThenBy(k => k.Key)
                .Select(k => new NoArvore
                {
                    Simbolo    = k.Key,
                    Frequencia = k.Value
                })
                .ToList();

            while (nodes.Count > 1)
            {
                var left  = nodes[0];
                var right = nodes[1];
                nodes.RemoveAt(0);
                nodes.RemoveAt(0);

                var parent = new NoArvore
                {
                    Simbolo    = null,
                    Frequencia = left.Frequencia + right.Frequencia,
                    Esquerda   = left,
                    Direita    = right
                };

                int idx = nodes.FindIndex(n =>
                    n.Frequencia > parent.Frequencia ||
                    (n.Frequencia == parent.Frequencia &&
                     GetMinSymbol(n) > GetMinSymbol(parent))
                );

                if (idx < 0)
                    nodes.Add(parent);
                else
                    nodes.Insert(idx, parent);
            }

            return nodes[0];
        }

        private static char GetMinSymbol(NoArvore node)
        {
            if (node.Simbolo.HasValue)
                return node.Simbolo.Value;

            char left  = GetMinSymbol(node.Esquerda!);
            char right = GetMinSymbol(node.Direita!);
            return left < right ? left : right;
        }
    }
}
