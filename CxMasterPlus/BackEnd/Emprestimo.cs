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

        public string RealizarEmprestimo(Tela tela, Conta conta, BaseDeDados baseDeDados, double vlrEmprestimo, int nrTotalParcelas)
        {
            double taxaJuros = 5;
            double vlrEmpComJuros = vlrEmprestimo + (vlrEmprestimo * (taxaJuros / 100));
            double vlrParcelas = vlrEmpComJuros / nrTotalParcelas;

            bool transacao = baseDeDados.realizarEmprestimo(conta, vlrEmprestimo, vlrParcelas, nrTotalParcelas, taxaJuros, Operacao.Emprestimo);
            if (transacao)
            {
                return "Transação Efetivada.";
            }
            else
            {
                return "Falha ao realizar a operação. Tente novamente mais tarde.";
            }


        }
    }
}