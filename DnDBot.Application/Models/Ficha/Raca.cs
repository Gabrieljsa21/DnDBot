using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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


        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Raca_Tag.
        /// </summary>
        public List<RacaTag> RacaTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de RacaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => RacaTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => RacaTags = value?.Select(tag => new RacaTag { Tag = tag, RacaId = this.Id }).ToList() ?? new();
        }
    }
}
