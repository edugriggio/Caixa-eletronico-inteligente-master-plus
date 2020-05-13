using System;
using System.Collections.Generic;
using System.Text;

namespace CxMasterPlus
{
    public class Deposito : Transacao
    {
        private double qtd;
        private static readonly int CANCELADO = 0;
        private double tipoDeposito = 0;
        private double valorDeposito = 0;

        public Deposito(int conta, BaseDeDados dados, Tela tela, CompartimentoDeSaque compartimentoDeSaque) : base(conta, dados, tela, compartimentoDeSaque)
        {
            this.execute();
        }

        public override void execute()
        {
            BaseDeDados baseDeDados = getBaseDeDados();
            Tela tela = getTela();

            qtd = this.menudeposito();

            if (qtd != CANCELADO)
            {
                Console.Clear();
                tela.imprimirMensagem("Por favor, insira um envelope contendo " + tela.converterValor(qtd) + ".");
                Console.ReadKey();
                Console.Clear();
                tela.imprimirMensagem("Transação efetivada.");
                Console.ReadKey();
                Console.Clear();
                baseDeDados.creditarValor(getNrConta(), qtd);
                if (tipoDeposito != 1)
                {
                    getCompartimentoDeSaque().adicionarNotas(qtd);
                }

            }
            else
            {
                Console.Clear();
                tela.imprimirMensagem("Cancelando transação...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public double solicitacaoDeDeposito()
        {
            int input;
            Tela tela = getTela();
            Console.Clear();
            Validador validador = new Validador();

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
                return 0;
            }

            Console.Clear();
            if (validador.validarUsuario(tela, getNrConta(), getBaseDeDados(), input))
            {
                return Convert.ToDouble(input / 100);
            }
            else
            {
                return 0;
            }
        }

        private double menudeposito()
        {
            int input;
            Tela tela = getTela();
            BaseDeDados baseDeDados = getBaseDeDados();
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
                        if (baseDeDados.getTipoConta(getNrConta()).Equals(1))
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
                            valorDeposito = solicitacaoDeDeposito();
                        }
                        break;
                    case 2:
                        tipoDeposito = 2;
                        valorDeposito = solicitacaoDeDeposito();
                        break;
                    case 3:
                        Console.Clear();
                        return 0;
                    default:
                        Console.Clear();
                        tela.imprimirMensagem("Opção Inválida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            return valorDeposito;
        }
    }
}
