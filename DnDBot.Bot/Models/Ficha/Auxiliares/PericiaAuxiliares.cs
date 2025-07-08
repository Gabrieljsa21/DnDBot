using DnDBot.Bot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class PericiaTag
    {
        public string PericiaId { get; set; }
        public string Tag { get; set; }

        public Pericia Pericia { get; set; }
    }
}
