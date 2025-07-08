using DnDBot.Bot.Models.Enums;
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
    public class SubRacaTag
    {
        public string SubRacaId { get; set; }
        public string Tag { get; set; }

        public SubRaca SubRaca { get; set; }
    }

    public class SubRacaAlinhamento
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string AlinhamentoId { get; set; }
        public Alinhamento Alinhamento { get; set; }
    }

    public class SubRacaResistencia
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public TipoDano TipoDano { get; set; }
    }

}
