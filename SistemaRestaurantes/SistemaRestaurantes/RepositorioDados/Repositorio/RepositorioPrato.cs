using RepositorioDados.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace RepositorioDados.Repositorio
{
    public class RepositorioPrato : IGenericRepository<Prato>, IDisposable
    {
        private EntidadeContexto contexto;

        public RepositorioPrato()
        {
            this.contexto = new EntidadeContexto();
        }

        public void Insert(Prato p)
        {
            contexto.Pratos.Add(p);
            contexto.SaveChanges();
        }

        public void Update(Prato p)
        {
            contexto.Pratos.Update(p);
            contexto.SaveChanges();
        }

        public void Delete(Prato p)
        {
            contexto.Pratos.Remove(p);
            contexto.SaveChanges();
        }

        public IList<Prato> Select()
        {
            return contexto.Pratos.ToList();
        }

        public Prato GetPratoEager(int idPrato)
        {
            Prato prato =
                contexto.Pratos
                .Include(p => p.Restaurante)
                .FirstOrDefault(p => p.ID == idPrato);

            return prato;
        }

        public IList<Prato> SelectEager()
        {
            return contexto.Pratos.Include(p => p.Restaurante).ToList();
        }

        public void Dispose()
        {
            contexto.Dispose();
        }
    }
}
