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

    public class FichaPersonagemAlinhamento
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string AlinhamentoId { get; set; }
        public Alinhamento Alinhamento { get; set; }
    }

    public class FichaPersonagemIdioma
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string IdiomaId { get; set; }
        public Idioma Idioma { get; set; }
    }

    public class FichaPersonagemCaracteristica
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }
    }

    public class FichaPersonagemResistencia
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string ResistenciaId { get; set; }
        public Resistencia Resistencia { get; set; }
        public TipoDano TipoDano { get; set; } = new TipoDano();
        }

    public class FichaPersonagemProficiencia
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string ProficienciaId { get; set; }
        public Proficiencia Proficiencia { get; set; }

        public bool TemEspecializacao { get; set; } = false;
        public int BonusAdicional { get; set; } = 0;
    }

    public class FichaPersonagemMagia
    {
        public Guid FichaPersonagemId { get; set; }
        public FichaPersonagem FichaPersonagem { get; set; }

        public string MagiaId { get; set; }
        public Magia Magia { get; set; }
    }
}
