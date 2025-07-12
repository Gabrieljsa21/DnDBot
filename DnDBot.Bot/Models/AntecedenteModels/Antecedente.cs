using DnDBot.Bot.Models;
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

        /// <summary>
        /// Lista das ferramentas concedidas por este antecedente.
        /// </summary>
        public List<AntecedenteProficiencia> Proficiencia { get; set; } = new();

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

        /// <summary>
        /// Ideais associados ao antecedente, que ajudam a moldar a personalidade do personagem.
        /// </summary>
        public List<AntecedenteIdeal> Ideais { get; set; } = new();

        /// <summary>
        /// Vínculos do personagem com pessoas, lugares ou eventos ligados a este antecedente.
        /// </summary>
        public List<AntecedenteVinculo> Vinculos { get; set; } = new();

        /// <summary>
        /// Defeitos ou fraquezas típicas associadas ao antecedente.
        /// </summary>
        public List<AntecedenteDefeito> Defeitos { get; set; } = new();

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
