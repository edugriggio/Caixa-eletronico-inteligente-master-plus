using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CxMasterPlus
{
    public class Validadores
    {

        private bool usuarioAutenticado = false;
        private int tentativas = 3;
        public Boolean ValidarUsuario(Tela tela, Conta conta, BaseDeDados baseDeDados)
        {
            while (!usuarioAutenticado && tentativas > 0)
            {
                tela.ImprimirMensagem("Por favor, digite sua senha: ");
                try
                {
                    conta.Senha = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    conta.Senha = 0;
                }
                usuarioAutenticado = baseDeDados.AutenticarUsuario(conta) != null;
                tentativas--;
                if (!usuarioAutenticado && tentativas > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Senha Inválida. Você possui mais " + tentativas + " tentativas.");
                }
                else if (!usuarioAutenticado && tentativas == 0)
                {
                    Console.Clear();
                    return false;
                }
            }
            Console.Clear();
            return usuarioAutenticado;
        }

        public int ValidarInputMenu(Tela tela, string input)
        {
            int testaInput = 0;

            try
            {
                testaInput = Convert.ToInt32(input);
            }
            catch (Exception)
            {
                Console.Clear();
                tela.ImprimirMensagem("O valor digitado é inválido. Sua Transação será cancelada.");
                Console.ReadKey();
                Console.Clear();
                return 0;
            }

            return testaInput;
        }
    }
}