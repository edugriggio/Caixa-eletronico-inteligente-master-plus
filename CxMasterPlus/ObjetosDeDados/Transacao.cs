using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Transacao
    {
        private DateTime dataTransacao;
        private DateTime proximaParcela;
        private char isActive;
        private int parcelasRestantes;
        private Operacao operacao;
        private double valor;
        private double taxa;
        private double? vlrTotalEmprestimo;
        private int? nrParcela;
        private int? nrTotalParcelas;
        private int id;

        public DateTime DataTransacao { get => dataTransacao; set => this.dataTransacao = value; }
        public DateTime ProximaParcela { get => proximaParcela; set => this.proximaParcela = value; }
        public int ParcelasRestantes { get => parcelasRestantes; set => this.parcelasRestantes = value; }
        public char IsActive { get => isActive; set => this.isActive = value;  }
        public Operacao Operacao { get => operacao; set => this.operacao = value;  }
        public double Valor { get => valor; set => this.valor = value;  }
        public double Taxa { get => taxa; set => this.taxa = value; }
        public int Id { get => id; set => this.id = value; }
        public double? VlrTotalEmprestimo { get => vlrTotalEmprestimo; set => this.vlrTotalEmprestimo = value;  }
        public int? NrParcela { get => nrParcela; set => this.nrParcela = value;  }
        public int? NrTotalParcelas { get => nrTotalParcelas; set => this.nrTotalParcelas = value;  }
    }
}