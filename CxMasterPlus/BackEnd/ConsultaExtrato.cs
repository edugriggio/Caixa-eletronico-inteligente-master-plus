using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FrameworkProjeto;
using CxMasterPlus;

namespace CxMasterPlus
{
    public class ConsultaExtrato : Aplicacao
    {
        public Retorno<Extrato> GerarExtrato(Conta conta, BaseDeDados baseDeDados, Tela tela, int intervaloExtrato)
        {
            try
            {
                #region Regras de negócio
                #region RN1 - Apenas intervalo maior ou igual a zero
                if (intervaloExtrato < 0)
                {
                    return Infra.RetornarFalha<Extrato>(new MensagemFalha(TipoFalha.IntervaloExtratoMenorQueZero));
                }
                #endregion

                #region RN2 - Apenas histórico no intervalo selecionado          
                List<Transacao> historicoTransacoes = new List<Transacao>(baseDeDados.getHistoricoTransacoes(conta));

                if (intervaloExtrato > 0)
                {
                    historicoTransacoes.RemoveAll(x => x.DataTransacao < DateTime.Now.AddDays(-intervaloExtrato));
                }
                #endregion

                double valorDisponivel = baseDeDados.RetornaSaldoDisponivel(conta);        
                #endregion

                Extrato extrato = new Extrato(valorDisponivel, historicoTransacoes);
                
                return Infra.RetornarSucesso<Extrato>(extrato, new OperacaoRealizadaMensagem("Consulta de Extrato"));
            }
            catch (Exception e)
            {
                return Infra.RetornarFalha<Extrato>(new Mensagem(e));
            }
        }
    }
}