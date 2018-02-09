using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class PratoView
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Valor { get; set; }

        public string NomeRestaurante  { get; set; }

        public int RestauranteID { get; set; }
    }
}