using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioDados.Entidades
{
    public class Prato
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public double Valor { get; set; }

        public virtual Restaurante Restaurante { get; set; }

        public int RestauranteID { get; set; }
    }
}
