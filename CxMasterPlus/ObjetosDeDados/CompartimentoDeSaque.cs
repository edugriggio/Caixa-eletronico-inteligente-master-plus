using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class CompartimentoDeSaque
    {
        private int quantiaInicial = 10;
        private int count;

        public CompartimentoDeSaque()
        {
            count = quantiaInicial;
        }

        public void dispensarDinheiro(int qtd)
        {
            int qtdNotas = qtd / 20;
            count -= qtdNotas;
        }

        public Boolean temSaldoSuficiente(int qtd)
        {
            int qtdNotas = qtd / 20;
            return count >= qtdNotas;
        }

        public double valorEmCaixa()
        {
            return count * 20;
        }

        public void adicionarNotas(double qtd)
        {
            count += (int)(qtd * 0.8) / 20;
        }
    }
}
