using NUnit.Framework;
using CxMasterPlus;
using FrameworkProjeto;
using System.Collections.Generic;
using System;

namespace TestesNUnit
{
    public class TestesPagamentoEmprestimo
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PagaParcelaEmprestimoSucesso()
        {
            PagamentoParcelas pagParcelaEmp = new PagamentoParcelas();

            Transacao emprestimo = new Transacao(new DateTime(), "Empréstimo", 500, 1, 500, 12);

            string retPagEmp = pagParcelaEmp.RealizarPagamento(new BaseDeDados(), 9999, emprestimo);

            Assert.AreEqual("Transação efetuada.", retPagEmp);
        }
    }
}