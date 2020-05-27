using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CxMasterPlus
{
    class PagamentoParcelas
    {      
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
            string opcaoSelecionada = tela.ConfirmacaoPagamentoParcelaEmp(parcela.Valor, novoValorParcela);

            Validadores validador = new Validadores();
            int opcao = validador.ValidarInputMenu(tela, opcaoSelecionada);
            #endregion         

            switch (opcao)
            {
                case 0:
                    return "Transação cancelada.";

                case 1:
                    #region Valida se conta tem dinheiro suficiente para pagamento da parcela
                    double saldoDisponivel = baseDeDados.RetornaSaldoDisponivel(nrConta);

                    if (!VeririficaSaldoPagamento(novoValorParcela, saldoDisponivel))
                    {                   
                        return "Saldo insuficiente para pagamento da parcela. A transação será cancelada.";
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

                case 2:
                    return "Operação cancelada.";

                default:
                    return "Opção Inválida. Operação cancelada.";
            }  
        }
    }
}