using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.WindowsRuntime;
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
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            this.Tabuleiro = new Tabuleiro(8, 8);
            this.Turno = 1;
            this.JogadorAtual = Cor.Branca;
            this.Terminada = false;
            this.Pecas = new HashSet<Peca>();
            this.Capturadas = new HashSet<Peca>();
            this.Xeque = false;
            ColocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = this.Tabuleiro.RetirarPeca(origem);
            p.IncrementarQtMovimentos();
            Peca pecaCapturada =  this.Tabuleiro.RetirarPeca(destino);
            this.Tabuleiro.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                this.Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = this.Tabuleiro.RetirarPeca(destino);
            p.DecrementarQtMovimentos();
            if(pecaCapturada != null)
            {
                this.Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            this.Tabuleiro.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                this.Xeque = true;
            }
            else
            {
                this.Xeque = false;
            }

            if (TesteXequemate(Adversaria(JogadorAtual)))
            {
                this.Terminada = true;
            }
            else
            {
                this.Turno++;
                MudaJogador();
            }
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
            if (!this.Tabuleiro.Peca(origem).MovimentoPossivel(destino))
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

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Nã tem rei da cor " + cor + " no tabuleiro.");
            }

            foreach(Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if(!EstaEmXeque(cor))
            {
                return false;
            }

            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int l = 0; l < this.Tabuleiro.Linhas; l++)
                {
                    for (int c = 0; c < this.Tabuleiro.Colunas; c++)
                    {
                        if (mat[l, c])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(l, c);
                            Peca pecaCapturada = ExecutaMovimento(origem, new Posicao(l, c));
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            this.Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            this.Pecas.Add(peca);
        }


        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(this.Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(this.Tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(this.Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(this.Tabuleiro, Cor.Preta));
        }

    }
}
