using System;

namespace DnDBot.Application.Models.Antecedente.Antecedente
{
    /// <summary>
    /// Representa um defeito (flaw) associado a um antecedente de personagem.
    /// Defeitos são características negativas que ajudam a definir a personalidade ou história do personagem.
    /// </summary>
    public class Defeito
    {
        /// <summary>
        /// Identificador único do defeito.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do defeito.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada do defeito.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Identificador do antecedente ao qual este defeito está associado.
        /// </summary>
        public string IdAntecedente { get; set; }
    }
}
