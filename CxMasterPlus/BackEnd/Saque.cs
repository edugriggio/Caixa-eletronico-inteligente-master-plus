using System;

namespace CxMasterPlus
{
    public class Saque
    {

        public String EfetuarSaque(int nrConta, BaseDeDados baseDeDados, int qtd, CompartimentoDeSaque compartimentoDeSaque)
        {
            try
            {
                double saldoDisponivel;

                if (baseDeDados.getLimiteDiario(nrConta, (DateTime.Today.DayOfWeek).ToString()) == 0)
                {
                    return "Valor excede o limite diário conforme o seu tipo de conta.";
                }

                saldoDisponivel = baseDeDados.RetornaSaldoDisponivel(nrConta);
                if (saldoDisponivel < 20)
                {
                    return "Saldo disponível em conta é menor que a nota de menor valor disponível no caixa eletrônico.";
                }

                if (qtd < saldoDisponivel)
                {
                    if (compartimentoDeSaque.TemSaldoSuficiente(qtd))
                    {
                        if (baseDeDados.getLimiteDiario(nrConta, (DateTime.Today.DayOfWeek).ToString()) >= qtd)
                        {
                            baseDeDados.DebitarValor(nrConta, qtd);

                            compartimentoDeSaque.DispensarDinheiro(qtd);
                            baseDeDados.SubtrairLimiteDiario(nrConta, qtd, (DateTime.Today.DayOfWeek).ToString());
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
