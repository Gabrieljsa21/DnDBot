using DnDBot.Application.Models;
using DnDBot.Application.Models.Antecedente.Antecedente;
using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Representa um antecedente (background) de personagem em Dungeons & Dragons,
/// contendo informações sobre habilidades, equipamentos, características, e outras propriedades relacionadas.
/// </summary>
public class Antecedente
{
    /// <summary>
    /// Identificador único do antecedente (ex: "soldado", "nobre", etc).
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome legível do antecedente para exibição.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada que explica a história ou características do antecedente.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Lista das perícias concedidas por este antecedente.
    /// </summary>
    public List<Pericia> Pericias { get; set; } = new();

    /// <summary>
    /// Lista das ferramentas concedidas por este antecedente.
    /// </summary>
    public List<Ferramenta> Ferramentas { get; set; } = new();

    /// <summary>
    /// Lista das línguas que o personagem aprende com este antecedente.
    /// </summary>
    public List<Idioma> Idiomas { get; set; } = new();

    /// <summary>
    /// Características especiais (features) concedidas por este antecedente.
    /// </summary>
    public List<Caracteristica> IdFeature { get; set; } = new();

    /// <summary>
    /// Quantidade adicional de idiomas que o personagem pode escolher com este antecedente.
    /// </summary>
    public int IdiomasAdicionais { get; set; } = 0;

    /// <summary>
    /// Opções de escolha para idiomas adicionais — permite que o jogador selecione idiomas extras.
    /// Esta propriedade não é mapeada no banco de dados.
    /// </summary>
    [NotMapped]
    public OpcaoEscolha<Idioma> OpcoesLinguas { get; set; } = new();

    /// <summary>
    /// Opções de escolha para equipamentos adicionais — permite seleção de equipamentos.
    /// Esta propriedade não é mapeada no banco de dados.
    /// </summary>
    [NotMapped]
    public OpcaoEscolha<Equipamento> OpcoesEquipamentos { get; set; } = new();

    /// <summary>
    /// Opções de escolha para perícias adicionais — permite seleção de perícias extras.
    /// Esta propriedade não é mapeada no banco de dados.
    /// </summary>
    [NotMapped]
    public OpcaoEscolha<Pericia> OpcoesPericias { get; set; } = new();

    /// <summary>
    /// Equipamentos detalhados que este antecedente concede ao personagem.
    /// </summary>
    public List<Equipamento> EquipamentosDetalhados { get; set; } = new();

    /// <summary>
    /// Ideais associados ao antecedente, que ajudam a moldar a personalidade do personagem.
    /// </summary>
    public List<Ideal> Ideais { get; set; } = new();

    /// <summary>
    /// Vínculos do personagem com pessoas, lugares ou eventos ligados a este antecedente.
    /// </summary>
    public List<Vinculo> Vinculos { get; set; } = new();

    /// <summary>
    /// Defeitos ou fraquezas típicas associadas ao antecedente.
    /// </summary>
    public List<Defeito> Defeitos { get; set; } = new();

    /// <summary>
    /// Requisitos para que o personagem possa ter este antecedente (pode ser usado para restrições).
    /// </summary>
    public string Requisitos { get; set; } = string.Empty;

    /// <summary>
    /// Tags para categorizar e facilitar filtros ou buscas pelo antecedente.
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Fonte oficial de onde o antecedente foi retirado (nome do livro, suplemento, etc).
    /// </summary>
    public string Fonte { get; set; } = string.Empty;

    /// <summary>
    /// Página da fonte oficial onde o antecedente é descrito.
    /// </summary>
    public string Pagina { get; set; } = string.Empty;

    /// <summary>
    /// Versão do conteúdo do antecedente (pode ser usado para controle de edições).
    /// </summary>
    public string Versao { get; set; } = string.Empty;

    /// <summary>
    /// Riqueza inicial representada pela lista de moedas que o personagem recebe ao escolher este antecedente.
    /// </summary>
    public List<Moeda> RiquezaInicial { get; set; } = new();
}
