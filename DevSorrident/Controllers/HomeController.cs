using DevSorrident.Dados;
using DevSorrident.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevSorrident.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        /* ÍNDICE DE COMENTÁRIOS PARA MARCAÇÃO DOS BLOCOS:
        */
        /* --------------- MARCAÇÃO DOS BLOCOS COM COMENTÁRIOS NESSE ESTILO ---------------*/
        /* ----- SUB ITEM ----- */
        /* --- !!!!! PONTOS DE ATENÇÃO !!!!! ---*/


        /* --------------- INSTANCIAS DAS AÇÕES (DADOS) ---------------*/

        AcoesLogin acLogin = new AcoesLogin();

        /* --------------- MÉTODO CONSULTANDO E CARREGANDO A DATABASE ---------------*/

        /* ----- CARREGA PAGINA INICIAL E VERIFICA LOGIN, TIPO DE USUARIO -----------*/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ModelLogin verlogin)
        {
            acLogin.TestarUsuario(verlogin);

            if (verlogin.usuario != null && verlogin.senha != null)
            {
                Session["usuarioLogado"] = verlogin.usuario.ToString();
                Session["senhaLogado"] = verlogin.usuario.ToString();

                if (verlogin.tipo == "1")
                {
                    Session["tipoLogado1"] = verlogin.tipo.ToString();
                }
                else
                {
                    Session["tipoLogado2"] = verlogin.tipo.ToString();
                }

                return RedirectToAction("Home", "Home");
            }
            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome de usuário e a senha";
                return View();
            }
        }

        public ActionResult Home()
        {
            if (Session["usuariologado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Descrição da página.";

                return View();
            }
        }



        /* ----- PAGINAS ABOUT E CONTACT ISOLADAS PARA TESTE ----- */

        /*public ActionResult About()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
        }


        public ActionResult Contact()
        {
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (Session["tipoLogado2"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Your contact page.";
                    return View();
                }
            }
        }*/

        public ActionResult semAcesso()
        {
            Response.Write("<script>alert('Acesso negado!')</script>");
            ViewBag.message = "Você não tem acesso a essa página";
            return View();
        }



        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado1"] = null;
            Session["tipoLogado2"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}