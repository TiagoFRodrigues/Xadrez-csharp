using System;
using System.Collections.Generic;
using System.Text;
using xadrez_console.tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int l =0; l < tabuleiro.Linhas; l++)
            {

                Console.Write(8 - l + " ");

                for (int c = 0; c < tabuleiro.Colunas; c++)
                {
                    if (tabuleiro.Peca(l,c) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        ImprimirPeca(tabuleiro.Peca(l, c));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h ");
        }

        public static void ImprimirPeca(Peca peca)
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

        }

    }
}
