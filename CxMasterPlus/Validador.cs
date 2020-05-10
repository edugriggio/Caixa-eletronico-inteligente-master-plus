using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CxMasterPlus
{
    public class Validador
    {

        private bool usuarioAutenticado = false;
        private int tentativas = 3;
        private int senha;
        public Boolean validarUsuario(Tela tela, int conta, BaseDeDados baseDeDados, int opcao)
        {
            while (!usuarioAutenticado && tentativas > 0)
            {
                if (opcao != 0)
                {
                    tela.imprimirMensagem("Por favor, digite sua senha: ");
                    try
                    {
                        senha = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        senha = 0;
                    }
                    usuarioAutenticado = baseDeDados.autenticarUsuario(conta, senha);
                    tentativas--;
                    if (!usuarioAutenticado && tentativas > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Senha Inválida. Você possui mais " + tentativas + " tentativas.");
                    }
                    else if (!usuarioAutenticado && tentativas == 0)
                    {
                        Console.Clear();
                        tela.imprimirMensagem("Senha Inválida. Sua transação não poderá ser efetivada.");
                        Console.ReadKey();
                        Console.Clear();
                        return false;
                    }
                }
                else
                {
                    return usuarioAutenticado;
                }
            }
            return usuarioAutenticado;
        }
    }
}
