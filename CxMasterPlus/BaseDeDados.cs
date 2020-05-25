using System;
using System.Collections.Generic;

namespace CxMasterPlus
{

    public class BaseDeDados
    {
        private Conta[] contas;

        public BaseDeDados()
        {
            contas = new Conta[3];
            contas[0] = new Conta(9999, 12345, 300, 1, 500);
            contas[1] = new Conta(8888, 12345, 1000, 2, 2000);
            contas[2] = new Conta(7777, 12345, 1000, 3, 10000);

            contas[0].GravarTransacao(DateTime.Now.AddDays(-30), "Saque", 200);
            contas[0].GravarTransacao(DateTime.Now.AddDays(-15), "Depósito", 500);
            contas[0].GravarTransacao(DateTime.Now.AddDays(-7), "Saque", 300);
        }

        private Conta RetornaConta(int nrConta)
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

        public Boolean AutenticarUsuario(int nrConta, int senha)
        {
            Conta conta = this.RetornaConta(nrConta);
            return conta != null ? conta.ValidarSenha(senha) : false;
        }

        public double RetornaSaldoDisponivel(int nrConta)
        {
            return RetornaConta(nrConta).ValorDisponivel();
        }

        public void CreditarValor(int nrConta, double valor, string operacao)
        {
            this.RetornaConta(nrConta).CreditarValor(valor, operacao);
        }

        public void DebitarValor(int nrConta, double valor)
        {
            this.RetornaConta(nrConta).DebitarValor(valor);
        }

        public void ParcelaEmprestimo(int nrConta, double valor, DateTime dataParcela)
        {
            this.RetornaConta(nrConta).ParcelaEmprestimo(valor, dataParcela);
        }

        public void SubtrairLimiteDiario(int nrConta, double valor, String date)
        {
            this.RetornaConta(nrConta).SubtrairLimiteDiario(valor, date);
        }

        public double getLimiteDiario(int nrConta, String date)
        {
            return this.RetornaConta(nrConta).LimiteDiario(date);
        }

        public int getTipoConta(int nrConta)
        {
            return this.RetornaConta(nrConta).TipoConta();
        }

        public List<Transacao> getHistoricoTransacoes(int nrConta)
        {
            return RetornaConta(nrConta).LerTransacoes();
        }

        public double getVlrDispEmprestimo(int nrConta)
        {
            return this.RetornaConta(nrConta).ValorDisponivelEmprestimo();
        }

    }

}