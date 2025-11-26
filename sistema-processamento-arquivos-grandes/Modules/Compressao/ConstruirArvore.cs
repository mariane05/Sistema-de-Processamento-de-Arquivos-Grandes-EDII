namespace Compressao;

public static class ConstruirArvore
{
    public class NoArvore
    {
        public char? Simbolo {get; set;} //todo: descobrir o que {get; set;} faz
        public long Frequencia {get; set;}

        public NoArvore? Esquerda {get; set;} //Pq tem que começar com UpperCase?
        public NoArvore? Direita  {get; set;}
    }



    public static class ArvoreBuilder
    {
        public static NoArvore BuildArvore(Dictionary<char, long> frequenciaDic)
        {
            //why?
            var filaPrioridades = new PriorityQueue<NoArvore, long>();

            //craindo um nó para cada símbolo
            foreach (var item in frequenciaDic)
            {
                filaPrioridades.Enqueue(
                    new NoArvore
                    {
                        Simbolo = item.Key,
                        Frequencia = item.Value
                    }, 
                    item.Value //oq é isso?
                );
            }


            //contar até restar 1           O que diabos isso quer dizer? pq?
            while(filaPrioridades.Count > 1)
            {
                var noFilhoEsquerda = filaPrioridades.Dequeue();
                var noFilhoDireita = filaPrioridades.Dequeue();

                var frequenciaTotalDosFilhos = noFilhoEsquerda.Frequencia + noFilhoDireita.Frequencia;

                var noPai = new NoArvore
                {
                    Simbolo = null,
                    Frequencia = frequenciaTotalDosFilhos,
                    Esquerda = noFilhoEsquerda,
                    Direita = noFilhoDireita
                };

                filaPrioridades.Enqueue(noPai, noPai.Frequencia);
            }

            return filaPrioridades.Dequeue(); //retorna a árvore final ... but how?

        }
        
    }
}