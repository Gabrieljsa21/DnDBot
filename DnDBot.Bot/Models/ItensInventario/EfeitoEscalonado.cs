using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class EfeitoEscalonado
    {
        public string Id { get; set; } = string.Empty;
        public string? MagiaId { get; set; }

        [JsonIgnore]
        public Magia? Magia { get; set; }

        public string? CaracteristicaId { get; set; }

        [JsonIgnore]
        public Caracteristica? Caracteristica { get; set; }

        public int NivelMinimo { get; set; }
        public int? NivelMaximo { get; set; }

        public int? UsosPorDescansoCurto { get; set; }
        public int? UsosPorDescansoLongo { get; set; }
        public int? DuracaoEmRodadas { get; set; }

        public AcaoRequerida AcaoRequerida { get; set; }
        public CondicaoAtivacao CondicaoAtivacao { get; set; }
        public string DescricaoEfeito { get; set; } = string.Empty;

        public List<EfeitoDano> Danos { get; set; } = new();

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FormaAreaEfeito? FormaAreaEfeito { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoAlcance Alcance { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Alvo Alvo { get; set; }

        public bool Concentracao { get; set; }

        public int? DuracaoQuantidade { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DuracaoUnidade? DuracaoUnidade { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RecargaMagia Recarga { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoUsoMagia TipoUso { get; set; }

        public bool RequerLinhaDeVisao { get; set; }
        public bool RequerLinhaReta { get; set; }

        public int? NumeroMaximoAlvos { get; set; }
        public string FocoNecessario { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Atributo AtributoTesteResistencia { get; set; }

        public bool MetadeNoTeste { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoUsoMagia TempoConjuracao { get; set; }

        public List<MagiaCondicaoAplicada> CondicoesAplicadas { get; set; } = new();
        public List<MagiaCondicaoRemovida> CondicoesRemovidas { get; set; } = new();

        public override string ToString() => $"{DescricaoEfeito} (nível {NivelMinimo}-{NivelMaximo?.ToString() ?? "∞"})";
    }


    public class EfeitoDano
    {
        public string Id { get; set; }

        public string DadoDano { get; set; } = string.Empty;
        public TipoDano TipoDano { get; set; }

        public string EfeitoEscalonadoId { get; set; }
        public EfeitoEscalonado EfeitoEscalonado { get; set; }
    }

}
