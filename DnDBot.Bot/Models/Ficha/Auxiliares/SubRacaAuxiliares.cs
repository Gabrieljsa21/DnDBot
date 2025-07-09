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

    public class SubRacaIdioma
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string IdiomaId { get; set; }
        public Idioma Idioma { get; set; }
    }

    public class SubRacaCaracteristica
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }

    public class SubRacaResistencia
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string ResistenciaId { get; set; }
        public Resistencia Resistencia { get; set; }
    }

    public class SubRacaProficiencia
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string ProficienciaId{ get; set; }
        public Proficiencia Proficiencia { get; set; }
    }

    public class SubRacaMagia
    {
        public string SubRacaId { get; set; }
        public SubRaca SubRaca { get; set; }

        public string MagiaId{ get; set; }
        public Magia Magia { get; set; }
    }

}
