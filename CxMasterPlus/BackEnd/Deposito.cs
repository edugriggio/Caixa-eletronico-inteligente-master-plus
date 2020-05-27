using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Deposito
    {
        public String RealizarDeposito(int nrConta, BaseDeDados baseDeDados, CompartimentoDeSaque compartimentoDeSaque, int tipoDeposito, int qtd)
        {
            try
            {
                int tipoConta = baseDeDados.getTipoConta(nrConta);

                //Verifica tipo de conta
                if (tipoConta == 1 && tipoDeposito == 1)
                {
                    return "Contas universitárias não podem efetuar depósito em cheque.";
                }
                if (tipoDeposito == 1 && qtd > 0 || tipoDeposito == 2 && qtd > 0)
                {
                    //Credita valor na conta
                    baseDeDados.CreditarValor(nrConta, qtd, Enums.Deposito);
                    if (tipoDeposito == 2)
                    {
                        compartimentoDeSaque.AdicionarNotas(qtd);
                    }
                    return "Transação Efetivada.";
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
