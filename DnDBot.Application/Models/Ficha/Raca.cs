using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma raça jogável no universo de Dungeons & Dragons.
    /// Contém informações como nome, descrição, sub-raças, fonte de origem e recursos visuais.
    /// </summary>
    public class Raca : EntidadeBase
    {

        /// <summary>
        /// Lista das sub-raças pertencentes a essa raça (ex: Elfo da Floresta, Anão da Colina).
        /// </summary>
        public List<SubRaca> SubRaca { get; set; } = new();

    }
}
