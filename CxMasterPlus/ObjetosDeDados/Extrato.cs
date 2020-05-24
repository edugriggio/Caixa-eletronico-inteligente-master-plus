using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Extrato
    {
        private double valorDisponivel;
        private List<Transacao> historicoTransacoes;

        public Extrato(double valorDisponivel, List<Transacao> historicoTransacoes)
        {
            this.valorDisponivel = valorDisponivel;
            this.historicoTransacoes = historicoTransacoes;
        }

        public double ValorDisponivel { get => valorDisponivel; }
        public List<Transacao> HistoricoTransacoes { get => historicoTransacoes; }
    }
}
