using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa um alinhamento moral e ético para um personagem.
    /// Exemplos incluem "Leal e Bom", "Caótico e Mal", etc.
    /// </summary>
    public class Alinhamento : EntidadeBase
    {

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Raca_Tag.
        /// </summary>
        public List<AlinhamentoTag> AlinhamentoTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de RacaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => AlinhamentoTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => AlinhamentoTags = value?.Select(tag => new AlinhamentoTag { Tag = tag, AlinhamentoId = this.Id }).ToList() ?? new();
        }
    }
}
