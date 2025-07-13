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
        [JsonIgnore] 
        public List<FerramentaPericia> PericiasAssociadas { get; set; } = new();

        [NotMapped]
        [JsonPropertyName("PericiasAssociadas")]
        public List<string> PericiasIds
        {
            get => PericiasAssociadas?.Select(p => p.PericiaId).ToList() ?? new();
            set => PericiasAssociadas = value?.Select(pid => new FerramentaPericia
            {
                FerramentaId = Id,
                PericiaId = pid
            }).ToList() ?? new();
        }

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

        /// <summary>
        /// Garante que os relacionamentos tenham o FerramentaId preenchido corretamente após a desserialização.
        /// </summary>
        public void NormalizarRelacionamentos()
        {
            if (PericiasAssociadas != null)
            {
                foreach (var pericia in PericiasAssociadas)
                {
                    pericia.FerramentaId = Id;
                }
            }

            if (FerramentaTags != null)
            {
                foreach (var tag in FerramentaTags)
                {
                    tag.FerramentaId = Id;
                }
            }
        }
    }
}
