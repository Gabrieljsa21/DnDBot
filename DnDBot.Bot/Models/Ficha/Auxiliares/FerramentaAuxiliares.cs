using DnDBot.Bot.Models.ItensInventario;
using System;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class FerramentaTag
    {
        public string FerramentaId { get; set; }

        public Ferramenta Ferramenta { get; set; }
        public string Tag { get; set; }
    }
    public class FerramentaPericia
    {
        public string FerramentaId { get; set; }

        public Ferramenta Ferramenta { get; set; }
        public string PericiaId { get; set; }

        public Pericia Pericia { get; set; }
    }
}
