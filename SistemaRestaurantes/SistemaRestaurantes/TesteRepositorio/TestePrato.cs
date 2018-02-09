using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepositorioDados.Repositorio;
using RepositorioDados.Entidades;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TesteRepositorio
{
    [TestClass]
    public class TestePrato
    {
        private RepositorioPrato repositorioPrato;
        private RepositorioRestaurante repositorioRestaurante;

        [TestInitialize]
        public void LoadStartTeste()
        {
            /// Iniciando os repositorios
            repositorioPrato = new RepositorioPrato();
            repositorioRestaurante = new RepositorioRestaurante();
            
        }

        [TestMethod]
        public void DeveAdicionarUmPratoAUmRestauranteNaBaseDados()
        {
            // Pega o primeiro restaurante da lista
            Restaurante restaurante = repositorioRestaurante.Select().FirstOrDefault();
            // cria um prato
            Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, Restaurante = restaurante };
            // Salva o prato no banco de dados
            repositorioPrato.Insert(fritas);
            // Guarda o id pra consultar
            int idPrato = fritas.ID;
            // Verifica de o prato esta na base de dados
            Assert.IsNotNull(repositorioPrato.Select().Where(p => p.ID == idPrato).FirstOrDefault());
            // Apaga o Prato usado para o teste
            ApagaOpratoUsadoParaTeste(fritas);
            // Verifica se foi apagado da base de dados
            Assert.IsNull(repositorioPrato.Select().Where(p => p.ID == idPrato).FirstOrDefault());
        }

/*
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NaoDeveAdicionarUmPratoAUmRestauranteQueNaoExisteDois()
        {
            // cria um prato com o Id de um restaurante que não existe na base de dados
            Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, RestauranteID = -200 };
            repositorioPrato.Insert(fritas);
            Assert.Fail();
        }
*/

        [TestMethod]
        public void NaoDeveAdicionarUmPratoAUmRestauranteQueNaoExiste()
        {
            // Try para o erro esperado
            try
            {
                // cria um prato com o Id de um restaurante que não existe na base de dados
                Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, RestauranteID = -200 };
                repositorioPrato.Insert(fritas);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                // Caso de exeption significa que deu certo pois realmente não pode add um prato a um restaurante que não existe.
                Assert.IsNotNull(ex);
            }

        }

        [TestMethod]
        public void DeveRemoverUmPrato()
        {
            // Pega o primeiro restaurante da lista
            Restaurante restaurante = repositorioRestaurante.Select().FirstOrDefault();
            // cria um prato
            Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, Restaurante = restaurante };
            // Salva o prato no banco de dados
            repositorioPrato.Insert(fritas);
            // Guarda o id pra consultar
            int idPrato = fritas.ID;
            // Deleta o prato criado agora
            repositorioPrato.Delete(fritas);
            // Verifica se prato realmente não existe mais na base de dados
            Assert.IsNull(repositorioPrato.Select().Where(p => p.ID == idPrato).FirstOrDefault());

        }

        [TestMethod]
        public void DeveTrazerTodosOsPratosComSeuRespectivoRestaurante()
        {
            // Realiza uma consulta Eager
            List<Prato> pratos = repositorioPrato.SelectEager().ToList();
            // percorre todos os pratos
            foreach (var prato in pratos)
            {
                // se o prato em questão tem valor em seu Restaurente
                // o objeto prato veio carregado de seu objeto Restaurante
                Assert.IsNotNull(prato.Restaurante);
            }
        }

        [TestMethod]
        public void DeveTrazerOPratoComSeuRespectivoRestaurante()
        {
            // Pega o primeiro restaurante da lista
            Restaurante restaurante = repositorioRestaurante.Select().FirstOrDefault();
            // cria um prato
            Prato fritas = new Prato() { Nome = "Fritas", Valor = 20.5, Restaurante = restaurante };
            // Salva o prato no banco de dados
            repositorioPrato.Insert(fritas);
            // Guarda o id pra consultar
            int idPrato = fritas.ID;
            // Limpa o obj Prato
            fritas = null;
            // Busca o prato na base de dados
            fritas = repositorioPrato.GetPratoEager(idPrato);
            // Verifica se o prato contem seu objeto restaurente
            Assert.IsNotNull(fritas.Restaurante);
        }

        private void ApagaOpratoUsadoParaTeste(Prato p)
        {
            repositorioPrato.Delete(p);
        }

    }
}
