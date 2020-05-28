using System;
using System.Collections.Generic;

namespace CxMasterPlus
{

    public class BaseDeDados
    {
        private Conta[] contas;

        public BaseDeDados()
        {
            int nrParcela = 1;

            contas = new Conta[3];
            contas[0] = new Conta(9999, 12345, 300, 1, 500);
            contas[1] = new Conta(8888, 12345, 2000, 2, 2000);
            contas[2] = new Conta(7777, 12345, 6000, 3, 10000);

            #region Grava transações conta universitária
            contas[0].GravarTransacao(DateTime.Now.AddDays(-30), Enums.Saque, 200);
            contas[0].GravarTransacao(DateTime.Now.AddDays(-15), Enums.Deposito, 500);
            contas[0].GravarTransacao(DateTime.Now.AddDays(-7), Enums.Saque, 300);
            #endregion

            #region Grava transações conta normal
            contas[1].GravarTransacao(new DateTime(2020, 02, 21), Enums.Emprestimo, 650);

            for (int i = 3; i < DateTime.Today.Month ; i++)
            {
                contas[1].GravarTransacao(new DateTime(2020, i, 21), String.Concat(Enums.PagtoParcela, " (", nrParcela, "/10)"), 650, 68.25, nrParcela, 10);

                nrParcela++;
            }

            contas[1].GravarTransacao(DateTime.Now.AddDays(-25), Enums.Saque, 100);
            contas[1].GravarTransacao(DateTime.Now.AddDays(-12), Enums.Deposito, 200);

            if (DateTime.Today.Day > 21)
            {
                contas[1].GravarTransacao(new DateTime(2020, DateTime.Today.Month, 21), String.Concat(Enums.PagtoParcela, " (", nrParcela, "/10)"), 650, 68.25, nrParcela, 10);
                contas[1].GravarTransacao(new DateTime(2020, DateTime.Today.AddMonths(1).Month, 21), String.Concat(Enums.PagtoParcelaPrevisto, " (", nrParcela + 1, "/10)"), 650, 68.25, nrParcela + 1, 10);
            }
            else
            {
                contas[1].GravarTransacao(new DateTime(2020, DateTime.Today.Month, 21), String.Concat(Enums.PagtoParcelaPrevisto, " (", nrParcela, "/10)"), 650, 68.25, nrParcela, 10);
            }
            #endregion
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

        public void AdicionarLimiteEmprestimo(int nrConta, double valor)
        {
            this.RetornaConta(nrConta).AdicionarLimiteEmprestimo(valor);
        }

        public void SubtrairLimiteEmprestimo(int nrConta, double valor)
        {
            this.RetornaConta(nrConta).SubtrairLimiteEmprestimo(valor);
        }

        public void ParcelaEmprestimo(int nrConta, double vrTotalEmprestimo, double valor, DateTime dataParcela, int nrParcela, int nrTotalParcelas)
        {
            this.RetornaConta(nrConta).ParcelaEmprestimo(vrTotalEmprestimo, valor, dataParcela, nrParcela, nrTotalParcelas);
        }

        public void PagamentoParcela(int nrConta, double vrTotalEmprestimo, double valor, DateTime dataParcela, int? nrParcela, int? nrTotalParcelas)
        {
            this.RetornaConta(nrConta).PagamentoParcela(vrTotalEmprestimo, valor, dataParcela, nrParcela, nrTotalParcelas);
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

        public void AtualizaParcelaPrevista(int nrConta, Transacao parcelaAntiga)
        {
            if (parcelaAntiga.NrParcela < parcelaAntiga.NrTotalParcelas)
            {
                int nrParcela = Convert.ToInt32(parcelaAntiga.NrParcela) + 1;
                string operacao = String.Concat(Enums.PagtoParcelaPrevisto, " (", nrParcela, "/10)");

                Transacao parcelaNova = new Transacao(parcelaAntiga.DataTransacao.AddMonths(1), 
                                                        operacao, 
                                                        parcelaAntiga.Valor,
                                                        nrParcela,
                                                        parcelaAntiga.VrTotalEmprestimo,
                                                        parcelaAntiga.NrTotalParcelas) ;

                this.RetornaConta(nrConta).AdicionarTransacao(parcelaNova);
            }

            this.RetornaConta(nrConta).RemoverTransacao(parcelaAntiga);
        }
    }

}