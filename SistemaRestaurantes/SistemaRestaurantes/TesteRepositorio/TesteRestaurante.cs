using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioDados.Repositorio;
using RepositorioDados.Entidades;
using System.Linq;
using System.Collections.Generic;

namespace TesteRepositorio
{
    [TestClass]
    public class TesteRestaurante
    {
        private RepositorioPrato repositorioPrato;
        private RepositorioRestaurante repositorioRestaurante;

        private Restaurante restaurante_x;
        private Restaurante restaurante_y;

        private string nomeRestauranteX = "NomeUnicoRestauranteX";
        private string nomeRestauranteY = "NomeUnicoRestauranteY";

        [TestInitialize]
        public void LoadStartTeste()
        {
            /// Iniciando os repositorios
            repositorioPrato = new RepositorioPrato();
            repositorioRestaurante = new RepositorioRestaurante();
            restaurante_x = new Restaurante() { Nome = nomeRestauranteX };
            restaurante_y = new Restaurante() { Nome = nomeRestauranteY };
        }

        [TestMethod]
        public void DeveAdicionarUmRestauranteNaBase()
        {
            // Add o restaurante x na base de dados
            repositorioRestaurante.Insert(restaurante_x);
            // verifica se o restaurante esta na base de dados
            Assert.IsNotNull(repositorioRestaurante.Select().FirstOrDefault(r => r.Nome == nomeRestauranteX));
        }

        [TestMethod]
        public void DeveAdicionarUmRestauranteEDoisPratosParaOMesmoNaBase()
        {
            // Add o Restaurante Y e dois pratos para o mesmo na base de dados
            AddRestauranteYeDoisPratosNaBaseDeDados();
            // deixa null o obj para limpalo
            restaurante_y.Pratos = null;
            restaurante_y = null;
            // Busca Eager
            restaurante_y = repositorioRestaurante.SelectEager().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault();
            // Verifica se devolveu obj da base de dados
            Assert.IsNotNull(restaurante_y);
            // Verifica se tem dois pratos nesse Restaurante
            Assert.AreEqual(restaurante_y.Pratos.Count, 2);
            
        }

        [TestMethod]
        public void DeveTrazerUmRestauranteLazy()
        {
            // Add o Restaurante Y e dois pratos para o mesmo na base de dados
            AddRestauranteYeDoisPratosNaBaseDeDados();
            // Verifica se tem dois pratos na base do restaurante Y
            Assert.AreEqual(repositorioPrato.SelectEager().Where(p => p.Restaurante.Nome == nomeRestauranteY).ToList().Count, 2);
            // deixa null o obj para limpalo
            restaurante_y.Pratos = null;
            restaurante_y = null;
            // Busca Lazy
            restaurante_y = repositorioRestaurante.Select().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault();
            // Verifica que realmente não veio os pratos no obj restaurante sendo assim fazendo dele um obj Lazy
            Assert.IsNull(restaurante_y.Pratos, null);
        }

        [TestMethod]
        public void DeveTrazerUmRestauranteEager()
        {
            // Add o Restaurante Y e dois pratos para o mesmo na base de dados
            AddRestauranteYeDoisPratosNaBaseDeDados();
            // deixa null o obj para limpalo
            restaurante_y.Pratos = null;
            restaurante_y = null;
            // Busca Lazy
            restaurante_y = repositorioRestaurante.Select().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault();
            // Verifica se veio sem os pratos
            Assert.IsNull(restaurante_y.Pratos);
            // Busca Eager
            restaurante_y = repositorioRestaurante.GetRestauranteEager(restaurante_y.ID);
            // Verifica se veio com os pratos a quantidade correta
            Assert.IsNotNull(restaurante_y.Pratos);
            Assert.AreEqual(restaurante_y.Pratos.Count, 2);
        }

        [TestMethod]
        public void DeveRemoverORestauranteESeusPratos()
        {
            var idRestaurante = 0;
            // Add o Restaurante Y e dois pratos para o mesmo na base de dados
            AddRestauranteYeDoisPratosNaBaseDeDados();
            // deixa null o obj para limpalo
            restaurante_y.Pratos = null;
            restaurante_y = null;
            // Busca Lazy
            restaurante_y = repositorioRestaurante.Select().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault();
            // Guarda id para consultas posteriores
            idRestaurante = restaurante_y.ID;
            // Verifica se veio sem os pratos
            Assert.IsNull(restaurante_y.Pratos);
            // Verifica se existe na base dois pratos referente ao restaurante
            Assert.AreEqual(repositorioPrato.Select().Where(p => p.RestauranteID == idRestaurante).ToList().Count, 2);
            // Busca Eager
            restaurante_y = repositorioRestaurante.GetRestauranteEager(restaurante_y.ID);
            // Verifica se veio com os pratos a quantidade correta
            Assert.AreEqual(restaurante_y.Pratos.Count, 2);
            // Deleta o Restaurante e consequentemente seus pratos devido ao contexto
            repositorioRestaurante.Delete(restaurante_y);
            // Verifica se realmente não existe mais na base de dados os pratos desse restaurante
            Assert.AreEqual(repositorioPrato.Select().Where(p => p.RestauranteID == idRestaurante).ToList().Count, 0);
            // Verifica se o restaurante não existe mais na base de dados
            Assert.IsNull(repositorioRestaurante.Select().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault());
            // Verifica se o restaurante não existe mais na base de dados agora por id
            Assert.IsNull(repositorioRestaurante.Select().Where(r => r.ID == idRestaurante).FirstOrDefault());

        }

        private void AddRestauranteYeDoisPratosNaBaseDeDados()
        {
            // Add na base de dados o restaurente y
            repositorioRestaurante.Insert(restaurante_y);
            Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, Restaurante = restaurante_y };
            Prato arroz = new Prato() { Nome = "Arroz", Valor = 25.9, Restaurante = restaurante_y };
            // Add na base de dados os dois pratos
            repositorioPrato.Insert(fritas);
            repositorioPrato.Insert(arroz);
        }

        [TestCleanup]
        public void EndTeste()
        {

            restaurante_x = null;

            restaurante_x = repositorioRestaurante.SelectEager().Where(r => r.Nome == nomeRestauranteX).FirstOrDefault();

            if (restaurante_x != null)
            {
                // ja remove os pratos juntos
                repositorioRestaurante.Delete(restaurante_x);
            }


            restaurante_y = null;

            restaurante_y = repositorioRestaurante.SelectEager().Where(r => r.Nome == nomeRestauranteY).FirstOrDefault();

            if (restaurante_y!= null)
            {
                // ja remove os pratos juntos
                repositorioRestaurante.Delete(restaurante_y);
            }
        }

    }
}
