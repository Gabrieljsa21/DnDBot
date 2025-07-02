using DnDBot.Application.Models;
using System.Collections.Generic;

/// <summary>
/// Representa um antecedente (background) de personagem em D&D, contendo características, perícias,
/// equipamentos, ideais, vínculos, defeitos e outras informações relacionadas.
/// </summary>
public class Antecedente
{
    /// <summary>
    /// Identificador único do antecedente.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nome do antecedente.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada do antecedente.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Lista dos IDs das perícias concedidas por este antecedente.
    /// </summary>
    public List<string> IdPericias { get; set; } = new();

    /// <summary>
    /// Lista dos IDs das ferramentas concedidas por este antecedente.
    /// </summary>
    public List<string> IdFerramentas { get; set; } = new();

    /// <summary>
    /// Lista dos IDs das línguas concedidas por este antecedente.
    /// </summary>
    public List<string> IdLinguas { get; set; } = new();

    /// <summary>
    /// Lista dos IDs das features (características especiais) deste antecedente.
    /// </summary>
    public List<string> IdFeature { get; set; } = new();

    /// <summary>
    /// Quantidade adicional de idiomas que o personagem recebe com este antecedente.
    /// </summary>
    public int IdiomasAdicionais { get; set; } = 0;

    /// <summary>
    /// Opções de escolha de línguas adicionais que o jogador pode selecionar.
    /// </summary>
    public OpcaoEscolha<string> OpcoesLinguas { get; set; } = new();

    /// <summary>
    /// Opções de escolha de equipamentos que o jogador pode selecionar.
    /// </summary>
    public OpcaoEscolha<string> OpcoesEquipamentos { get; set; } = new();

    /// <summary>
    /// Opções de escolha de perícias adicionais que o jogador pode selecionar.
    /// </summary>
    public OpcaoEscolha<string> OpcoesPericias { get; set; } = new();

    /// <summary>
    /// Equipamentos detalhados (com propriedades específicas) concedidos por este antecedente.
    /// </summary>
    public List<Equipamento> EquipamentosDetalhados { get; set; } = new();

    /// <summary>
    /// Lista dos IDs dos ideais associados a este antecedente.
    /// </summary>
    public List<string> IdIdeais { get; set; } = new();

    /// <summary>
    /// Lista dos IDs dos vínculos (connections) relacionados a este antecedente.
    /// </summary>
    public List<string> IdVinculos { get; set; } = new();

    /// <summary>
    /// Lista dos IDs dos defeitos associados a este antecedente.
    /// </summary>
    public List<string> IdDefeitos { get; set; } = new();

    /// <summary>
    /// Requisitos para utilizar este antecedente (restrições ou pré-condições).
    /// </summary>
    public string Requisitos { get; set; } = string.Empty;

    /// <summary>
    /// Tags para categorização ou filtragem do antecedente.
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    /// Fonte oficial de onde o antecedente foi extraído (livro, suplemento, etc).
    /// </summary>
    public string Fonte { get; set; } = string.Empty;

    /// <summary>
    /// Página da fonte oficial onde o antecedente aparece.
    /// </summary>
    public string Pagina { get; set; } = string.Empty;

    /// <summary>
    /// Versão do conteúdo do antecedente (por exemplo, edição do livro ou atualização).
    /// </summary>
    public string Versao { get; set; } = string.Empty;

    /// <summary>
    /// Lista de moedas iniciais (riqueza) que o personagem começa com este antecedente.
    /// </summary>
    public List<Moeda> RiquezaInicial { get; set; } = new();
}
