using DevSorrident.Dados;
using DevSorrident.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace DevSorrident.Controllers
{
    public class AdmController : Controller
    {
        // GET: Adm
        /* --------------- MARCAÇÃO DOS BLOCOS COM COMENTÁRIOS NESSE ESTILO ---------------*/
        /* ----- SUB ITEM ----- */
        /* --- !!!!! PONTOS DE ATENÇÃO !!!!! ---*/


        /* --------------- INSTANCIAS DAS AÇÕES (DADOS) ---------------*/

        AcoesLogin acLogin = new AcoesLogin();
        AcoesPaciente acPac = new AcoesPaciente();        
        AcoesDentista acDen = new AcoesDentista();
        AcoesEspecialidade acEsp = new AcoesEspecialidade();
        AcoesAtendimento acAten = new AcoesAtendimento();

        /* --------------- MÉTODOS CONSULTANDO A DATABASE ---------------*/


        /* ----- PACIENTE ----- */
        public void CarregaPaciente()
        {
            List<SelectListItem> paciente = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdDevSorrident; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    paciente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.paciente = new SelectList(paciente, "Value", "Text");
        }

        /* ----- DENTISTA ----- */
        public void CarregaDentista()
        {
            List<SelectListItem> dentista = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdDevSorrident; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbDentista", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    dentista.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.dentista = new SelectList(dentista, "Value", "Text");
        }

        /* ----- ESPECIALIDADE ----- */

        public void CarregaEspecialidade()
        {
            List<SelectListItem> especialidade = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdDevSorrident; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    especialidade.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.especialidade = new SelectList(especialidade, "Value", "Text");
        }

        

        /* --------------- PÁGINA INICIAL - INDEX/HOME PAGE ---------------*/
        public ActionResult Index()
        {
            return View();
        }

        /* --------------- MÉTODOS PARA CADASTRO ---------------*/

        /* ----- PACIENTE ----- */
        public ActionResult CadPaciente()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();

            }
        }
        [HttpPost]
        public ActionResult CadPaciente(ModelPaciente cm)
        {
            acPac.inserirPaciente(cm);
            ViewBag.msg = "Cadastro efetuado com sucesso";

            return RedirectToAction("CadPaciente", "Adm");
        }

        /* ----- END PACIENTE ----- */

        /* ----- ESPECIALIDADE ----- */
        public ActionResult CadEspecialidade()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult CadEspecialidade(ModelEspecialidade cm)
        {
            acEsp.inserirEspecialidade(cm);
            ViewBag.msg = "Cadastro realizado com sucesso!";

            return View();
        }
        /* ----- END ESPECIALIDADE ----- */

        /* ----- DENTISTA ----- */
        public ActionResult CadDentista()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                CarregaEspecialidade();
                return View();
            }
        }
        [HttpPost]
        public ActionResult CadDentista(ModelDentista cm)
        {
            CarregaEspecialidade();
            cm.codEsp = Request["especialidade"];
            acDen.inserirDentista(cm);
            ViewBag.msg = "Cadastro realizado com sucesso!";
            return View();
        }
        /* ----- END DENTISTA ----- */

        /* ----- ATENDIMENTO ----- */
        public ActionResult CadAtendimento()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                CarregaDentista();
                CarregaPaciente();
                return View();
            }

        }
        [HttpPost]
        public ActionResult CadAtendimento(ModelAtendimento mod)
        {
            CarregaDentista();
            CarregaPaciente();
            mod.codDen = Request["dentista"];
            mod.codPac = Request["paciente"];
            acAten.testarAtendimento(mod);

            if (mod.confAtendimento == "1")
            {
                acAten.inserirAtendimento(mod);
                ViewBag.msg = "Agendamento Realizado";

            }
            else if (mod.confAtendimento == "0")
            {
                ViewBag.msg = "Horário indisponível";

            }
            return View();
        }
        /* ----- END ATENDIMENTO ----- */

        /* --------------- CADASTRO DE USUARIOS E ADMS DO SISTEMA ---------------*/

        public ActionResult CadLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadLogin(ModelLogin modLog)
        {
            acLogin.InserirLogin(modLog);

            return RedirectToAction("CadLogin", "Adm");
        }

        public void CarregaLogin()
        {
            List<SelectListItem> logins = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdAula2010; User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbLogin", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    logins.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.logins = new SelectList(logins, "value", "Text");

        }
        public ActionResult ConsultaLogin(ModelLogin modLog)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acLogin.CarregaLogin(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }

    }
}