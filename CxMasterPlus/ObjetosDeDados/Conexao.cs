using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CxMasterPlus
{
    class Conexao
    {
        SqlConnection con = new SqlConnection();
        public Conexao()
        {
            con.ConnectionString = "Data Source=masterplus.c9o3wijgqblh.sa-east-1.rds.amazonaws.com;Initial Catalog=cxmasterplus;Persist Security Info=True;User ID=admin;Password=engenharia2020";
        }

        public SqlConnection conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public SqlConnection desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }

        public SqlTransaction beginTransaction()
        {
            return con.BeginTransaction("SampleTransaction");
        }
    }
}