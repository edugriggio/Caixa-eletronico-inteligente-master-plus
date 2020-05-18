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
                baseDeDados.creditarValor(nrConta, qtd);
                if (tipoDeposito != 1)
                {
                    compartimentoDeSaque.adicionarNotas(qtd);
                }
                return "Transação Efetivada.";
            }
            catch (Exception)
            {
                return "Problemas ao efetuar sua transação. Tente novamente mais tarde.";
            }
        }

    }
}
