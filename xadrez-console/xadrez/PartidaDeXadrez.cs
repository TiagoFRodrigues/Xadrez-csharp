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
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;
            this.Pecas = new HashSet<Peca>();
            this.Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtMovimentos();
            Peca pecaCapturada =  this.Tabuleiro.RetirarPeca(destino);
            this.Tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                this.Capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {

            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in this.Capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in this.Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            this.Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            this.Pecas.Add(peca);
        }


        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(this.Tabuleiro, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(this.Tabuleiro, Cor.Preta));
        }

    }
}
