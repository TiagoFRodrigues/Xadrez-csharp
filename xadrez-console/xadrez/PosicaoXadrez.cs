
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.Coluna = coluna;
            this.Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - this.Linha, this.Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + this.Coluna + this.Linha;
        }

    }
}
