using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CxMasterPlus
{
    public class Tela
    {
        public void imprimirMensagem(String mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public void imprimirMensagem(String mensagem, params string[] args)
        {
            Console.WriteLine(string.Format(mensagem, args[0]));
        }

        public String converterValor(double valor)
        {
            return valor.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}
