using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CxMasterPlus
{
    public class Conta
    {
        private int numeroConta;
        private int senha;
        private double valorDisponivel;
        private double limiteDiario = 0;
        private double limiteFimDeSemana = 0;
        private double vlrDispEmprestimo = 0;
        private int tipoConta;
        private List<Transacao> historicoTransacoes;

        public Conta(int acc, int password, double vlrDisponivel, int tipoConta, double vlrDispEmprestimo)
        {
            this.tipoConta = tipoConta;
            numeroConta = acc;
            senha = password;
            valorDisponivel = vlrDisponivel;
            historicoTransacoes = new List<Transacao>();
            switch (tipoConta)
            {
                case 1:
                    this.limiteDiario = 200;
                    this.limiteFimDeSemana = 200;
                    this.vlrDispEmprestimo = 500;
                    break;
                case 2:
                    this.limiteDiario = 1000;
                    this.limiteFimDeSemana = 750;
                    this.vlrDispEmprestimo = 2000;
                    break;
                case 3:
                    this.limiteDiario = 3000;
                    this.limiteFimDeSemana = 5000;
                    this.vlrDispEmprestimo = 10000;
                    break;
            }
        }

        public Boolean ValidarSenha(int password)
        {
            return password == senha;
        }

        public double ValorDisponivel()
        {
            return this.valorDisponivel;
        }

        public double ValorDisponivelEmprestimo()
        {
            return this.vlrDispEmprestimo;
        }

        public void CreditarValor(double valor, string operacao)
        {
            valorDisponivel += valor;
            GravarTransacao(DateTime.Now, operacao, valor);

            if(operacao == "Empréstimo")
            {
                SubtrairLimiteEmprestimo(valor);
            }
        }

        public void DebitarValor(double valor)
        {
            valorDisponivel -= valor;
            GravarTransacao(DateTime.Now, "Saque", valor);
        }

        public void ParcelaEmprestimo(double valor, DateTime dataParcela)
        {
            GravarTransacao(dataParcela, "Amortização de empréstimo", valor);
        }

        public int NumeroConta()
        {
            return numeroConta;
        }

        public int TipoConta()
        {
            return tipoConta;
        }

        public double LimiteDiario(String date)
        {
            if (date.Equals(DayOfWeek.Sunday.ToString()) || date.Equals(DayOfWeek.Saturday.ToString()))
            {
                return limiteFimDeSemana;
            }
            else
            {
                return limiteDiario;
            }
        }

        public void SubtrairLimiteDiario(double valor, String date)
        {
            if (date.Equals(DayOfWeek.Sunday.ToString()) || date.Equals(DayOfWeek.Saturday.ToString()))
            {
                limiteFimDeSemana -= valor;
            }
            else
            {
                limiteDiario -= valor;
            }
        }

        public void SubtrairLimiteEmprestimo(double valor)
        {
            vlrDispEmprestimo -= valor;
        }

        public void GravarTransacao(DateTime diaTransacao, string operacao, double valor)
        {
            historicoTransacoes.Add(new Transacao(diaTransacao, operacao, valor));
        }

        public List<Transacao> LerTransacoes()
        {
            return historicoTransacoes;
        }
    }
}