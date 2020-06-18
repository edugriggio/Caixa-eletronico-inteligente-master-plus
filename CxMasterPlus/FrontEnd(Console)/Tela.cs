using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CxMasterPlus
{
    public class Tela
    {
        public void ImprimirMensagem(String mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public String converterValor(double valor)
        {
            return valor.ToString("C2", CultureInfo.CurrentCulture);
        }

        public void ExibirExtrato(Extrato extrato)
        {
            //Exibe cabeçalho do extrato
            StringBuilder sb = new StringBuilder();
            sb.Append("Informações de saldo:");
            sb.Append("\nSaldo disponível: " + converterValor(extrato.ValorDisponivel));

            if (extrato.HistoricoTransacoes.Count == 0)
            {
                sb.Append("\nNenhuma movimentação no período selecionado.");
            }
            else
            {
                //Exibe corpo do extrato
                foreach (Transacao transacao in extrato.HistoricoTransacoes)
                {
                    sb.Append("\n------------------------------------");
                    sb.Append("\nData: " + transacao.DataTransacao);
                    sb.Append("\nOperação: " + transacao.Operacao);

                    //Verifica sinal da transação de acordo com o tipo                
                    if (transacao.Operacao == Operacao.Saque)
                    {
                        sb.Append("\nValor: - " + converterValor(transacao.Valor));
                    }
                    else
                    {
                        sb.Append("\nValor: " + converterValor(transacao.Valor));
                    }
                }
            }
           
            ImprimirMensagem(sb.ToString());
        }

        public string ConfirmacaoEmprestimo(double vlrEmprestimo, DateTime dataAtual, int nrTotalParcelas, double vlrEmpComJuros)
        {
            Console.Clear();
            ImprimirMensagem(string.Concat("Valor do empréstimo: ", vlrEmprestimo.ToString("C")));
            ImprimirMensagem(string.Concat("Prazo final para pagamento: ", dataAtual.AddMonths(nrTotalParcelas).ToString("dd/MM/yyyy")));
            ImprimirMensagem(string.Concat("Valor total a ser pago: ", vlrEmpComJuros.ToString("C")));
            ImprimirMensagem("\n--------------------------------------\n");
            ImprimirMensagem("Confirma a solicitação do empréstimo?");
            ImprimirMensagem("1 - Sim");
            ImprimirMensagem("2 - Não");
             
            return Console.ReadLine(); 
        }

        public string ConfirmacaoPagamentoParcelaEmp(double valorParcela, double novoValorParcela)
        {
            Console.Clear();
            ImprimirMensagem(String.Concat("Juros inicial: 5%"));
            ImprimirMensagem(String.Concat("Valor da parcela: ", valorParcela.ToString("C")));
            ImprimirMensagem(String.Concat("\nJuros abonado: 2%"));
            ImprimirMensagem(String.Concat("Novo valor da parcela: ", novoValorParcela.ToString("C")));
            ImprimirMensagem("\n--------------------------------------\n");
            ImprimirMensagem(String.Concat("Confirma o pagamento no valor de ", novoValorParcela.ToString("C"), "?"));
            ImprimirMensagem("1 - Sim");
            ImprimirMensagem("2 - Não");

            return Console.ReadLine();
        }
    }   
}
