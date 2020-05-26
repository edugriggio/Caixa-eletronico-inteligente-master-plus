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
            //teste de deposito de valos valores positivos na conta universit�ria
             teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transa��o Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta universit�ria
            teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transa��o. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta universit�ria
            teste = deposito.RealizarDeposito(9999, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Contas universit�rias n�o podem efetuar dep�sito em cheque.", teste);

        }

        [TestMethod]
        public void Testedepoditocontapadrao()
        {
            //teste de deposito de valos valores positivos na conta padr�o
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transa��o Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta padr�o
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transa��o. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta padr�o
            teste = deposito.RealizarDeposito(8888, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Transa��o Efetivada.", teste);
        }

        [TestMethod]
        public void Testedepositocontapremium()
        {
            //teste de deposito de valos valores positivos na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 2, 1000);
            Assert.AreEqual("Transa��o Efetivada.", teste);

            //teste de deposito de valos valores negativos na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 2, -1000);
            Assert.AreEqual("Problemas ao efetuar sua transa��o. Tente novamente mais tarde.", teste);

            //teste de deposito em cheque na conta premium
            teste = deposito.RealizarDeposito(7777, dados, compartimentoDeSaque, 1, 1000);
            Assert.AreEqual("Transa��o Efetivada.", teste);
        }
    }
}
