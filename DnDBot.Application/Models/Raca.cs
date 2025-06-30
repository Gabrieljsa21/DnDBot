using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma raça jogável do D&D.
    /// </summary>
    public class Raca
    {
        /// <summary>
        /// Nome da raça (ex: Elfo, Anão).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição breve da raça.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de sub-raças pertencentes a essa raça.
        /// </summary>
        public List<string> SubRacas { get; set; } = new();
    }

    /// <summary>
    /// Representa uma sub-raça específica com descrição (uso futuro).
    /// </summary>
    public class SubRaca
    {
        /// <summary>
        /// Nome da sub-raça (ex: Alto Elfo, Anão da Montanha).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da sub-raça.
        /// </summary>
        public string Descricao { get; set; }
    }
}
