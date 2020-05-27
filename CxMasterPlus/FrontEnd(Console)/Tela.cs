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

        public void imprimirMensagem(String mensagem, params string[] args)
        {
            Console.WriteLine(string.Format(mensagem, args[0]));
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
                    if (transacao.Operacao == Enums.Deposito || transacao.Operacao == Enums.Emprestimo)
                    {
                        sb.Append("\nValor: + " + converterValor(transacao.Valor));
                    }
                    else if (transacao.Operacao == Enums.Saque || transacao.Operacao.Contains("Pagamento"))
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
    }
}
