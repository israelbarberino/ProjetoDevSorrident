using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DevSorrident.Models;
using MySql.Data.MySqlClient;
using System.Web.Mvc;

namespace DevSorrident.Dados
{
    public class AcoesEspecialidade
    {
        Conexao con = new Conexao();
        public void inserirEspecialidade(ModelEspecialidade cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEspecialidade values(default,@especialidade)", con.MyConectarBD());

            cmd.Parameters.Add("@especialidade", MySqlDbType.VarChar).Value = cm.especialidade;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<ModelEspecialidade> GetEspecialidade()
        {
            List<ModelEspecialidade> EspecialidadeList = new List<ModelEspecialidade>();

            MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade order by codEsp", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                EspecialidadeList.Add(
                    new ModelEspecialidade
                    {
                        codEsp = Convert.ToString(dr["codEsp"]),
                        especialidade = Convert.ToString(dr["Especialidade"])
                    });
            }
            return EspecialidadeList;
        }

        public bool DeleteEspecialidade(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbEspecialidade where codEsp=@id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool AtualizaEspecialidade(ModelEspecialidade cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbEspecialidade set especialidade=@especialidade where codEsp=@codEsp", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@Especialidade", cm.especialidade);
            cmd.Parameters.AddWithValue("@codEsp", cm.codEsp);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}