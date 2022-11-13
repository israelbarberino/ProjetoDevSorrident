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
    public class AcoesPaciente
    {
        Conexao con = new Conexao();
        public void inserirPaciente(ModelPaciente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPaciente values(default, @nomePac, @cpfPac, @cepPac, @emailPac, @telefonePac, @sexoPac)", con.MyConectarBD());

            cmd.Parameters.Add("@nomePac", MySqlDbType.VarChar).Value = cm.nomePac;
            cmd.Parameters.Add("@cpfPac", MySqlDbType.VarChar).Value = cm.cpfPac;
            cmd.Parameters.Add("@cepPac", MySqlDbType.VarChar).Value = cm.cepPac;
            cmd.Parameters.Add("@emailPac", MySqlDbType.VarChar).Value = cm.emailPac;
            cmd.Parameters.Add("@telefonePac", MySqlDbType.VarChar).Value = cm.telefonePac;
            cmd.Parameters.Add("@sexoPac", MySqlDbType.VarChar).Value = cm.sexoPac;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }


        public List<ModelPaciente> GetPaciente()
        {
            List<ModelPaciente> PacienteList = new List<ModelPaciente>();

            MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                PacienteList.Add(
                    new ModelPaciente
                    {
                        codPac = Convert.ToString(dr["codPac"]),
                        nomePac = Convert.ToString(dr["nomePac"]),
                        cpfPac = Convert.ToString(dr["cpfPac"]),
                        cepPac = Convert.ToString(dr["cepPac"]),
                        telefonePac = Convert.ToString(dr["telefonePac"]),
                        emailPac = Convert.ToString(dr["emailPac"]),
                    });
            }
            return PacienteList;
        }
        public bool DeletePaciente(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbPaciente where codPac=@id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool AtualizaPaciente(ModelPaciente cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbPaciente set nomePac=@nomePac, cpfPac=@cpfPac, cepPac=@cepPac, telefonePac=@telefonePac, emailPac=@emailPac where codPac=@codPac", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@nomePac", cm.nomePac);
            cmd.Parameters.AddWithValue("@cpfPac", cm.cpfPac);
            cmd.Parameters.AddWithValue("@cepPac", cm.cepPac);
            cmd.Parameters.AddWithValue("@telefonePac", cm.telefonePac);
            cmd.Parameters.AddWithValue("@emailPac", cm.emailPac);
            cmd.Parameters.AddWithValue("@codPac", cm.codPac);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}