using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Deposito
    {
        public String RealizarDeposito(Conta conta, BaseDeDados baseDeDados, int tipoDeposito, int qtd)
        {
            try
            {
                //Verifica tipo de conta
                if (conta.TipoConta == 1 && tipoDeposito == 1)
                {
                    return "Contas universitárias não podem efetuar depósito em cheque.";
                }
                if (tipoDeposito == 1 && qtd > 0 || tipoDeposito == 2 && qtd > 0)
                {

                    if (baseDeDados.CreditarValor(conta, qtd, Operacao.Deposito, tipoDeposito))
                    {
                        return "Transação Efetivada.";
                    }
                }
                return "Problemas ao efetuar sua transação. Tente novamente mais tarde.";
            }
            catch (Exception)
            {
                return "Problemas ao efetuar sua transação. Tente novamente mais tarde.";
            }
        }

    }
}
