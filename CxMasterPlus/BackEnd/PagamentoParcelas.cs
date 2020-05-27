using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CxMasterPlus
{
    class PagamentoParcelas
    {
        Validadores validador = new Validadores();

        public bool VeririficaSaldoPagamento(double novoValorParcela, double saldoDisponivel)
        {
            return novoValorParcela < saldoDisponivel ? true : false;
        }

        public string RealizarPagamento(Tela tela, BaseDeDados baseDeDados, int nrConta, Transacao parcela)
        {
            double vrTotalEmp = Convert.ToDouble(parcela.VrTotalEmprestimo);

            //Recalcula valor com novo juros
            double novoValorTotal = vrTotalEmp + (vrTotalEmp * 0.02);
            double novoValorParcela = novoValorTotal / Convert.ToDouble(parcela.NrTotalParcelas);

            #region Menu de confirmação da operação
            Console.Clear();
            tela.ImprimirMensagem(String.Concat("Juros inicial: 5%"));
            tela.ImprimirMensagem(String.Concat("Valor da parcela: ", parcela.Valor.ToString("C")));
            tela.ImprimirMensagem(String.Concat("\nJuros abonado: 2%"));
            tela.ImprimirMensagem(String.Concat("Novo valor da parcela: ", novoValorParcela.ToString("C")));
            tela.ImprimirMensagem("\n--------------------------------------\n");
            tela.ImprimirMensagem(String.Concat("Confirma o pagamento no valor de ", novoValorParcela.ToString("C"), "?"));
            tela.ImprimirMensagem("1 - Sim");
            tela.ImprimirMensagem("2 - Não");
            #endregion
            int opcao = validador.ValidarInputMenu(tela, Console.ReadLine());

            if (opcao == 1)
            {
                #region Valida se conta tem dinheiro suficiente para pagamento da parcela
                double saldoDisponivel = baseDeDados.RetornaSaldoDisponivel(nrConta);

                if (!VeririficaSaldoPagamento(novoValorParcela, saldoDisponivel))
                {
                    Console.Clear();
                    tela.ImprimirMensagem(String.Concat("Saldo insuficiente para pagamento da parcela. A transação será cancelada."));
                    Console.ReadKey();
                    Console.Clear();
                }
                #endregion
                #region Realiza pagamento
                else
                {
                    //Busca parcela a ser paga
                    var parcelaAPagar = baseDeDados.getHistoricoTransacoes(nrConta)
                                                    .Where(x => x.DataTransacao == parcela.DataTransacao).ToList();

                    //Efetua pagamento
                    baseDeDados.PagamentoParcela(nrConta, novoValorTotal, novoValorParcela, DateTime.Now, parcela.NrParcela, parcela.NrTotalParcelas);

                    //Atualiza próxima parcela para o extrato
                    baseDeDados.AtualizaParcelaPrevista(nrConta, parcela);

                    //Adiciona limite de empréstimo na conta
                    baseDeDados.AdicionarLimiteEmprestimo(nrConta, vrTotalEmp);

                    return "Transação efetuada.";
                }
                #endregion
            }

            //Caso a operação seja recusada
            return "Operação cancelada.";
        }
    }
}
