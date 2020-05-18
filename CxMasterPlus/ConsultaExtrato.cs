using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class ConsultaExtrato
    {

        public String GerarExtrato(int nrConta, BaseDeDados baseDeDados, Tela tela, int intervaloExtrato)
        {
            try
            {
                double valorDisponivel = baseDeDados.retornaSaldoDisponivel(nrConta);

                List<Transacao> historicoTransacoes = baseDeDados.getHistoricoTransacoes(nrConta);
                StringBuilder sb = new StringBuilder();
                sb.Append("Informações de saldo:");
                sb.Append("\nSaldo disponível: " + tela.converterValor(valorDisponivel));

                if (intervaloExtrato > 0)
                {
                    sb.Append(String.Format("\nExtrato dos últimos {0} dias:", intervaloExtrato.ToString()));

                    foreach (Transacao transacao in historicoTransacoes)
                    {
                        if (transacao.DataTransacao >= DateTime.Now.AddDays(-intervaloExtrato))
                        {
                            sb.Append("\n------------------------------------");
                            sb.Append("\nData: " + transacao.DataTransacao);
                            sb.Append("\nOperação: " + transacao.Operacao);
                            sb.Append("\nValor: " + tela.converterValor(transacao.Valor));
                        }
                    }
                }
                else
                {
                    foreach (Transacao transacao in historicoTransacoes)
                    {
                        sb.Append("\n------------------------------------");
                        sb.Append("\nData: " + transacao.DataTransacao);
                        sb.Append("\nOperação: " + transacao.Operacao);
                        sb.Append("\nValor: " + tela.converterValor(transacao.Valor));
                    }
                }

                return sb.ToString();
            }
            catch (Exception)
            {
                return "Problema ao realizar a consulta. Tente novamente mais tarde.";
            }

        }
    }
}