using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.AntecedenteModels
{
    /// <summary>
    /// Representa um antecedente (background) de personagem em Dungeons & Dragons,
    /// contendo informações sobre habilidades, equipamentos, características, e outras propriedades relacionadas.
    /// </summary>
    public class Antecedente : EntidadeBase
    {

        /// <summary>
        /// Lista das perícias concedidas por este antecedente.
        /// </summary>
        public List<AntecedentePericia> Pericias { get; set; } = new();

        /// <summary>
        /// Lista das ferramentas concedidas por este antecedente.
        /// </summary>
        public List<AntecedenteFerramenta> Ferramentas { get; set; } = new();

        /// <summary>
        /// Lista das línguas que o personagem aprende com este antecedente.
        /// </summary>
        public List<AntecedenteIdioma> Idiomas { get; set; } = new();

        /// <summary>
        /// Características especiais (features) concedidas por este antecedente.
        /// </summary>
        public List<AntecedenteCaracteristica> Caracteristicas { get; set; } = new();

        /// <summary>
        /// Equipamentos detalhados que este antecedente concede ao personagem.
        /// </summary>
        public List<AntecedenteItem> itens { get; set; } = new();

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
        /// Riqueza inicial representada pela lista de moedas que o personagem recebe ao escolher este antecedente.
        /// </summary>
        public List<AntecedenteMoeda> Moedas { get; set; } = new();

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Raca_Tag.
        /// </summary>
        public List<AntecedenteTag> AntecedenteTags { get; set; } = new();



        /// <summary>
        /// Quantidade adicional de idiomas que o personagem pode escolher com este antecedente.
        /// </summary>
        public int IdiomasAdicionais { get; set; } = 0;

        /// <summary>
        /// Opções de escolha para idiomas adicionais — permite que o jogador selecione idiomas extras.
        /// Esta propriedade não é mapeada no banco de dados.
        /// </summary>
        [NotMapped]
        public OpcaoEscolha<Idioma> OpcoesIdiomas { get; set; } = new();

        /// <summary>
        /// Opções de escolha para equipamentos adicionais — permite seleção de equipamentos.
        /// Esta propriedade não é mapeada no banco de dados.
        /// </summary>
        [NotMapped]
        public OpcaoEscolha<InventarioItem> OpcoesEquipamentos { get; set; } = new();

        /// <summary>
        /// Opções de escolha para perícias adicionais — permite seleção de perícias extras.
        /// Esta propriedade não é mapeada no banco de dados.
        /// </summary>
        [NotMapped]
        public OpcaoEscolha<Pericia> OpcoesPericias { get; set; } = new();


        /// <summary>
        /// Tags derivadas da lista de RacaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => AntecedenteTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => AntecedenteTags = value?.Select(tag => new AntecedenteTag { Tag = tag, AntecedenteId = Id }).ToList() ?? new();
        }

    }
}
