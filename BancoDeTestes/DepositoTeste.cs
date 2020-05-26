using Microsoft.VisualStudio.TestTools.UnitTesting;
using CxMasterPlus;

namespace BancoDeTestes
{
    [TestClass]
    public class DepositoTeste
    {
        private BaseDeDados dados = new BaseDeDados();
        private CompartimentoDeSaque compartimentoDeSaque = new CompartimentoDeSaque();
        Deposito deposito = new Deposito();
        string teste;

        [TestMethod]
        public void Testedepositocontauniversitaria()
        {
            //teste de deposito de valos valores positivos na conta universitária
             teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transação Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta universitária
            teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transação. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta universitária
            teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Contas universitárias não podem efetuar depósito em cheque.", teste);

        }

        [TestMethod]
        public void Testedepoditocontapadrao()
        {
            //teste de deposito de valos valores positivos na conta padrão
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transação Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta padrão
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transação. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta padrão
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Transação Efetivada.", teste);
        }

        [TestMethod]
        public void Testedepositocontapremium()
        {
            //teste de deposito de valos valores positivos na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transação Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transação. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Transação Efetivada.", teste);
        }
    }
}
