using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Transacao
    {
        private DateTime dataTransacao;
        private string operacao;
        private double valor;

        public Transacao(DateTime dataTransacao, string operacao, double valor)
        {
            this.dataTransacao = dataTransacao;
            this.operacao = operacao;
            this.valor = valor;
        }

        public DateTime DataTransacao { get => dataTransacao; }
        public string Operacao { get => operacao; }
        public double Valor { get => valor; }
    }
}