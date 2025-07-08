using DnDBot.Bot.Models.Ficha;
using System;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class AlinhamentoTag
    {
        public string AlinhamentoId { get; set; }
        public string Tag { get; set; }

        public Alinhamento Alinhamento { get; set; }
    }
}
