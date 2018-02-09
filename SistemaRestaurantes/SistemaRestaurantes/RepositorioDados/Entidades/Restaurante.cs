using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioDados.Entidades
{
    public class Restaurante
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public virtual IList<Prato> Pratos { get; set; }
    }
}
