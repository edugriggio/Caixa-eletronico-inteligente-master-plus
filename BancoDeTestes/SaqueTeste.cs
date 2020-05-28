using Microsoft.VisualStudio.TestTools.UnitTesting;
using CxMasterPlus;

namespace BancoDeTestes
{
    [TestClass]
    public class SaqueTeste
    {
        private BaseDeDados dados = new BaseDeDados();
        private CompartimentoDeSaque compartimentoDeSaque = new CompartimentoDeSaque();
        

        [TestMethod]
        public void Testesaquecontauniversitaria()
        {
            Saque saque = new Saque();
            string teste;

            //teste dos limites diarios
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Sunday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Saturday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Monday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Tuesday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Wednesday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Thursday"));
            Assert.AreEqual(200, dados.getLimiteDiario(9999, "Friday"));

            //teste de saque de valor negativo
            teste = saque.EfetuarSaque(9999, dados, -200, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de zero
            teste = saque.EfetuarSaque(9999, dados, 0, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de valor superior ao limite diario
            teste = saque.EfetuarSaque(9999, dados, 500, compartimentoDeSaque);
            Assert.AreEqual("Você não possui saldo suficiente em sua conta.\n\nPor favor, escolha um valor menor.", teste);

            //teste de saque do dentro do limite diario
            teste = saque.EfetuarSaque(9999, dados, 200, compartimentoDeSaque);
            Assert.AreEqual("Transação realizada.\nPor favor, retire seu dinheiro.", teste);

            //teste de saque apos ultrapassar o limite diario
            teste = saque.EfetuarSaque(9999, dados, 200, compartimentoDeSaque);
            Assert.AreEqual("Valor excede o limite diário conforme o seu tipo de conta.", teste);

            

        }

        [TestMethod]
         public void Testesaquecontapadrao()
         {
             Saque saque = new Saque();
             string teste;
            //teste dos limites diarios
            Assert.AreEqual(750, dados.getLimiteDiario(8888, "Sunday"));
             Assert.AreEqual(750, dados.getLimiteDiario(8888, "Saturday"));
             Assert.AreEqual(1000, dados.getLimiteDiario(8888, "Monday"));
             Assert.AreEqual(1000, dados.getLimiteDiario(8888, "Tuesday"));
             Assert.AreEqual(1000, dados.getLimiteDiario(8888, "Wednesday"));
             Assert.AreEqual(1000, dados.getLimiteDiario(8888, "Thursday"));
             Assert.AreEqual(1000, dados.getLimiteDiario(8888, "Friday"));

            //teste de saque de valor negativo
            teste = saque.EfetuarSaque(8888, dados, -200, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de zero
            teste = saque.EfetuarSaque(8888, dados, 0, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de valor superior ao limite diario
            teste = saque.EfetuarSaque(8888, dados, 2000, compartimentoDeSaque);
            Assert.AreEqual("Você não possui saldo suficiente em sua conta.\n\nPor favor, escolha um valor menor.", teste);

            //teste de saque do dentro do limite diario
            teste = saque.EfetuarSaque(8888, dados, 200, compartimentoDeSaque);
            Assert.AreEqual("Transação realizada.\nPor favor, retire seu dinheiro.", teste);

            //teste de saque apos ultrapassar o limite diario
            teste = saque.EfetuarSaque(8888, dados, 900, compartimentoDeSaque);
            Assert.AreEqual("Valor ultrapassa seu limite de saque diário.", teste);
         }

         [TestMethod]
         public void Testesaquecontapremium()
         {
             Saque saque = new Saque();
             string teste;

             //teste dos limites diarios
             Assert.AreEqual(5000, dados.getLimiteDiario(7777, "Sunday"));
             Assert.AreEqual(5000, dados.getLimiteDiario(7777, "Saturday"));
             Assert.AreEqual(3000, dados.getLimiteDiario(7777, "Monday"));
             Assert.AreEqual(3000, dados.getLimiteDiario(7777, "Tuesday"));
             Assert.AreEqual(3000, dados.getLimiteDiario(7777, "Wednesday"));
             Assert.AreEqual(3000, dados.getLimiteDiario(7777, "Thursday"));
             Assert.AreEqual(3000, dados.getLimiteDiario(7777, "Friday"));

            //teste de saque de valor negativo
            teste = saque.EfetuarSaque(7777, dados, -200, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de zero
            teste = saque.EfetuarSaque(7777, dados, 0, compartimentoDeSaque);
            Assert.AreEqual("Valor inválido", teste);

            //teste de saque de valor superior ao limite diario
            teste = saque.EfetuarSaque(7777, dados, 6000, compartimentoDeSaque);
            Assert.AreEqual("Você não possui saldo suficiente em sua conta.\n\nPor favor, escolha um valor menor.", teste);

            //teste de saque do dentro do limite diario
            teste = saque.EfetuarSaque(7777, dados, 3000, compartimentoDeSaque);
            Assert.AreEqual("Transação realizada.\nPor favor, retire seu dinheiro.", teste);

            //teste de saque apos ultrapassar o limite diario
            teste = saque.EfetuarSaque(7777, dados, 3000, compartimentoDeSaque);
            Assert.AreEqual("Valor excede o limite diário conforme o seu tipo de conta.", teste);
         }
    }
}
