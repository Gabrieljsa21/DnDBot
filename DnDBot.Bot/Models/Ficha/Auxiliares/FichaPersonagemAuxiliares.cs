using DnDBot.Bot.Models.Enums;
using System;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class FichaPersonagemTag
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }
        public string Tag { get; set; }

    }
    public class FichaPersonagemResistencia
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public TipoDano TipoDano { get; set; }

    }
}
