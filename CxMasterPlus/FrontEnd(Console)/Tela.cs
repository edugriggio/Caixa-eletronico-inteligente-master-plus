using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CxMasterPlus
{
    public class Tela
    {
        public void imprimirMensagem(String mensagem)
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
            StringBuilder sb = new StringBuilder();
            sb.Append("Informações de saldo:");
            sb.Append("\nSaldo disponível: " + converterValor(extrato.ValorDisponivel));

            if (extrato.HistoricoTransacoes.Count == 0)
            {
                sb.Append("\nNenhuma movimentação no período selecionado.");
            }
            else
            {
                foreach (Transacao transacao in extrato.HistoricoTransacoes)
                {
                    sb.Append("\n------------------------------------");
                    sb.Append("\nData: " + transacao.DataTransacao);
                    sb.Append("\nOperação: " + transacao.Operacao);
                    sb.Append("\nValor: " + converterValor(transacao.Valor));
                }
            }
           
            imprimirMensagem(sb.ToString());
        }
    }
}
