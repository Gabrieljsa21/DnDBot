using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    /// <summary>
    /// Representa uma tag associada a uma raça.
    /// Usada para categorizar ou descrever características especiais da raça.
    /// </summary>
    public class RacaTag
    {
        public string RacaId { get; set; }
        public string Tag { get; set; }

        public Raca Raca { get; set; }
    }
}
