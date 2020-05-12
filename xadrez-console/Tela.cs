using System;
using System.Collections.Generic;
using xadrez_console.tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {

            ImprimirTabuleiro(partida.Tabuleiro);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();

            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.WriteLine("XEQUE!");
                }

                Console.WriteLine();
                Console.Write("Origem: ");
                Posicao origem = LerPosicaoXadrez().ToPosicao();
                partida.ValidarPosicaoOrigem(origem);

                bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();

                Console.Clear();
                ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

                Console.WriteLine("Turno: " + partida.Turno);
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                Console.WriteLine();
                Console.Write("Destino: ");
                Posicao destino = LerPosicaoXadrez().ToPosicao();
                partida.ValidarPosicaoDestino(origem, destino);

                partida.RealizaJogada(origem, destino);
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
            }
        }

        private static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();

            Console.Write("Pretas:  ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        private static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        private static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int l = 0; l < tabuleiro.Linhas; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Colunas; c++)
                {
                    ImprimirPeca(tabuleiro.Peca(l, c));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h ");
            Console.WriteLine();
        }

        private static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int l = 0; l < tabuleiro.Linhas; l++)
            {
                Console.Write(8 - l + " ");
                for (int c = 0; c < tabuleiro.Colunas; c++)
                {
                    if (posicoesPossiveis[l, c])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    ImprimirPeca(tabuleiro.Peca(l, c));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h ");
            Console.WriteLine();
        }

        private static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        private static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

    }
}
