using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Ficha.Auxiliares
{
    public class MagiaTag
    {
        public string MagiaId { get; set; }
        public string Tag { get; set; }

        public Magia Magia { get; set; }
    }

}
