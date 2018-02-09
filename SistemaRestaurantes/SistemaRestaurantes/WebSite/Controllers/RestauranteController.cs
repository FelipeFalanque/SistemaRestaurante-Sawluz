using RepositorioDados.Entidades;
using RepositorioDados.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Utils;

namespace WebSite.Controllers
{
    public class RestauranteController : Controller
    {
        // GET: Restaurante
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
        public ActionResult GetRestaurantes()
        {
            // disponibiliza o repositorio para ser usado
            RepositorioRestaurante repositorio = new RepositorioRestaurante();
            // lista de restaurante que sera devolvida
            List<Restaurante> restaurantes = new List<Restaurante>();
            // busca no banco os restaurantes
            restaurantes = repositorio.Select().OrderBy(r => r.Nome).ToList();
            // ResponseView objeto usado para trafegar dados do back end para front end
            ResponseView response = new ResponseView() { Status = Status.OK, Result = restaurantes };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarRestaurante(Restaurante restaurante)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioRestaurante repositorio = new RepositorioRestaurante();
            // Devo atualizar
            if (restaurante.ID > 0) { repositorio.Update(restaurante); }
            // Devo Adicionar
            else { repositorio.Insert(restaurante); }
            // ResponseView objeto usado para trafegar dados do back end para front end
            ResponseView response = new ResponseView() { Status = Status.OK, Result = null };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveRestaurante(int idRestaurante)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioRestaurante repositorio = new RepositorioRestaurante();
            // bucas restaurante da base de dados com seus pratos
            Restaurante restaurante = repositorio.SelectEager().FirstOrDefault(r => r.ID == idRestaurante);
            // Apaga o restaurante e seus ratos
            repositorio.Delete(restaurante);
            // ResponseView objeto usado para trafegar dados do back end para front end
            // devolve o id para o front para remover a linha da tabela com esse id
            ResponseView response = new ResponseView() { Status = Status.OK, Result = idRestaurante };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRestaurante(int idRestaurante)
        {
            // disponibiliza o repositorio para ser usado
            RepositorioRestaurante repositorio = new RepositorioRestaurante();
            // bucas restaurante da base de dados lazy
            Restaurante restaurante = repositorio.Select().FirstOrDefault(r => r.ID == idRestaurante);
            // ResponseView objeto usado para trafegar dados do back end para front end
            // devolve o restaurante para o front
            ResponseView response = new ResponseView() { Status = Status.OK, Result = restaurante };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}