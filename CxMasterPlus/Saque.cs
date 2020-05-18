using System;

namespace CxMasterPlus
{
    public class Saque
    {
        private readonly DateTime date = new DateTime();

        public String EfetuarSaque(int nrConta, BaseDeDados baseDeDados, int qtd, CompartimentoDeSaque compartimentoDeSaque)
        {
            try
            {
                double saldoDisponivel;

                if (baseDeDados.getLimiteDiario(nrConta, (date.DayOfWeek - 1).ToString()) == 0)
                {
                    return "Valor excede o limite diário conforme o seu tipo de conta.";
                }

                saldoDisponivel = baseDeDados.retornaSaldoDisponivel(nrConta);
                if (saldoDisponivel < 20)
                {
                    return "Saldo disponível em conta é menor que a nota de menor valor disponível no caixa eletrônico.";
                }

                if (qtd < saldoDisponivel)
                {
                    if (compartimentoDeSaque.temSaldoSuficiente(qtd))
                    {
                        if (baseDeDados.getLimiteDiario(nrConta, (date.DayOfWeek - 1).ToString()) >= qtd)
                        {
                            baseDeDados.debitarValor(nrConta, qtd);

                            compartimentoDeSaque.dispensarDinheiro(qtd);
                            baseDeDados.subtrairLimiteDiario(nrConta, qtd, (date.DayOfWeek - 1).ToString());
                            return "Transação realizada.\nPor favor, retire seu dinheiro.";
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
