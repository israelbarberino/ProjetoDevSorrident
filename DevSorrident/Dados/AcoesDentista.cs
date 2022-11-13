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
    public class AcoesDentista
    {
        Conexao con = new Conexao();
        public void inserirDentista(ModelDentista cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbDentista values(default,@nomeDen, @codEsp)", con.MyConectarBD());

            cmd.Parameters.Add("@nomeDen", MySqlDbType.VarChar).Value = cm.nomeDen;
            cmd.Parameters.Add("@codEsp", MySqlDbType.VarChar).Value = cm.codEsp;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelDentista> GetDentista()
        {
            List<ModelDentista> DentistaList = new List<ModelDentista>();
            MySqlCommand cmd = new MySqlCommand("select tbDentista.codDen, tbDentista.nomeDen, tbEspecialidade.Especialidade from tbDentista INNER JOIN tbEspecialidade on tbEspecialidade.codEsp = tbDentista.codEsp;", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                DentistaList.Add(
                    new ModelDentista
                    {
                        codDen = Convert.ToString(dr["codDen"]),
                        nomeDen = Convert.ToString(dr["nomeDen"]),
                        especialidade = Convert.ToString(dr["especialidade"])
                    });
            }
            return DentistaList;
        }

        public bool DeleteDentista(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbDentista where codDen=@id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool AtualizaDentista(ModelDentista cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbDentista set nomeDen=@nomeDen, codEsp=@codEsp where codDen=@codDen", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@nomeDen", cm.nomeDen);
            cmd.Parameters.AddWithValue("@codEsp", cm.codEsp);
            cmd.Parameters.AddWithValue("@codDen", cm.codDen);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}