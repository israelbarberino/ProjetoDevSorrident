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
    public class ConsultaController : Controller
    {
        // GET: Consulta


        /* ÍNDICE DE COMENTÁRIOS PARA MARCAÇÃO DOS BLOCOS:
        */
        /* --------------- MARCAÇÃO DOS BLOCOS COM COMENTÁRIOS NESSE ESTILO ---------------*/
        /* ----- SUB ITEM ----- */
        /* --- !!!!! PONTOS DE ATENÇÃO !!!!! ---*/


        /* --------------- INSTANCIAS DAS AÇÕES (DADOS) ---------------*/

        AcoesLogin acLogin = new AcoesLogin();
        AcoesPaciente acPac = new AcoesPaciente();
        AcoesDentista acDen = new AcoesDentista();
        AcoesEspecialidade acEsp = new AcoesEspecialidade();
        AcoesAtendimento acAten = new AcoesAtendimento();

        /* --------------- MÉTODO CONSULTANDO E CARREGANDO A DATABASE ---------------*/

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

        /* --------------- CARREGA A PÁGINA INICIAL ---------------*/
        public ActionResult Index()
        {
            return View();
        }

        /* --------------- MÉTODOS DE LISTAGEM, INSERÇÃO, EDIÇÃO E EXCLUSÃO DOS DADOS (CRUD METHODS) ---------------*/
        
        /* ----- PACIENTE ----- */

        public ActionResult ListarPaciente()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(acPac.GetPaciente());
            }
        }

        public ActionResult ExcluirPaciente(int id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                acPac.DeletePaciente(id);
                return RedirectToAction("ListarPaciente");
            }

        }
        public ActionResult EditarPaciente(string id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                return View(acPac.GetPaciente().Find(model => model.codPac == id));
            }

        }
        [HttpPost]
        public ActionResult EditarPaciente(int id, ModelPaciente cm)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                cm.codPac = id.ToString();
                acPac.AtualizaPaciente(cm);
                return RedirectToAction ("ListarPaciente", "Consulta");
            }
        }


        /* ----- DENTISTA ----- */

        public ActionResult ListarDentista()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(acDen.GetDentista());
            }
        }

        public ActionResult ExcluirDentista(int id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                acDen.DeleteDentista(id);
                return RedirectToAction("ListarDentista");
            }

        }
        public ActionResult EditarDentista(string id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                CarregaEspecialidade();
                return View(acDen.GetDentista().Find(model => model.codDen == id));
            }

        }
        [HttpPost]
        public ActionResult EditarDentista(int id, ModelDentista cm)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    CarregaEspecialidade();
                    cm.codEsp = Request["especialidade"];
                    cm.codDen = id.ToString();
                    acDen.AtualizaDentista(cm);
                    ViewBag.msg = "Atualização efetuada com sucesso";

                    return View();
                }
            }
        }

        /* ----- ESPECIALIDADE ----- */

        public ActionResult ListarEspecialidade()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(acEsp.GetEspecialidade());
            }
        }

        public ActionResult ExcluirEspecialidade(int id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                acEsp.DeleteEspecialidade(id);
                return RedirectToAction("ListarEspecialidade");
            }
        }

        public ActionResult EditarEspecialidade(string id)
        {
            if (Session["tipoLogado2"] == null)
            {
                return RedirectToAction("semAcesso", "Home");
            }
            else
            {
                return View(acEsp.GetEspecialidade().Find(model => model.codEsp == id));
            }
        }

        [HttpPost]
        public ActionResult EditarEspecialidade(int id, ModelEspecialidade cm)
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    cm.codEsp = id.ToString();
                    acEsp.AtualizaEspecialidade(cm);
                    ViewBag.msg = "Atualização efetuada com sucesso";

                    return View();
                }
            }
        }


        /* ----- ATENDIMENTO ----- */

        public ActionResult ListarAtendimentos(ModelAtendimento modAtend)
        {
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = acAten.CarregaAtendimento(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            return View();
        }

    }
}