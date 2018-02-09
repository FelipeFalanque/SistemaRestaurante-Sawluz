using Microsoft.Data.Entity;
using RepositorioDados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioDados.Repositorio
{
    public class RepositorioRestaurante : IGenericRepository<Restaurante>, IDisposable
    {
        private EntidadeContexto contexto;

        public RepositorioRestaurante()
        {
            this.contexto = new EntidadeContexto();
        }

        public void Insert(Restaurante p)
        {
            contexto.Restaurantes.Add(p);
            contexto.SaveChanges();
        }

        public void Update(Restaurante p)
        {
            contexto.Restaurantes.Update(p);
            contexto.SaveChanges();
        }

        public void Delete(Restaurante p)
        {
            contexto.Restaurantes.Remove(p);
            contexto.SaveChanges();
        }

        public IList<Restaurante> Select()
        {
            return contexto.Restaurantes.ToList();
        }

        public Restaurante GetRestauranteEager(int idRestaurante)
        {
            Restaurante restaurante = 
                contexto.Restaurantes
                .Include(r => r.Pratos)
                .FirstOrDefault(r => r.ID == idRestaurante);

            return restaurante;
        }

        public IList<Restaurante> SelectEager()
        {
            return contexto.Restaurantes.Include(r => r.Pratos).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
