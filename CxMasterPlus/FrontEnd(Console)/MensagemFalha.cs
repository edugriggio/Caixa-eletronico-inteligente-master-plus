using System;
using System.Collections.Generic;
using System.Text;
using FrameworkProjeto;

namespace CxMasterPlus
{
    /// <summary>Tipos de falha</summary>
    public enum TipoFalha
    {
        IntervaloExtratoMenorQueZero
    }

    public class MensagemFalha : Mensagem
    {
        public MensagemFalha(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.IntervaloExtratoMenorQueZero:
                    this.mensagem = "Intervalo do extrato não pode ser menor que zero.";
                    break;
                default:
                    break;
            }
        }
    }
}