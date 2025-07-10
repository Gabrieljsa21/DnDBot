using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class MagiaTag
    {
        public string MagiaId { get; set; }
        public string Tag { get; set; }

        public Magia Magia { get; set; }
    }

    public class MagiaCondicaoAplicada
    {
        public string MagiaId { get; set; }
        public Magia Magia { get; set; }

        public Condicao Condicao { get; set; } // Enum diretamente
    }

    public class MagiaCondicaoRemovida
    {
        public string MagiaId { get; set; }
        public Magia Magia { get; set; }

        public Condicao Condicao { get; set; }
    }
    public class MagiaClassePermitida
    {
        public string MagiaId { get; set; }
        public Magia Magia { get; set; }

        public ClassePersonagem Classe { get; set; }
    }
}
