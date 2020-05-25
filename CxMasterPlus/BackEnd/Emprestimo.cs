using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace CxMasterPlus.BackEnd
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

        public string RealizarEmprestimo(Tela tela, int nrConta, BaseDeDados baseDeDados, double vlrEmprestimo, double taxaJuros, int nrParcelas)
        {
            DateTime dataAtual = DateTime.Today;
            double vlrEmpComJuros = vlrEmprestimo + (vlrEmprestimo * (taxaJuros / 100));
            double vlrParcelas = vlrEmpComJuros / nrParcelas;

            #region Menu de confirmação da operação
            Console.Clear();
            tela.ImprimirMensagem(string.Concat("Valor do empréstimo: ", vlrEmprestimo.ToString("C")));
            tela.ImprimirMensagem(string.Concat("Prazo final para pagamento: ", dataAtual.AddMonths(nrParcelas).ToString("dd/MM/yyyy")));
            tela.ImprimirMensagem(string.Concat("Valor total a ser pago: ", vlrEmpComJuros.ToString("C")));
            tela.ImprimirMensagem("\n--------------------------------------\n");
            tela.ImprimirMensagem("Confirma a solicitação do empréstimo?");
            tela.ImprimirMensagem("1 - Sim");
            tela.ImprimirMensagem("2 - Não");

            Validadores validador = new Validadores();
            int opcao = validador.ValidarInputMenu(tela, Console.ReadLine());
            #endregion

            #region Realiza o Empréstimo
            if (opcao == 1)
            {
                //Credita valor na conta
                baseDeDados.CreditarValor(nrConta, vlrEmprestimo, "Empréstimo");

                //Adiciona parcelas futuras ao extrato
                for (int i = 1; i <= nrParcelas; i++)
                {
                    DateTime dataParcela = dataAtual.AddMonths(i);
                    baseDeDados.ParcelaEmprestimo(nrConta, vlrParcelas, dataParcela);
                }

                return "Transação Efetivada.";
            }
            #endregion

            //Caso a operação seja recusada
            return "Operação cancelada.";
        }
    }
}
