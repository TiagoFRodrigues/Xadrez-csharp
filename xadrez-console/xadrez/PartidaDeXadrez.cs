using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtMovimentos();
            Peca pecaCapturada =  this.Tabuleiro.RetirarPeca(destino);
            this.Tabuleiro.ColocarPeca(p, destino);



        }

        private void ColocarPecas()
        {
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Rei(this.Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Torre(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            this.Tabuleiro.ColocarPeca(new Rei(this.Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }

    }
}
