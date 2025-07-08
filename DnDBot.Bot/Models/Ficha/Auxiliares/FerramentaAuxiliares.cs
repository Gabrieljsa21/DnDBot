using DnDBot.Bot.Models.ItensInventario;
using System;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class FerramentaTag
    {
        public string FerramentaId { get; set; }
        public string Tag { get; set; }

        public Ferramenta Ferramenta { get; set; }
    }
}
