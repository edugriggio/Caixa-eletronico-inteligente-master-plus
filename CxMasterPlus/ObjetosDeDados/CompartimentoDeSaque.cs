using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class CompartimentoDeSaque
    {
        private int quantiaInicial = 300;
        private int count;

        public CompartimentoDeSaque()
        {
            count = quantiaInicial;
        }

        public void DispensarDinheiro(int qtd)
        {
            int qtdNotas = qtd / 20;
            count -= qtdNotas;
        }

        public Boolean TemSaldoSuficiente(int qtd)
        {
            int qtdNotas = qtd / 20;
            return count >= qtdNotas;
        }

        public double ValorEmCaixa()
        {
            return count * 20;
        }

        public void AdicionarNotas(double qtd)
        {
            count += (int)(qtd * 0.8) / 20;
        }
    }
}
