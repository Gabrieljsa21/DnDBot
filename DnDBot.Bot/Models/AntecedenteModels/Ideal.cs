using DnDBot.Bot.Models;
using System;

namespace DnDBot.Bot.Models.AntecedenteModels
{
    /// <summary>
    /// Representa um ideal associado a um antecedente de personagem.
    /// Ideais são princípios ou valores que guiam o comportamento do personagem.
    /// </summary>
    public class Ideal : EntidadeBase
    {

        /// <summary>
        /// Identificador do antecedente ao qual este ideal está associado.
        /// </summary>
        public string AntecedenteId { get; set; }
    }
}
