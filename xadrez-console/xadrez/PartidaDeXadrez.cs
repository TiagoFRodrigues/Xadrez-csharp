using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tabuleiro { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            this.tabuleiro = new Tabuleiro(8, 8);
            this.turno = 1;
            this.jogadorAtual = Cor.Branca;
            this.Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.tabuleiro.RetirarPeca(origem);
            p.IncrementarQtMovimentos();
            Peca pecaCapturada =  this.tabuleiro.RetirarPeca(destino);
            this.tabuleiro.ColocarPeca(p, destino);



        }

        private void ColocarPecas()
        {
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            this.tabuleiro.ColocarPeca(new Rei(this.tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            this.tabuleiro.ColocarPeca(new Torre(this.tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            this.tabuleiro.ColocarPeca(new Rei(this.tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }

    }
}
