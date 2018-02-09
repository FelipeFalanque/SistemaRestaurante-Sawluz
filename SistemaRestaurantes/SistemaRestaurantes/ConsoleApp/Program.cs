using RepositorioDados.Entidades;
using RepositorioDados.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RepositorioRestaurante repositorioRestaurante = new RepositorioRestaurante();

            foreach (var restaurante in repositorioRestaurante.Select())
            {
                Console.WriteLine("o restaurante " + restaurante.Nome);
            }

            RepositorioPrato repositorioPrato = new RepositorioPrato();

            foreach (var prato in repositorioPrato.Select())
            {
                Console.WriteLine("o prato " + prato.Nome);
            }
            
            Console.ReadLine();
        }
    }
}
