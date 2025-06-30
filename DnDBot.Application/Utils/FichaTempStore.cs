using DnDBot.Application.Models;
using System.Collections.Generic;

/// <summary>
/// Armazena fichas de personagem temporárias durante o processo de criação.
/// Utilizado para registrar escolhas parciais antes da finalização.
/// </summary>
public static class FichaTempStore
{
    /// <summary>
    /// Dicionário interno que relaciona o ID do jogador à ficha temporária.
    /// </summary>
    private static readonly Dictionary<ulong, FichaPersonagem> _fichasTemporarias = new();

    /// <summary>
    /// Salva uma ficha parcial com nome e atributos iniciais.
    /// Substitui qualquer ficha temporária existente para o jogador.
    /// </summary>
    /// <param name="jogadorId">ID do jogador no Discord.</param>
    /// <param name="nome">Nome do personagem.</param>
    /// <param name="raca">Raça escolhida (opcional, padrão: "Não definida").</param>
    /// <param name="classe">Classe escolhida (opcional, padrão: "Não definida").</param>
    /// <param name="antecedente">Antecedente escolhido (opcional, padrão: "Não definido").</param>
    /// <param name="alinhamento">Alinhamento escolhido (opcional, padrão: "Não definido").</param>
    public static void SavePartialFicha(ulong jogadorId, string nome,
        string raca = "Não definida",
        string classe = "Não definida",
        string antecedente = "Não definido",
        string alinhamento = "Não definido")
    {
        _fichasTemporarias[jogadorId] = new FichaPersonagem
        {
            JogadorId = jogadorId,
            Nome = nome,
            Raca = raca,
            Classe = classe,
            Antecedente = antecedente,
            Alinhamento = alinhamento
        };
    }

    /// <summary>
    /// Atualiza uma ficha temporária existente com novos valores parciais.
    /// Campos nulos serão ignorados (mantendo o valor anterior).
    /// </summary>
    /// <param name="jogadorId">ID do jogador no Discord.</param>
    /// <param name="raca">Nova raça (opcional).</param>
    /// <param name="classe">Nova classe (opcional).</param>
    /// <param name="antecedente">Novo antecedente (opcional).</param>
    /// <param name="alinhamento">Novo alinhamento (opcional).</param>
    public static void UpdateFicha(ulong jogadorId,
        string raca = null,
        string classe = null,
        string antecedente = null,
        string alinhamento = null)
    {
        if (!_fichasTemporarias.TryGetValue(jogadorId, out var ficha))
            return;

        if (raca != null)
            ficha.Raca = raca;
        if (classe != null)
            ficha.Classe = classe;
        if (antecedente != null)
            ficha.Antecedente = antecedente;
        if (alinhamento != null)
            ficha.Alinhamento = alinhamento;
    }

    /// <summary>
    /// Recupera a ficha temporária atual de um jogador.
    /// </summary>
    /// <param name="jogadorId">ID do jogador no Discord.</param>
    /// <returns>A ficha temporária, ou null se não houver ficha em progresso.</returns>
    public static FichaPersonagem GetFicha(ulong jogadorId)
    {
        _fichasTemporarias.TryGetValue(jogadorId, out var ficha);
        return ficha;
    }

    /// <summary>
    /// Remove a ficha temporária de um jogador (usado após finalização ou cancelamento).
    /// </summary>
    /// <param name="jogadorId">ID do jogador no Discord.</param>
    public static void RemoveFicha(ulong jogadorId)
    {
        _fichasTemporarias.Remove(jogadorId);
    }
}
