using System;
using System.Collections.Generic;

namespace CxMasterPlus
{
    public class ConsultaExtrato : Operacao
    {
        public ConsultaExtrato(int codConta, BaseDeDados dados, Tela tela, CompartimentoDeSaque compartimentoDeSaque) : base(codConta, dados, tela, compartimentoDeSaque)
        {
            this.execute();
        }

        public override void execute()
        {
            BaseDeDados baseDeDados = getBaseDeDados();

            Tela tela = getTela();

            double valorDisponivel = baseDeDados.retornaSaldoDisponivel(getNrConta());
            double valorTotal = getCompartimentoDeSaque().valorEmCaixa();

            List<Transacao> historicoTransacoes = baseDeDados.getHistoricoTransacoes(getNrConta());

            Console.Clear();

            tela.imprimirMensagem("Digite o intervalo de dias que deseja visualizar do seu extrato (0 para ver todo extrato): ");
            int intervaloExtrato = Convert.ToInt32(Console.ReadLine());

            tela.imprimirMensagem("Informações de saldo:");
            tela.imprimirMensagem("Saldo disponível: " + tela.converterValor(valorDisponivel));

            if (intervaloExtrato > 0)
            {
                tela.imprimirMensagem("Extrato dos últimos {0} dias", intervaloExtrato.ToString());

                foreach (Transacao transacao in historicoTransacoes)
                {
                    if (transacao.DataTransacao >= DateTime.Now.AddDays(-intervaloExtrato))
                    {
                        tela.imprimirMensagem("------------------------------------");
                        tela.imprimirMensagem("Data: " + transacao.DataTransacao);
                        tela.imprimirMensagem("Operação: " + transacao.Operacao);
                        tela.imprimirMensagem("Valor: " + tela.converterValor(transacao.Valor));
                    }
                }
            }
            else
            {
                foreach (Transacao transacao in historicoTransacoes)
                {
                    tela.imprimirMensagem("------------------------------------");
                    tela.imprimirMensagem("Data: " + transacao.DataTransacao);
                    tela.imprimirMensagem("Operação: " + transacao.Operacao);             
                    tela.imprimirMensagem("Valor: " + tela.converterValor(transacao.Valor));
                }
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}