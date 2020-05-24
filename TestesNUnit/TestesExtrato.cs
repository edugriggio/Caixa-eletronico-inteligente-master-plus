using NUnit.Framework;
using CxMasterPlus;
using FrameworkProjeto;
using System.Collections.Generic;
using System;

namespace TestesNUnit
{
    public class TestesExtrato
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsultaExtratoSucessoTodoHistorico()
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(9999, new BaseDeDados(), new Tela(), 0);

            Assert.IsTrue(retExtrato.Ok, retExtrato.Mensagem.ToString());
            Assert.AreEqual("Consulta de Extrato realizada com sucesso", retExtrato.Mensagem.ToString());
            Assert.AreEqual(retExtrato.Dados.HistoricoTransacoes.Count, 3);
        }

        [Test]
        public void ConsultaExtratoSucessoHistorico8Dias()
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(9999, new BaseDeDados(), new Tela(), 8);

            Assert.IsTrue(retExtrato.Ok, retExtrato.Mensagem.ToString());
            Assert.AreEqual("Consulta de Extrato realizada com sucesso", retExtrato.Mensagem.ToString());
            Assert.AreEqual(retExtrato.Dados.HistoricoTransacoes.Count, 1);
        }

        [Test]
        public void ConsultaExtratoSucessoHistorico16Dias()
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(9999, new BaseDeDados(), new Tela(), 16);

            Assert.IsTrue(retExtrato.Ok, retExtrato.Mensagem.ToString());
            Assert.AreEqual("Consulta de Extrato realizada com sucesso", retExtrato.Mensagem.ToString());
            Assert.AreEqual(retExtrato.Dados.HistoricoTransacoes.Count, 2);
        }

        [Test]
        public void ConsultaExtratoSucessoHistorico32Dias()
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(9999, new BaseDeDados(), new Tela(), 32);

            Assert.IsTrue(retExtrato.Ok, retExtrato.Mensagem.ToString());
            Assert.AreEqual("Consulta de Extrato realizada com sucesso", retExtrato.Mensagem.ToString());
            Assert.AreEqual(retExtrato.Dados.HistoricoTransacoes.Count, 3);
        }

        [Test]
        public void ConsultaExtratoFalhaContaInvalida()
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(3817, new BaseDeDados(), new Tela(), 0);

            Assert.IsFalse(retExtrato.Ok);
            NullReferenceException ex = new NullReferenceException();
            Assert.AreEqual(ex.Message, retExtrato.Mensagem.ToString());         
        }

        [Test]
        public void ConsultaExtratoFalhaIntervaloExtratoMenorqueZero([Values(9999, 8888, 7777)] int nrConta, [Values(-1, -5, -100000)] int intervaloExtrato)
        {
            ConsultaExtrato consultaExtrato = new ConsultaExtrato();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(nrConta, new BaseDeDados(), new Tela(), intervaloExtrato);

            Assert.IsFalse(retExtrato.Ok);
            Assert.AreEqual("Intervalo do extrato não pode ser menor que zero.", retExtrato.Mensagem.ToString());
        }
    }
}