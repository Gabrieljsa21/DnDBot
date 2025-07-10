using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.Ficha
{
    public class Magia : EntidadeBase
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NivelMagia Nivel { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EscolaMagia Escola { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoUsoMagia TempoConjuracao { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoAlcance Alcance { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Alvo Alvo { get; set; }

        public bool Concentracao { get; set; }

        public int? DuracaoQuantidade { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DuracaoUnidade DuracaoUnidade { get; set; }

        public bool PodeSerRitual { get; set; }

        public bool ComponenteVerbal { get; set; }
        public bool ComponenteSomatico { get; set; }
        public bool ComponenteMaterial { get; set; }

        public string DetalhesMaterial { get; set; }
        public bool ComponenteMaterialConsumido { get; set; }
        public string CustoComponenteMaterial { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoDano TipoDano { get; set; }

        public string DadoDano { get; set; }
        public string Escalonamento { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Atributo AtributoTesteResistencia { get; set; }

        public bool MetadeNoTeste { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FormaAreaEfeito? FormaAreaEfeito { get; set; }

        public List<MagiaCondicaoAplicada> CondicoesAplicadas { get; set; } = new();
        public List<MagiaCondicaoRemovida> CondicoesRemovidas { get; set; } = new();
        public List<MagiaClassePermitida> ClassesPermitidas { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RecargaMagia Recarga { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoUsoMagia TipoUso { get; set; }

        public bool RequerLinhaDeVisao { get; set; }
        public bool RequerLinhaReta { get; set; }

        public int? NumeroMaximoAlvos { get; set; }
        public string AreaEfeito { get; set; }
        public string FocoNecessario { get; set; }
        public string LimiteUso { get; set; }
        public string EfeitoPorTurno { get; set; }

        public int NumeroDeUsos { get; set; }

        public List<MagiaTag> MagiaTags { get; set; } = new();

        [NotMapped]
        public List<string> Tags
        {
            get => MagiaTags?.Select(mt => mt.Tag).ToList() ?? new();
            set => MagiaTags = value?.Select(tag => new MagiaTag { Tag = tag, MagiaId = Id }).ToList() ?? new();
        }
    }
}
