using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Saque : Operacao
    {
        private int qtd;
        private CompartimentoDeSaque compartimentoDeSaque;
        private static readonly int CANCELADO = 6;
        private readonly DateTime date = new DateTime();

        public Saque(int conta, BaseDeDados dados, Tela tela, CompartimentoDeSaque compartimentoDeSaque) : base(conta, dados, tela, compartimentoDeSaque)
        {
            this.compartimentoDeSaque = compartimentoDeSaque;
            this.execute();
        }

        public override void execute()
        {
            bool dinheiroEntregue = false;
            double saldoDisponivel;

            BaseDeDados baseDeDados = getBaseDeDados();
            Tela tela = getTela();

            do
            {
                if (baseDeDados.getLimiteDiario(getNrConta(), (date.DayOfWeek - 1).ToString()) == 0)
                {
                    Console.Clear();
                    tela.imprimirMensagem("Você já efetuou o limite de saque diário para o seu tipo de conta.");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                saldoDisponivel = baseDeDados.retornaSaldoDisponivel(getNrConta());
                if (saldoDisponivel >= 20)
                {
                    qtd = exibirMenuDeValores();
                }
                else
                {
                    Console.Clear();
                    tela.imprimirMensagem("Você não possui saldo disponível em conta superior ao valor mínimo de saque. Desta forma, não será possível realizar esta operação.");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }


                if (qtd != CANCELADO)
                {
                    if (qtd <= saldoDisponivel)
                    {
                        if (compartimentoDeSaque.temSaldoSuficiente(qtd))
                        {
                            if (baseDeDados.getLimiteDiario(getNrConta(), (date.DayOfWeek - 1).ToString()) >= qtd)
                            {
                                baseDeDados.debitarValor(getNrConta(), qtd);

                                compartimentoDeSaque.dispensarDinheiro(qtd);
                                dinheiroEntregue = true;

                                tela.imprimirMensagem("Transação realizada.\nPor favor, retire seu dinheiro.");
                                baseDeDados.subtrairLimiteDiario(getNrConta(), qtd, (date.DayOfWeek - 1).ToString());
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                tela.imprimirMensagem("Valor ultrapassa seu limite de saque diário.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                        else
                        {
                            tela.imprimirMensagem("Valor indisponível no Caixa eletrônico." +
                            "\n\nPor favor, escolha um valor menor.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        tela.imprimirMensagem("Você não possui saldo suficiente em sua conta." +
                        "\n\nPor favor, escolha um valor menor.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    tela.imprimirMensagem("Cancelando transação...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

            } while (!dinheiroEntregue);
        }

        private int exibirMenuDeValores()
        {
            int opcao = 0;
            Validador validador = new Validador();

            Tela tela = getTela();

            int[] quantias = { 0, 20, 50, 100, 200, 500 };
            int input;

            while (opcao == 0)
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
                        if (validador.validarUsuario(tela, getNrConta(), getBaseDeDados(), input))
                        {
                            opcao = quantias[input];
                            Console.Clear();
                        }
                        else
                        {
                            opcao = 6;
                        }
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
            return opcao;
        }
    }
}
