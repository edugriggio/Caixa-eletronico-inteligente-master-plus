﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CxMasterPlus.BackEnd;
using FrameworkProjeto;

namespace CxMasterPlus
{
    public class MenuOperacoes
    {
        readonly Validadores validador = new Validadores();

        public void MenuSaque(int contaLogada, Tela tela, BaseDeDados baseDeDados, CompartimentoDeSaque compartimentoDeSaque)
        {
            int opcao = 0;
            int input = 0;
            int[] quantias = { 0, 20, 50, 100, 200, 500 };
            Saque saque = new Saque();

            while (opcao != 6)
            {
                Console.Clear();
                tela.ImprimirMensagem("Menu de Saque:");
                tela.ImprimirMensagem("1 - R$20");
                tela.ImprimirMensagem("2 - R$50");
                tela.ImprimirMensagem("3 - R$100");
                tela.ImprimirMensagem("4 - R$200");
                tela.ImprimirMensagem("5 - R$500");
                tela.ImprimirMensagem("6 - Cancelar operação");
                tela.ImprimirMensagem("\nEscolha uma opção: ");

                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    input = 0;
                }
                Console.Clear();

                switch (input)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        input = quantias[input];
                        opcao = 6;
                        Console.Clear();
                        break;
                    case 6:
                        opcao = 6;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        tela.ImprimirMensagem("Opção inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }

            if (input != 6)
            {
                Validadores validador = new Validadores();
                if (validador.ValidarUsuario(tela, contaLogada, baseDeDados))
                {
                    String mensagem = saque.EfetuarSaque(contaLogada, baseDeDados, input, compartimentoDeSaque);
                    tela.ImprimirMensagem(mensagem);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    tela.ImprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                tela.ImprimirMensagem("Cancelando operação...");
                Console.ReadKey();
            }

        }
        public void MenuDeposito(int contaLogada, Tela tela, BaseDeDados baseDeDados, CompartimentoDeSaque compartimentoDeSaque)
        {
            int tipoDeposito = 0;
            int input;
            while (tipoDeposito == 0)
            {
                Console.Clear();
                tela.ImprimirMensagem("Selecione o tipo de depósito: ");
                tela.ImprimirMensagem("1 - Cheque");
                tela.ImprimirMensagem("2 - Dinheiro");
                tela.ImprimirMensagem("3 - Cancelar operação");

                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    input = 0;
                }

                switch (input)
                {
                    case 1:
                        if (baseDeDados.getTipoConta(contaLogada).Equals(1))
                        {
                            Console.Clear();
                            tela.ImprimirMensagem("Contas universitárias não podem efetuar depósito em cheque.");
                            Console.ReadKey();
                            Console.Clear();
                            tipoDeposito = 0;
                        }
                        else
                        {
                            tipoDeposito = 1;
                        }
                        break;
                    case 2:
                        tipoDeposito = 2;
                        break;
                    case 3:
                        tipoDeposito = 3;
                        break;
                    default:
                        Console.Clear();
                        tela.ImprimirMensagem("Opção Inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }

            if (tipoDeposito != 3)
            {
                Console.Clear();
                tela.ImprimirMensagem("Por favor, insira uma quantia em R$ (ou 0 para cancelar): ");
                input = validador.ValidarInputMenu(tela, Console.ReadLine());

                if (input != 0)
                {
                    Validadores validador = new Validadores();
                    Console.Clear();
                    if (validador.ValidarUsuario(tela, contaLogada, baseDeDados))
                    {
                        tela.ImprimirMensagem("Por favor, insira um envelope contendo " + tela.converterValor(input) + ".");
                        Console.ReadKey();
                        Console.Clear();
                        Deposito deposito = new Deposito();
                        Console.Clear();
                        String retornoDeposito = deposito.RealizarDeposito(contaLogada, baseDeDados, compartimentoDeSaque, tipoDeposito, input);
                        Console.Clear();
                        tela.ImprimirMensagem(retornoDeposito);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        tela.ImprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
        public void MenuExtrato(int contaLogada, Tela tela, BaseDeDados baseDeDados)
        {
            int intervaloExtrato = 0;
            bool validaIntervalo = false;

            while (!validaIntervalo)
            {
                Console.Clear();
                tela.ImprimirMensagem("Digite o intervalo de dias que deseja visualizar do seu extrato (0 para ver todo extrato): ");
                try
                {
                    intervaloExtrato = Convert.ToInt32(Console.ReadLine());
                    if (intervaloExtrato < 0)
                    {
                        Console.Clear();
                        tela.ImprimirMensagem("O valor não pode ser negativo.");
                        Console.ReadKey();
                    }
                    else
                    {
                        validaIntervalo = true;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    tela.ImprimirMensagem("O valor digitado é inválido.");
                    Console.ReadKey();
                }
            }

            ConsultaExtrato consultaExtrato = new ConsultaExtrato();
            Console.Clear();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(contaLogada, baseDeDados, tela, intervaloExtrato);
            if (!retExtrato.Ok)
            {
                tela.ImprimirMensagem(retExtrato.Mensagem.ToString());
            }

            tela.ExibirExtrato(retExtrato.Dados);

            Console.ReadKey();
            Console.Clear();
        }
        public void MenuEmprestimo(int contaLogada, Tela tela, BaseDeDados baseDeDados)
        {
            int inputMenu = 0;

            #region Menu Empréstimo

            while (inputMenu == 0)
            {
                Console.Clear();
                tela.ImprimirMensagem("Selecione a opção desejada: ");
                tela.ImprimirMensagem("1 - Solicitar Empréstimo");
                tela.ImprimirMensagem("2 - Pagar Parcela do Empréstimo");
                tela.ImprimirMensagem("3 - Cancelar operação");

                try
                {
                    inputMenu = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    inputMenu = 0;
                }

                switch (inputMenu)
                {
                    case 1:
                    case 2:
                    case 3:
                        break;
                    default:
                        Console.Clear();
                        tela.ImprimirMensagem("Opção Inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            #endregion

            #region Solicitar Empréstimo
            double valorMaxEmprestimo;
            double valorEmprestimo;
            double taxaJuros = 5;

            if (inputMenu == 1)
            {
                Emprestimo svEmprestimo = new Emprestimo();
                valorMaxEmprestimo = baseDeDados.getVlrDispEmprestimo(contaLogada);

                Console.Clear();
                tela.ImprimirMensagem(string.Concat("Valor disponível para empréstimo: ", valorMaxEmprestimo.ToString("C")));
                tela.ImprimirMensagem("Taxa de juros: 5%");
                tela.ImprimirMensagem("Por favor, insira uma quantia desejada em R$ (ou 0 para cancelar): ");
                valorEmprestimo = validador.ValidarInputMenu(tela, Console.ReadLine());

                if (valorEmprestimo != 0)
                {
                    #region RN: Valida se valor do empréstimo é permitido
                    if (!svEmprestimo.VeririficaValorEmprestimo(valorEmprestimo, valorMaxEmprestimo))
                    {
                        Console.Clear();
                        tela.ImprimirMensagem(string.Concat("O valor do empréstimo deve ser positivo e menor que ", valorMaxEmprestimo.ToString("C"), ". A transação será cancelada."));
                        Console.ReadKey();
                        Console.Clear();
                        valorEmprestimo = 0;
                    }
                    #endregion

                    tela.ImprimirMensagem("\nPor favor, insira um prazo para o empréstimo (prazo máximo de 12 meses): ");
                    int nrParcelas = validador.ValidarInputMenu(tela, Console.ReadLine());

                    if (nrParcelas > 0)
                    {
                        #region RN: Valida prazo do empréstimo
                        if (!svEmprestimo.VeririficaPrazoEmprestimo(nrParcelas))
                        {
                            Console.Clear();
                            tela.ImprimirMensagem(string.Concat("O prazo deve ser positivo de no máximo 12 meses. A transação será cancelada."));
                            Console.ReadKey();
                            Console.Clear();
                            valorEmprestimo = 0;
                        }
                        #endregion

                        Console.Clear();
                        if (validador.ValidarUsuario(tela, contaLogada, baseDeDados))
                        {
                            string retornoEmprestimo = svEmprestimo.RealizarEmprestimo(tela, contaLogada, baseDeDados, valorEmprestimo, taxaJuros, nrParcelas);
                            Console.Clear();
                            tela.ImprimirMensagem(retornoEmprestimo);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            tela.ImprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
            }
            #endregion

            #region Pagamento das Parcelas
            if (inputMenu == 2)
            {
                //TODO: Pagamento das parcelas do empréstimo
            }
            #endregion
        }
    }
}