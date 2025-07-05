using System;

namespace DnDBot.Application.Models.AntecedenteModels
{
    /// <summary>
    /// Representa um defeito (flaw) associado a um antecedente de personagem.
    /// Defeitos são características negativas que ajudam a definir a personalidade ou história do personagem.
    /// </summary>
    public class Defeito : EntidadeBase
    {
        /// <summary>
        /// Identificador do antecedente ao qual este defeito está associado.
        /// </summary>
        public string AntecedenteId { get; set; }
    }
}
