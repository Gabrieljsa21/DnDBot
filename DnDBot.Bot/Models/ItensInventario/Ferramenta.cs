using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa uma ferramenta utilizada por personagens em diversas tarefas e perícias.
    /// </summary>
    public class Ferramenta : Item
    {
        /// <summary>
        /// Lista de perícias que a ferramenta pode auxiliar ou está associada.
        /// </summary>
        [JsonIgnore] // Ignorar no JSON, pois você vai usar outra propriedade para desserializar as IDs
        public List<FerramentaPericia> PericiasAssociadas { get; set; } = new();

        [JsonPropertyName("PericiasAssociadas")]
        public List<string> PericiasIds { get; set; } = new();

        /// <summary>
        /// Indica se a ferramenta requer proficiência para uso efetivo.
        /// </summary>
        public bool RequerProficiencia { get; set; }

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Ferramenta_Tag.
        /// </summary>
        public List<FerramentaTag> FerramentaTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de FerramentaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => FerramentaTags?.Select(ft => ft.Tag).ToList() ?? new();
            set => FerramentaTags = value?.Select(tag => new FerramentaTag { Tag = tag, FerramentaId = Id }).ToList() ?? new();
        }

    }
}
