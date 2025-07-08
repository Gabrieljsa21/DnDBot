using DnDBot.Bot.Models.AntecedenteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class AntecedenteTag
    {
        public string AntecedenteId { get; set; }
        public string Tag { get; set; }

        public Antecedente Antecedente { get; set; }
    }

}
