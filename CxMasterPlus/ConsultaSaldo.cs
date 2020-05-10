using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class ConsultaSaldo : Transacao
    {
        public ConsultaSaldo(int codConta, BaseDeDados dados, Tela tela, CompartimentoDeSaque compartimentoDeSaque) : base(codConta, dados, tela, compartimentoDeSaque)
        {
            this.execute();
        }

        public override void execute()
        {
            BaseDeDados baseDeDados = getBaseDeDados();

            Tela tela = getTela();

            double valorDisponivel = baseDeDados.retornaSaldoDisponivel(getNrConta());
            double valorTotal = getCompartimentoDeSaque().valorEmCaixa();

            Console.Clear();
            tela.imprimirMensagem("Informações de saldo:");
            tela.imprimirMensagem("Saldo disponível: " + tela.converterValor(valorDisponivel));
            tela.imprimirMensagem("Saldo disponível no caixa: " + tela.converterValor(valorTotal));
            Console.ReadKey();
            Console.Clear();
        }
    }
}
