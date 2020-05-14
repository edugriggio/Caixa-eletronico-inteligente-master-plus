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

            int intervaloExtrato = 0;
            bool validaIntervalo = false;
            
            while (!validaIntervalo) {
                Console.Clear();
                tela.imprimirMensagem("Digite o intervalo de dias que deseja visualizar do seu extrato (0 para ver todo extrato): ");
                try
                {
                    intervaloExtrato = Convert.ToInt32(Console.ReadLine());
                    validaIntervalo = true;
                }
                catch (Exception)
                {
                    Console.Clear();
                    tela.imprimirMensagem("O valor digitado não é válido.");
                    Console.ReadKey();
                }
            }
            

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