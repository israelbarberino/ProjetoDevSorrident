using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DevSorrident.Models;
using MySql.Data.MySqlClient;

namespace DevSorrident.Dados
{
    public class AcoesAtendimento
    {
        Conexao con = new Conexao();

        public void testarAtendimento(ModelAtendimento atendimento)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAten = @dataAten and horaDen = @horaDen ", con.MyConectarBD());

            cmd.Parameters.Add("@dataAten", MySqlDbType.VarChar).Value = atendimento.dataAten;
            cmd.Parameters.Add("@horaDen", MySqlDbType.VarChar).Value = atendimento.horaDen;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    atendimento.confAtendimento = "0";
                }
            }
            else
            {
                atendimento.confAtendimento = "1";
            }
            con.MyDesconectarBD();
        }

        public void inserirAtendimento(ModelAtendimento cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento (dataAten,horaDen,codPac,codDen) values (@dataAten, @horaDen, @codPac, @codDen)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@dataAten", MySqlDbType.VarChar).Value = cm.dataAten;
            cmd.Parameters.Add("@horaDen", MySqlDbType.VarChar).Value = cm.horaDen;
            cmd.Parameters.Add("@codPac", MySqlDbType.VarChar).Value = cm.codPac;
            cmd.Parameters.Add("@codDen", MySqlDbType.VarChar).Value = cm.codDen;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable CarregaAtendimento()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_ListaAtendimento", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable Atendimento = new DataTable();
            da.Fill(Atendimento);
            con.MyDesconectarBD();
            return Atendimento;
        }
    }
}