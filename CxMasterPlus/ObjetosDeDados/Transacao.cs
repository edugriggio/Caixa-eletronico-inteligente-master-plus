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
        private double? vrTotalEmprestimo;
        private int? nrParcela;
        private int? nrTotalParcelas;

        public Transacao(DateTime dataTransacao, string operacao, double valor, int? nrParcela, double? vrTotalEmprestimo, int? nrTotalParcelas)
        {
            this.dataTransacao = dataTransacao;
            this.operacao = operacao;
            this.valor = valor;
            this.vrTotalEmprestimo = vrTotalEmprestimo;
            this.nrParcela = nrParcela;
            this.nrTotalParcelas = nrTotalParcelas;
        }

        public DateTime DataTransacao { get => dataTransacao; }
        public string Operacao { get => operacao; }
        public double Valor { get => valor; }
        public double? VrTotalEmprestimo { get => vrTotalEmprestimo; }
        public int? NrParcela { get => nrParcela; }
        public int? NrTotalParcelas { get => nrTotalParcelas; }
    }
}