using NUnit.Framework;
using CxMasterPlus;
using FrameworkProjeto;
using System.Collections.Generic;
using System;

namespace TestesNUnit
{
    public class TestesEmprestimo
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RealizaEmprestimoSucesso()
        {
            Emprestimo emprestimo = new Emprestimo();

            string retEmprestimo = emprestimo.RealizarEmprestimo(new Tela(), 9999, new BaseDeDados(), 500, 12);

            Assert.AreEqual("Transação Efetivada.", retEmprestimo);

            PagamentoParcelas pag = new PagamentoParcelas();
        }


    }
}