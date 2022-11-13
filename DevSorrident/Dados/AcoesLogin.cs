using DevSorrident.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace DevSorrident.Dados
{
    public class AcoesLogin
    {
        Conexao con = new Conexao();

        public void TestarUsuario(ModelLogin user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @usuario and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                }
            }
            else
            {
                user.usuario = null;
                user.senha = null;
                user.tipo = null;
            }
            con.MyDesconectarBD();
        }

        public void InserirLogin(ModelLogin cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbLogin (usuario, senha, tipo) values (@usuario, @senha, @tipo)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cm.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cm.tipo;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable CarregaLogin()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Login = new DataTable();
            da.Fill(Login);
            con.MyDesconectarBD();
            return Login;
        }
    }
}