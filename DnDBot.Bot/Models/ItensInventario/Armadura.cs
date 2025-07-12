using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa uma armadura no sistema D&D 5e.
    /// </summary>
    public class Armadura : Item
    {
        public int ClasseArmadura { get; set; } // CA base
        public bool ImpedeFurtividade { get; set; } // true = Desvantagem em Furtividade
        public int BonusDestrezaMaximo { get; set; } = -1; // -1 = sem limite

        public int RequisitoForca { get; set; } = 0;
        public List<string> PropriedadesEspeciais { get; set; } = new();

        public int DurabilidadeAtual { get; set; }
        public int DurabilidadeMaxima { get; set; }

        public List<TipoDano> ResistenciasDano { get; set; } = new();
        public List<TipoDano> ImunidadesDano { get; set; } = new();

        public List<ArmaduraTag> ArmaduraTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => ArmaduraTags?.Select(at => at.Tag).ToList() ?? new();
            set => ArmaduraTags = value?.Select(tag => new ArmaduraTag { Tag = tag, ArmaduraId = Id }).ToList() ?? new();
        }
    }
}
