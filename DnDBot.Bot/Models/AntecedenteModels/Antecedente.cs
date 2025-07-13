using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.AntecedenteModels
{
    /// <summary>
    /// Representa um antecedente (background) de personagem em Dungeons & Dragons,
    /// contendo informações sobre habilidades, equipamentos, características, e outras propriedades relacionadas.
    /// </summary>
    public class Antecedente : EntidadeBase
    {
        [JsonIgnore]
        public List<AntecedenteProficiencia> Proficiencias { get; set; } = new();

        /// <summary>
        /// Lista de IDs das proficiências, usada para facilitar a serialização.
        /// </summary>
        [NotMapped]
        [JsonPropertyName("Proficiencias")]
        public List<string> ProficienciaIds
        {
            get => Proficiencias?.Select(p => p.ProficienciaId).ToList() ?? new();
            set => Proficiencias = value?.Select(id => new AntecedenteProficiencia
            {
                AntecedenteId = Id,
                ProficienciaId = id
            }).ToList() ?? new();
        }


        /// <summary>
        /// Equipamentos detalhados que este antecedente concede ao personagem.
        /// </summary>
        public List<AntecedenteProficienciaOpcoes> OpcoesProficiencia { get; set; }
        public int QntOpcoesProficiencia { get; set; }

        /// <summary>
        /// Equipamentos detalhados que este antecedente concede ao personagem.
        /// </summary>
        public List<AntecedenteItem> Itens { get; set; } = new();

        /// <summary>
        /// Opções de escolha para equipamentos adicionais — permite seleção de equipamentos.
        /// Esta propriedade não é mapeada no banco de dados.
        /// </summary>
        public List<AntecedenteItemOpcoes> OpcoesItens { get; set; } = new();
        public int QntOpcoesItem { get; set; } = 0;

        /// <summary>
        /// Número de idiomas adicionais concedidos.
        /// </summary>
        public int IdiomasAdicionais { get; set; } = 0;

        /// <summary>
        /// Quantidade fixa de ouro inicial concedida pelo antecedente.
        /// </summary>
        public int Ouro { get; set; } = 0;

        /// <summary>
        /// Características especiais (features) concedidas por este antecedente.
        /// </summary>

        [JsonIgnore]
        public List<AntecedenteCaracteristica> Caracteristicas { get; set; } = new();

        public List<AntecedenteNarrativa> Narrativas { get; set; } = new();

        // Facilita acesso separado a cada tipo de narrativa (ideal, vínculo, defeito)
        [NotMapped]
        public List<AntecedenteNarrativa> Ideais => Narrativas?.Where(n => n.Tipo == TipoNarrativa.Ideal).ToList() ?? new();

        [NotMapped]
        public List<AntecedenteNarrativa> Vinculos => Narrativas?.Where(n => n.Tipo == TipoNarrativa.Vinculo).ToList() ?? new();

        [NotMapped]
        public List<AntecedenteNarrativa> Defeitos => Narrativas?.Where(n => n.Tipo == TipoNarrativa.Defeito).ToList() ?? new();


        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Antecedente_Tag.
        /// </summary>
        public List<AntecedenteTag> AntecedenteTags { get; set; } = new();



        /// <summary>
        /// Tags derivadas da lista de AntecedenteTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => AntecedenteTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => AntecedenteTags = value?.Select(tag => new AntecedenteTag { Tag = tag, AntecedenteId = Id }).ToList() ?? new();
        }

        // Essa propriedade é usada para JSON, para receber só os IDs
        [JsonPropertyName("Caracteristicas")]
        public List<string> CaracteristicaIds
        {
            get => Caracteristicas.Select(c => c.CaracteristicaId).ToList();
            set
            {
                Caracteristicas = value?.Select(id => new AntecedenteCaracteristica
                {
                    CaracteristicaId = id,
                    AntecedenteId = this.Id
                }).ToList() ?? new List<AntecedenteCaracteristica>();
            }
        }

    }
}
