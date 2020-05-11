using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            this.Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (this.Tabuleiro.Peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (this.JogadorAtual != this.Tabuleiro.Peca(posicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é a sua!");
            }
            if (!this.Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!this.Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                this.JogadorAtual = Cor.Preta;
            }
            else
            {
                this.JogadorAtual = Cor.Branca;
            }
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
