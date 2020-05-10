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

        public String converterValor(double valor)
        {
            return valor.ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}
