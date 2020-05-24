using System;
using System.Collections.Generic;
using System.Text;
using FrameworkProjeto;

namespace CxMasterPlus
{
    public class MenuOperacoes
    {
        public void MenuSaque(int contaLogada, Tela tela, BaseDeDados baseDeDados, CompartimentoDeSaque compartimentoDeSaque)
        {
            int opcao = 0;
            int input = 0;
            int[] quantias = { 0, 20, 50, 100, 200, 500 };
            Saque saque = new Saque();

            while (opcao != 6)
            {
                Console.Clear();
                tela.imprimirMensagem("Menu de Saque:");
                tela.imprimirMensagem("1 - R$20");
                tela.imprimirMensagem("2 - R$50");
                tela.imprimirMensagem("3 - R$100");
                tela.imprimirMensagem("4 - R$200");
                tela.imprimirMensagem("5 - R$500");
                tela.imprimirMensagem("6 - Cancelar operação");
                tela.imprimirMensagem("\nEscolha uma opção: ");

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
                        tela.imprimirMensagem("Opção inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }

            if (input != 6)
            {
                Validador validador = new Validador();
                if (validador.validarUsuario(tela, contaLogada, baseDeDados))
                {
                    String mensagem = saque.EfetuarSaque(contaLogada, baseDeDados, input, compartimentoDeSaque);
                    tela.imprimirMensagem(mensagem);
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    tela.imprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                tela.imprimirMensagem("Cancelando operação...");
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
                tela.imprimirMensagem("Selecione o tipo de depósito: ");
                tela.imprimirMensagem("1 - Cheque");
                tela.imprimirMensagem("2 - Dinheiro");
                tela.imprimirMensagem("3 - Cancelar operação");

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
                            tela.imprimirMensagem("Contas universitárias não podem efetuar depósito em cheque.");
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
                        tela.imprimirMensagem("Opção Inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            if (tipoDeposito != 3)
            {
                Console.Clear();
                tela.imprimirMensagem("Por favor, insira uma quantia em R$ (ou 0 para cancelar): ");
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Clear();
                    tela.imprimirMensagem("O valor digitado é inválido. Sua Transação será cancelada.");
                    Console.ReadKey();
                    Console.Clear();
                    input = 0;
                }
                if (input != 0)
                {
                    Validador validador = new Validador();
                    Console.Clear();
                    if (validador.validarUsuario(tela, contaLogada, baseDeDados))
                    {
                        tela.imprimirMensagem("Por favor, insira um envelope contendo " + tela.converterValor(input) + ".");
                        Console.ReadKey();
                        Console.Clear();
                        Deposito deposito = new Deposito();
                        Console.Clear();
                        String retornoDeposito = deposito.realizarDeposito(contaLogada, baseDeDados, compartimentoDeSaque, tipoDeposito, input);
                        Console.Clear();
                        tela.imprimirMensagem(retornoDeposito);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        tela.imprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
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
                tela.imprimirMensagem("Digite o intervalo de dias que deseja visualizar do seu extrato (0 para ver todo extrato): ");
                try
                {
                    intervaloExtrato = Convert.ToInt32(Console.ReadLine());
                    if (intervaloExtrato < 0)
                    {
                        Console.Clear();
                        tela.imprimirMensagem("O valor não pode ser negativo.");
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
                    tela.imprimirMensagem("O valor digitado é inválido.");
                    Console.ReadKey();
                }
            }

            ConsultaExtrato consultaExtrato = new ConsultaExtrato();
            Console.Clear();

            Retorno<Extrato> retExtrato = consultaExtrato.GerarExtrato(contaLogada, baseDeDados, tela, intervaloExtrato);
            if (!retExtrato.Ok)
            {
                tela.imprimirMensagem(retExtrato.Mensagem.ToString());
            }
           
            tela.ExibirExtrato(retExtrato.Dados);

            Console.ReadKey();
            Console.Clear();
        }
    }
}