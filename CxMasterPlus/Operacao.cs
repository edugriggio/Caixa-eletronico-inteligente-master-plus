using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public abstract class Operacao
    {
        private int nrConta;
        private BaseDeDados baseDeDados;
        private Tela tela;
        private CompartimentoDeSaque compartimentoDeSaque;

        public Operacao(int conta, BaseDeDados dados, Tela tela, CompartimentoDeSaque compartimentoDeSaque)
        {
            this.nrConta = conta;
            this.baseDeDados = dados;
            this.tela = tela;
            this.compartimentoDeSaque = compartimentoDeSaque;
        }

        public int getNrConta()
        {
            return nrConta;
        }

        public Tela getTela()
        {
            return tela;
        }

        public BaseDeDados getBaseDeDados()
        {
            return baseDeDados;
        }

        public CompartimentoDeSaque getCompartimentoDeSaque()
        {
            return compartimentoDeSaque;
        }

        abstract public void execute();
    }
}
