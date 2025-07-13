using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.Ficha
{
    public class Magia : EntidadeBase
    {
        public int Nivel { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EscolaMagia Escola { get; set; }

        public bool PodeSerRitual { get; set; }

        public bool ComponenteVerbal { get; set; }
        public bool ComponenteSomatico { get; set; }
        public bool ComponenteMaterial { get; set; }

        public string DetalhesMaterial { get; set; }
        public bool ComponenteMaterialConsumido { get; set; }
        public string CustoComponenteMaterial { get; set; }

        public List<MagiaClassePermitida> ClassesPermitidas { get; set; } = new();

        public List<EfeitoEscalonado> EfeitosEscalonados { get; set; } = new();
        public List<MagiaTag> MagiaTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => MagiaTags?.Select(mt => mt.Tag).ToList() ?? new();
            set => MagiaTags = value?.Select(tag => new MagiaTag { Tag = tag, MagiaId = Id }).ToList() ?? new();
        }

    }

}
