using RepositorioDados.Entidades;
using RepositorioDados.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using WebSite.Utils;

namespace WebSite.Controllers
{
    public class PratoController : Controller
    {
        // GET: Prato
        public ActionResult Index()
        {
            return View();
        }

        // GET: Restaurante
        public ActionResult Formulario()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPratos()
        {
            // disponibiliza o repositorio para ser usado
            RepositorioPrato repositorio = new RepositorioPrato();
            // busca todos os pratos com seus respctivos restaurantes
            List<Prato> pratosBD = repositorio.SelectEager().ToList();
            // lista de pratos que serão enviadas via json /* uma solulção pra reference circular
            List<PratoView> pratos_view = new List<PratoView>();
            // percorre os pratos trasidos so banco e os tranformam em Prato view
            foreach (var prato in pratosBD)
            {
                pratos_view.Add(new PratoView() {
                    ID = prato.ID,
                    Nome = prato.Nome,
                    NomeRestaurante = prato.Restaurante.Nome,
                    RestauranteID = prato.RestauranteID,
                    Valor = prato.Valor.ToString()
                });
            }
            // ResponseView objeto usado para trafegar dados do back end para front end
            ResponseView response = new ResponseView() { Status = Status.OK, Result = pratos_view };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarPrato(PratoView prato)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioPrato repositorio = new RepositorioPrato();
            // Devo atualizar
            if (prato.ID > 0) {
                // busca prato do banco para ser atualizado
                Prato pratoBD = repositorio.Select().Where(p => p.ID == prato.ID).FirstOrDefault();
                // Edita os atributos
                pratoBD.Nome = prato.Nome;
                pratoBD.Valor = Convert.ToDouble(prato.Valor);
                pratoBD.RestauranteID = prato.RestauranteID;
                // Salva na base o praro editado
                repositorio.Update(pratoBD);
            }
            // Devo Adicionar
            else
                repositorio.Insert(new Prato() { Nome = prato.Nome, Valor = Convert.ToDouble(prato.Valor), RestauranteID = prato.RestauranteID });

            // ResponseView objeto usado para trafegar dados do back end para front end
            ResponseView response = new ResponseView() { Status = Status.OK, Result = null };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemovePrato(int idPrato)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioPrato repositorio = new RepositorioPrato();
            // busca o prato no banco de dados
            Prato prato = repositorio.Select().FirstOrDefault(p => p.ID == idPrato);
            // deleta o prato do banco
            repositorio.Delete(prato);
            // ResponseView objeto usado para trafegar dados do back end para front end
            // Devolvo o Id para o front excluir a linha do prato com esse Id
            ResponseView response = new ResponseView() { Status = Status.OK, Result = idPrato };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPrato(int idPrato)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioPrato repositorio = new RepositorioPrato();
            // busca o prato no banco de dados "Prato com seu Restaurante"
            Prato pratoDB = repositorio.GetPratoEager(idPrato);
            // tranforma em pratoBD em PratoView
            PratoView prato = new PratoView()
            {
                ID = pratoDB.ID,
                Nome = pratoDB.Nome,
                NomeRestaurante = pratoDB.Restaurante.Nome,
                RestauranteID = pratoDB.RestauranteID,
                Valor = pratoDB.Valor.ToString()
            };
            // ResponseView objeto usado para trafegar dados do back end para front end
            // envia para o front o objeto Prato View
            ResponseView response = new ResponseView() { Status = Status.OK, Result = prato };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}