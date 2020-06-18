using System;

namespace CxMasterPlus
{
    public class Saque
    {

        public String EfetuarSaque(Conta conta, BaseDeDados baseDeDados, int qtd)
        {
            try
            {
                double saldoDisponivel;

                if (qtd <= 0)
                {
                    return "Valor inválido";
                }

                if (baseDeDados.getLimiteDiario(conta, (DateTime.Today.DayOfWeek).ToString()) == 0)
                {
                    return "Valor excede o limite diário conforme o seu tipo de conta.";
                }

                saldoDisponivel = baseDeDados.RetornaSaldoDisponivel(conta);
                if (saldoDisponivel < 20)
                {
                    return "Saldo disponível em conta é menor que a nota de menor valor disponível no caixa eletrônico.";
                }

                if (qtd < saldoDisponivel)
                {
                    if (baseDeDados.ObterSaldoCaixa()[0] > qtd)
                    {
                        if (baseDeDados.getLimiteDiario(conta, (DateTime.Today.DayOfWeek).ToString()) >= qtd)
                        {
                            if (baseDeDados.DebitarValor(conta, qtd, Operacao.Saque))
                            {
                                return "Transação realizada.\nPor favor, retire seu dinheiro.";
                            }
                            else
                            {
                                return "Problema ao efetuar sua transação. Tente novamente mais tarde.";
                            }
                        }
                        else
                        {
                            return "Valor ultrapassa seu limite de saque diário.";
                        }
                    }
                    else
                    {
                        return "Valor indisponível no Caixa eletrônico.\n\nPor favor, escolha um valor menor.";
                    }
                }
                else
                {
                    return "Você não possui saldo suficiente em sua conta.\n\nPor favor, escolha um valor menor.";
                }
            }
            catch (Exception)
            {
                return "Problema ao efetuar sua transação. Tente novamente mais tarde.";
            }
        }
    }
}
