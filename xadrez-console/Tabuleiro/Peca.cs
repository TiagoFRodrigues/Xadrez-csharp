

namespace xadrez_console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.Posicao = null;
            this.Cor = cor;
            this.Tab = tab;
            this.QtMovimentos = 0;
        }

        public abstract bool[,] MovimentosPossiveis();
        

        public void IncrementarQtMovimentos()
        {
            this.QtMovimentos++;
        }
    }
}
