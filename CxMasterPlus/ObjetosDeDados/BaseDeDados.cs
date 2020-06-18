using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Transactions;

namespace CxMasterPlus
{

    public class BaseDeDados
    {
        Conexao con = new Conexao();
        SqlDataReader reader;

        public Conta obterConta(Conta conta)
        {
            String sql = "SELECT COD_CONTA, TIPO_CONTA, SALDO, LIMITE, LIMITE_SAQUE FROM CONTA WHERE COD_CONTA = " + conta.NumeroConta;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Conta
                    {
                        NumeroConta = int.Parse(reader[0].ToString()),
                        ValorDisponivel = double.Parse(reader[2].ToString()),
                        TipoConta = int.Parse(reader[1].ToString()),
                        LimiteDiario = double.Parse(reader[4].ToString()),
                        ValorDisponivelEmprestimo = double.Parse(reader[3].ToString())
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.desconectar();
            }
        }

        public Conta AutenticarUsuario(Conta conta)
        {
            String sql = "SELECT COD_CONTA, TIPO_CONTA FROM CONTA WHERE COD_CONTA = " + conta.NumeroConta + " AND SENHA = " + conta.Senha;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Conta
                    {
                        NumeroConta = int.Parse(reader[0].ToString()),
                        TipoConta = int.Parse(reader[1].ToString()),
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.desconectar();
            }
        }

        public double RetornaSaldoDisponivel(Conta conta)
        {
            return obterConta(conta).ValorDisponivel;
        }

        public Double[] ObterSaldoCaixa()
        {
            String sql = "SELECT SALDO_CAIXA, MARGEM_SEGURA, CHEQUE FROM CAIXA";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Double[3] { Double.Parse(reader[0].ToString()), Double.Parse(reader[1].ToString()), Double.Parse(reader[2].ToString()) };
                }
                else
                {
                    return new Double[3] { 0, 0, 0 };
                }
            }
            catch (Exception)
            {
                return new Double[3] { 0, 0, 0 };
            }
            finally
            {
                con.desconectar();
            }
        }

        public Boolean CreditarValor(Conta conta, double valor, Operacao op, int tipoDeposito)
        {
            Double[] saldoCaixa = ObterSaldoCaixa();
            Conta atual = obterConta(conta);
            string valorOp = (atual.ValorDisponivel + valor).ToString().Replace(".", "").Replace(",", ".");

            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction;
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            transaction = con.beginTransaction();
            cmd.Transaction = transaction;

            try
            {
                string sqlUpdateSaldoCaixa = null;
                if (tipoDeposito == 2)
                {
                    sqlUpdateSaldoCaixa = "UPDATE CAIXA SET SALDO_CAIXA = " + (saldoCaixa[0] + valor * 0.8).ToString().Replace(".", "").Replace(",", ".") +
                    ", MARGEM_SEGURA = " + (saldoCaixa[1] + valor * 0.2).ToString().Replace(".", "").Replace(",", ".");
                }
                else
                {
                    sqlUpdateSaldoCaixa = "UPDATE CAIXA SET CHEQUE = " + (saldoCaixa[2] + valor).ToString().Replace(".", "").Replace(",", ".");
                }
                string sqlUpdate = "UPDATE CONTA SET SALDO = " + valorOp + " WHERE COD_CONTA = " + conta.NumeroConta;
                string sqlInsert = "INSERT INTO EXTRATO(TIPO_MOVIMENTACAO, DATA_MOVIMENTACAO, VALOR, CONTA_ORIGEM, CONTA_DESTINO) " +
                "VALUES ('" + (Char)op + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," + valor + "," + conta.NumeroConta + "," + conta.NumeroConta + ")";

                cmd.CommandText = sqlUpdateSaldoCaixa;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInsert;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlUpdate;
                cmd.ExecuteNonQuery();

                // Attempt to commit the transaction.
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    con.desconectar();
                }
            }
            finally
            {
                con.desconectar();
            }
        }

        public Boolean realizarEmprestimo(Conta conta, double valor, double valorParcela, int nrParcelas, double taxa, Operacao op)
        {
            Conta atual = obterConta(conta);
            string valorCredito = (atual.ValorDisponivel + valor).ToString().Replace(".", "").Replace(",", ".");
            string valorEmprestimo = (atual.ValorDisponivelEmprestimo - valor).ToString().Replace(".", "").Replace(",", ".");

            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction;
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            transaction = con.beginTransaction();
            cmd.Transaction = transaction;

            try
            {
                string sqlConta = "UPDATE CONTA SET SALDO = " + valorCredito + ", LIMITE = " + valorEmprestimo + "  WHERE COD_CONTA = " + conta.NumeroConta;
                string sqlExtrato = "INSERT INTO EXTRATO(TIPO_MOVIMENTACAO, DATA_MOVIMENTACAO, VALOR, CONTA_ORIGEM, CONTA_DESTINO) " +
                "VALUES ('" + (char)op + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', " + valor + ", " + conta.NumeroConta + "," + conta.NumeroConta + ")";
                string sqlInsertEmprestimo = "INSERT INTO EMPRESTIMO (VALOR_TOTAL, DATA_SOLICITACAO, PROXIMA_PARCELA, VALOR_PARCELA, NR_PARCELAS, NR_PARCELAS_RESTANTES, TAXA_JUROS, IS_ACTIVE, COD_CONTA) " +
                "VALUES(" + valor.ToString().Replace(".", "").Replace(",", ".") + ", '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy") + "', " + valorParcela.ToString().Replace(".", "").Replace(",", ".") +
                ", " + nrParcelas + ", " + nrParcelas + ", " + taxa.ToString().Replace(",", ".") + ", 'S', " + conta.NumeroConta + ")";


                cmd.CommandText = sqlExtrato;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlConta;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInsertEmprestimo;
                cmd.ExecuteNonQuery();
                // Attempt to commit the transaction.
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    con.desconectar();
                }
            }
            finally
            {
                con.desconectar();
            }
        }

        public Boolean DebitarValor(Conta conta, double valor, Operacao op)
        {
            string saldoCaixa = (ObterSaldoCaixa()[0] + valor).ToString().Replace(".", "").Replace(",", ".");
            Conta atual = obterConta(conta);
            string valorOp = (atual.ValorDisponivel - valor).ToString().Replace(".", "").Replace(",", ".");
            string valorSaque = (atual.LimiteDiario - valor).ToString().Replace(".", "").Replace(",", ".");

            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction;
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            transaction = con.beginTransaction();
            cmd.Transaction = transaction;

            try
            {
                string updateSaldoCaixa = "UPDATE CAIXA SET SALDO_CAIXA = " + saldoCaixa;
                string sqlUpdate = "UPDATE CONTA SET SALDO = " + valorOp + ", LIMITE_SAQUE = " + valorSaque + " WHERE COD_CONTA = " + conta.NumeroConta;
                string sqlInsert = "INSERT INTO EXTRATO(TIPO_MOVIMENTACAO, DATA_MOVIMENTACAO, VALOR, CONTA_ORIGEM, CONTA_DESTINO) " +
                "VALUES ('" + (char)op + "','" + DateTime.Now.ToString("dd/MM/yyyy") + "'," + valor + "," + conta.NumeroConta + "," + conta.NumeroConta + ")";

                cmd.CommandText = updateSaldoCaixa;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInsert;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlUpdate;
                cmd.ExecuteNonQuery();

                // Attempt to commit the transaction.
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    con.desconectar();
                }
            }
            finally
            {
                con.desconectar();
            }
        }

        public Boolean PagamentoParcela(Conta conta, double valorParcela, Transacao parcela, Operacao op)
        {
            Conta atual = obterConta(conta);

            string valorEmprestimo = (atual.ValorDisponivelEmprestimo + parcela.Valor).ToString().Replace(".", "").Replace(",", ".");
            string saldoConta = (atual.ValorDisponivel - valorParcela).ToString().Replace(".", "").Replace(",", ".");

            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction;
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            transaction = con.beginTransaction();
            cmd.Transaction = transaction;

            try
            {
                string sqlUpdate = "UPDATE CONTA SET LIMITE = " + valorEmprestimo + ", SALDO = " + saldoConta + " WHERE COD_CONTA = " + conta.NumeroConta;
                string sqlInsert = "INSERT INTO EXTRATO(TIPO_MOVIMENTACAO, DATA_MOVIMENTACAO, VALOR, CONTA_ORIGEM, CONTA_DESTINO) " +
                "VALUES ('" + (Char)op + "', '" + DateTime.Now.ToString("dd/MM/yyyy") + "'," + valorParcela.ToString().Replace(".", "").Replace(",", ".") + "," + conta.NumeroConta + "," + conta.NumeroConta + ")";

                string updateParcela;
                if (parcela.ParcelasRestantes == 1)
                {
                    updateParcela = "UPDATE EMPRESTIMO SET NR_PARCELAS_RESTANTES = " + (parcela.ParcelasRestantes - 1) + ", PROXIMA_PARCELA = '', IS_ACTIVE = 'N' WHERE COD_CONTA = " + conta.NumeroConta + " AND IS_ACTIVE = 'S' AND ID_TRANSACAO = " + parcela.Id;
                }
                else
                {
                    updateParcela = "UPDATE EMPRESTIMO SET NR_PARCELAS_RESTANTES = " + (parcela.ParcelasRestantes - 1) + ", PROXIMA_PARCELA = '" + parcela.ProximaParcela.AddMonths(1).ToShortDateString() +
                "' WHERE COD_CONTA = " + conta.NumeroConta + " AND IS_ACTIVE = 'S' AND ID_TRANSACAO = " + parcela.Id;
                }

                cmd.CommandText = updateParcela;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlInsert;
                cmd.ExecuteNonQuery();
                cmd.CommandText = sqlUpdate;
                cmd.ExecuteNonQuery();

                // Attempt to commit the transaction.
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    con.desconectar();
                }
            }
            finally
            {
                con.desconectar();
            }
        }

        public double getLimiteDiario(Conta conta, String date)
        {
            return obterConta(conta).LimiteDiario;
        }

        public List<Transacao> getHistoricoTransacoes(Conta conta)
        {
            String sql = "SELECT TIPO_MOVIMENTACAO, DATA_MOVIMENTACAO, VALOR, CONTA_ORIGEM, CONTA_DESTINO FROM EXTRATO WHERE CONTA_ORIGEM = " + conta.NumeroConta;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            try
            {
                reader = cmd.ExecuteReader();

                List<Transacao> transacoes = new List<Transacao>();

                while (reader.Read())
                {
                    transacoes.Add(new Transacao
                    {
                        DataTransacao = DateTime.ParseExact(reader.GetString(reader.GetOrdinal("DATA_MOVIMENTACAO")).ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR")),
                        Operacao = (Operacao)reader.GetString(reader.GetOrdinal("TIPO_MOVIMENTACAO")).ToString()[0],
                        Valor = Double.Parse(reader.GetDecimal(reader.GetOrdinal("VALOR")).ToString())
                    });
                }
                return transacoes;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                con.desconectar();
            }
        }

        public List<Transacao> obterEmprestimo(Conta conta)
        {
            List<Transacao> lista = new List<Transacao>();
            String sql = "SELECT VALOR_TOTAL, DATA_SOLICITACAO, PROXIMA_PARCELA, VALOR_PARCELA, NR_PARCELAS, NR_PARCELAS_RESTANTES, IS_ACTIVE, TAXA_JUROS, ID_TRANSACAO FROM EMPRESTIMO WHERE IS_ACTIVE = 'S' AND COD_CONTA = " + conta.NumeroConta;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con.conectar();
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Transacao
                    {
                        VlrTotalEmprestimo = Double.Parse(reader[0].ToString()),
                        DataTransacao = DateTime.ParseExact(reader[1].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR")),
                        ProximaParcela = DateTime.ParseExact(reader[2].ToString(), "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("pt-BR")),
                        Valor = Double.Parse(reader[3].ToString()),
                        ParcelasRestantes = int.Parse(reader[5].ToString()),
                        NrTotalParcelas = int.Parse(reader[4].ToString()),
                        IsActive = reader[6].ToString()[0],
                        Taxa = Double.Parse(reader[7].ToString()),
                        NrParcela = (int.Parse(reader[4].ToString()) - int.Parse(reader[5].ToString())) + 1,
                        Id = int.Parse(reader[8].ToString())
                    });
                }
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.desconectar();
            }
        }

        public double getVlrDispEmprestimo(Conta conta)
        {
            return obterConta(conta).ValorDisponivelEmprestimo;
        }
    }

}