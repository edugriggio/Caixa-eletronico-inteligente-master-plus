using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace CxMasterPlus
{
    public class Emprestimo
    {
        public bool VeririficaValorEmprestimo(double vlrEmprestimo, double vlrMaximo)
        {
            return (vlrEmprestimo > 0 && vlrEmprestimo <= vlrMaximo) ? true : false;
        }

        public bool VeririficaPrazoEmprestimo(double nrParcelas)
        {
            return (nrParcelas > 0 && nrParcelas <= 12) ? true : false;
        }

        public string RealizarEmprestimo(Tela tela, int nrConta, BaseDeDados baseDeDados, double vlrEmprestimo, int nrTotalParcelas)
        {
            DateTime dataAtual = DateTime.Today;
            double taxaJuros = 5;
            double vlrEmpComJuros = vlrEmprestimo + (vlrEmprestimo * (taxaJuros / 100));
            double vlrParcelas = vlrEmpComJuros / nrTotalParcelas;

            int nrParcela = 1;

            //Credita valor na conta
            baseDeDados.CreditarValor(nrConta, vlrEmprestimo, Enums.Emprestimo);

            //Subtrai limite de empréstimo na conta
            baseDeDados.SubtrairLimiteEmprestimo(nrConta, vlrEmprestimo);

            //Adiciona parcelas futuras ao extrato
            for (int i = 1; i <= nrTotalParcelas; i++)
            {
                DateTime dataParcela = dataAtual.AddMonths(i);
                baseDeDados.ParcelaEmprestimo(nrConta, vlrEmprestimo, vlrParcelas, dataParcela, nrParcela, nrTotalParcelas);
                nrParcela++;
            }
            return "Transação Efetivada.";

        }
    }
}