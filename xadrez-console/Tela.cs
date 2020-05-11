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
                for (int c = 0; c < tabuleiro.Colunas; c++)
                {
                    if (tabuleiro.Peca(l,c) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.Peca(l, c) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
