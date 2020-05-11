using System;
using System.Collections.Generic;
using System.Text;

namespace xadrez_console.tabuleiro
{
    class TabuleiroException : Exception
    {

        public TabuleiroException(string mensagem) : base(mensagem)
        {
        }

    }
}
