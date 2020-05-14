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
        private int tipoConta;
        private List<Transacao> historicoTransacoes;

        public Conta(int acc, int password, double vlrDisponivel, int tipoConta)
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
                    break;
                case 2:
                    this.limiteDiario = 1000;
                    this.limiteFimDeSemana = 750;
                    break;
                case 3:
                    this.limiteDiario = 3000;
                    this.limiteFimDeSemana = 5000;
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

        public void CreditarValor(double valor)
        {
            valorDisponivel += valor;
            GravarTransacao(DateTime.Now, "Depósito", valor);
        }

        public void DebitarValor(double valor)
        {
            valorDisponivel -= valor;
            GravarTransacao(DateTime.Now, "Saque", valor);
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

        public void subtrairLimiteDiario(double valor, String date)
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