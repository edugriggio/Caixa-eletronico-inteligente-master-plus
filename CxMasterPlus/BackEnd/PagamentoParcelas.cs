using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CxMasterPlus
{
    public class PagamentoParcelas
    {
        public bool VeririficaSaldoPagamento(double novoValorParcela, double saldoDisponivel)
        {
            return novoValorParcela < saldoDisponivel ? true : false;
        }

        public string RealizarPagamento(BaseDeDados baseDeDados, Conta conta, Transacao parcela)
        {
            double vrTotalEmp = Convert.ToDouble(parcela.VlrTotalEmprestimo);
            double novoValorParcela = parcela.Valor;
            //Recalcula valor com novo juros
            if (parcela.ProximaParcela > DateTime.Now)
            {
                novoValorParcela = (parcela.Valor / (1 + (parcela.Taxa / 100))) * 1.02;
            }

            #region Valida se conta tem dinheiro suficiente para pagamento da parcela
            double saldoDisponivel = baseDeDados.obterConta(conta).ValorDisponivel;

            if (!VeririficaSaldoPagamento(novoValorParcela, saldoDisponivel))
            {
                return "Saldo insuficiente para pagamento da parcela. A transação será cancelada.";
            }
            #endregion
            #region Realiza pagamento
            else
            {
                //Efetua pagamento
                if (baseDeDados.PagamentoParcela(conta, novoValorParcela, parcela, Operacao.PagtoParcela))
                {
                    return "Transação efetuada.";
                }
                else
                {
                    return "Problema ao realizar a operação. Tente novamente mais tarde.";
                }

            }
            #endregion
        }
    }
}