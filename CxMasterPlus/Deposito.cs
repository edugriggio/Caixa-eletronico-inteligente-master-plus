using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Deposito
    {
        public String realizarDeposito(int nrConta, BaseDeDados baseDeDados, CompartimentoDeSaque compartimentoDeSaque, int tipoDeposito, int qtd)
        {
            try
            {
                int tipoConta = baseDeDados.getTipoConta(nrConta);
                if (tipoConta == 1 && tipoDeposito == 1)
                {
                    return "Contas universitárias não podem efetuar depósito em cheque.";
                }
                if (tipoDeposito == 1 || tipoDeposito == 2)
                {
                    baseDeDados.creditarValor(nrConta, qtd);
                    if (tipoDeposito == 2)
                    {
                        compartimentoDeSaque.adicionarNotas(qtd);
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
