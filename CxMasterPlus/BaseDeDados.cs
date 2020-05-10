using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CxMasterPlus
{

    public class BaseDeDados
    {

        private Conta[] contas;

        public BaseDeDados()
        {
            contas = new Conta[3];
            contas[0] = new Conta(9999, 12345, 300, 1);
            contas[1] = new Conta(8888, 12345, 1000, 2);
            contas[2] = new Conta(7777, 12345, 1000, 3);
        }

        private Conta retornaConta(int nrConta)
        {
            foreach (Conta acc in contas)
            {
                if (acc.NumeroConta().Equals(nrConta))
                {
                    return acc;
                }
            }
            return null;
        }

        public Boolean autenticarUsuario(int nrConta, int senha)
        {
            Conta conta = this.retornaConta(nrConta);
            return conta != null ? conta.ValidarSenha(senha) : false;
        }

        public double retornaSaldoDisponivel(int nrConta)
        {
            return retornaConta(nrConta).ValorDisponivel();
        }

        public void creditarValor(int nrConta, double valor)
        {
            this.retornaConta(nrConta).CreditarValor(valor);
        }

        public void debitarValor(int nrConta, double valor)
        {
            this.retornaConta(nrConta).DebitarValor(valor);
        }

        public void subtrairLimiteDiario(int nrConta, double valor, String date)
        {
            this.retornaConta(nrConta).subtrairLimiteDiario(valor, date);
        }

        public double getLimiteDiario(int nrConta, String date)
        {
            return this.retornaConta(nrConta).LimiteDiario(date);
        }

        public int getTipoConta(int nrConta)
        {
            return this.retornaConta(nrConta).TipoConta();
        }
    }


}
