using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CxMasterPlus
{
    public class Conta
    {
        private int numeroConta;
        private int senha;
        private double valorDisponivel;
        private double limiteDiario;
        private double limiteFimDeSemana;
        private double vlrDispEmprestimo;
        private int tipoConta;
        private List<Transacao> historicoTransacoes;

        public int NumeroConta
        {
            get => this.numeroConta;
            set
            {
                numeroConta = value;
            }
        }

        public int Senha
        {
            get => this.senha;
            set
            {
                senha = value;
            }
        }

        public int TipoConta
        {
            get => this.tipoConta;
            set
            {
                tipoConta = value;
            }
        }

        public double ValorDisponivel
        {
            get => this.valorDisponivel;
            set
            {
                valorDisponivel = value;
            }
        }

        public double LimiteDiario {
            get => this.limiteDiario;
            set {
                limiteDiario = value;
            }
        }

        public double ValorDisponivelEmprestimo
        {
            get => this.vlrDispEmprestimo;
            set
            {
                vlrDispEmprestimo = value;
            }
        }

    }
}