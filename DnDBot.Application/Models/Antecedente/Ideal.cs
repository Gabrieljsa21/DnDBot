using System;

namespace DnDBot.Application.Models.Antecedente.Antecedente
{
    /// <summary>
    /// Representa um ideal associado a um antecedente de personagem.
    /// Ideais são princípios ou valores que guiam o comportamento do personagem.
    /// </summary>
    public class Ideal
    {
        /// <summary>
        /// Identificador único do ideal.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do ideal.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada do ideal.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Identificador do antecedente ao qual este ideal está associado.
        /// </summary>
        public string IdAntecedente { get; set; }
    }
}
