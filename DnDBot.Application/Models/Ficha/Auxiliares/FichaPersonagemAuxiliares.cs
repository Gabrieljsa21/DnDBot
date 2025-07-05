using System;

namespace DnDBot.Application.Models.Ficha.Auxiliares
{
    public class FichaPersonagemTag
    {
        public Guid FichaPersonagemId { get; set; }
        public string Tag { get; set; }

        public FichaPersonagem FichaPersonagem { get; set; }
    }
}
