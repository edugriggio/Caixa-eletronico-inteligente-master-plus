using System;

namespace CxMasterPlus
{
    class CaixaEletronico
    {
        private bool usuarioAutenticado;
        private Conta contaLogada;
        private Tela tela;
        private BaseDeDados baseDeDados;

        public CaixaEletronico()
        {
            usuarioAutenticado = false;
            contaLogada = null;
            tela = new Tela();
            baseDeDados = new BaseDeDados();
        }

        public void executar()
        {
            while (true)
            {
                while (!usuarioAutenticado)
                {
                    autenticarUsuario();
                }

                executarTransacao();
                usuarioAutenticado = false;
                contaLogada = null;
                tela.ImprimirMensagem("Obrigado por utilizar o Caixa Master Plus. Até logo!");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void autenticarUsuario()
        {
            Conta conta = new Conta();
            conta.NumeroConta = 0;
            conta.Senha = 0;
            tela.ImprimirMensagem("Por favor digite o número da sua conta: ");
            try
            {
                conta.NumeroConta = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                conta.NumeroConta = 0;
            }

            tela.ImprimirMensagem("\nDigite sua senha: ");
            try
            {
                conta.Senha = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                conta.Senha = 0;
            }

            contaLogada = baseDeDados.AutenticarUsuario(conta);
            if (contaLogada != null)
            {
                usuarioAutenticado = true;
                Console.Clear();
            }
            else
            {
                Console.Clear();
                tela.ImprimirMensagem("Número da conta ou senha inválidos. Por Favor, tente novamente.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void executarTransacao()
        {
            bool usuarioDeslogado = false;

            while (!usuarioDeslogado)
            {

                int selecao = exibirMenuPrincipal();
                
                switch (selecao)
                {
                    case 1:
                        criarOperacao(1);
                        break;
                    case 2:
                        criarOperacao(2);
                        break;
                    case 3:
                        criarOperacao(3);
                        break;
                    case 4:
                        criarOperacao(4);
                        break;
                    case 5:
                        Console.Clear();
                        tela.ImprimirMensagem("Saindo do sistema...");
                        usuarioDeslogado = true;
                        break;
                    default:
                        Console.Clear();
                        tela.ImprimirMensagem("Você não digitou uma opção válida. Tente novamente.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void criarOperacao(int x)
        {
            MenuOperacoes operacao = new MenuOperacoes();

            switch (x)
            {
                case 1:
                    operacao.MenuExtrato(contaLogada, tela, baseDeDados);
                    break;
                case 2:
                    operacao.MenuSaque(contaLogada, tela, baseDeDados);
                    break;
                case 3:
                    operacao.MenuDeposito(contaLogada, tela, baseDeDados);
                    break;
                case 4:
                    operacao.MenuEmprestimo(contaLogada, tela, baseDeDados);
                    break;
            }
        }

        private int exibirMenuPrincipal()
        {
            Console.Clear();
            tela.ImprimirMensagem("Menu Principal:");
            tela.ImprimirMensagem("1 - Consulta de Extrato");
            tela.ImprimirMensagem("2 - Sacar");
            tela.ImprimirMensagem("3 - Depositar");
            tela.ImprimirMensagem("4 - Empréstimo");
            tela.ImprimirMensagem("5 - Sair\n");
            tela.ImprimirMensagem("Escolha uma opção: ");
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
