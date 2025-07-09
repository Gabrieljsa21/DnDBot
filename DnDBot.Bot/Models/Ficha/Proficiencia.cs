using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using System;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa uma proficiência que um personagem pode ter, como em armas, armaduras,
    /// perícias ou ferramentas.
    /// </summary>
    public class Proficiencia : EntidadeBase
    {
        public TipoProficiencia Tipo { get; set; }

        public string PericiaId { get; set; }
        public Pericia? Pericia { get; set; } // Só se Tipo == Pericia

        public bool TemEspecializacao { get; set; } = false;
        public int BonusAdicional { get; set; } = 0;
    }

}
