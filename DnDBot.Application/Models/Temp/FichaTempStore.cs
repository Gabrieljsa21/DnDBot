using DnDBot.Application.Models.Ficha;
using System.Collections.Concurrent;

/// <summary>
/// Armazena fichas de personagens temporárias em memória, associadas ao ID do jogador.
/// Útil para operações intermediárias antes de persistência no banco.
/// </summary>
public static class FichaTempStore
{
    private static readonly ConcurrentDictionary<ulong, FichaPersonagem> _fichasTemporarias = new();

    /// <summary>
    /// Salva ou substitui completamente a ficha temporária de um jogador.
    /// </summary>
    /// <param name="jogadorId">ID único do jogador.</param>
    /// <param name="ficha">Objeto completo da ficha do personagem.</param>
    public static void SaveFicha(ulong jogadorId, FichaPersonagem ficha)
    {
        _fichasTemporarias[jogadorId] = ficha;
    }

    /// <summary>
    /// Retorna a ficha temporária associada ao jogador, se existir.
    /// </summary>
    /// <param name="jogadorId">ID do jogador.</param>
    /// <returns>Ficha do personagem, ou null se não encontrada.</returns>
    public static FichaPersonagem GetFicha(ulong jogadorId)
    {
        return _fichasTemporarias.TryGetValue(jogadorId, out var ficha) ? ficha : null;
    }

    /// <summary>
    /// Remove a ficha temporária associada ao jogador.
    /// </summary>
    /// <param name="jogadorId">ID do jogador.</param>
    public static void RemoveFicha(ulong jogadorId)
    {
        _fichasTemporarias.TryRemove(jogadorId, out _);
    }

    /// <summary>
    /// Cria uma nova ficha básica com nome definido, preenchendo valores padrões nos demais campos.
    /// </summary>
    /// <param name="jogadorId">ID do jogador.</param>
    /// <param name="nome">Nome do personagem.</param>
    public static void SavePartialFicha(ulong jogadorId, string nome)
    {
        var ficha = new FichaPersonagem
        {
            IdJogador = jogadorId,
            Nome = nome,
            IdRaca = "Não definida",
            IdClasse = "Não definida",
            IdAntecedente = "Não definido",
            IdAlinhamento = "Não definido"
        };

        SaveFicha(jogadorId, ficha);
    }

    /// <summary>
    /// Atualiza campos específicos da ficha temporária de um jogador, se fornecidos.
    /// </summary>
    /// <param name="idJogador">ID do jogador.</param>
    /// <param name="idRaca">ID da raça (opcional).</param>
    /// <param name="idClasse">ID da classe (opcional).</param>
    /// <param name="idAntecedente">ID do antecedente (opcional).</param>
    /// <param name="idAlinhamento">ID do alinhamento (opcional).</param>
    /// <param name="idSubraca">ID da sub-raça (opcional).</param>
    public static void UpdateFicha(
        ulong idJogador,
        string idRaca = null,
        string idClasse = null,
        string idAntecedente = null,
        string idAlinhamento = null,
        string idSubraca = null)
    {
        if (!_fichasTemporarias.TryGetValue(idJogador, out var ficha))
            return;

        if (idRaca != null) ficha.IdRaca = idRaca;
        if (idClasse != null) ficha.IdClasse = idClasse;
        if (idAntecedente != null) ficha.IdAntecedente = idAntecedente;
        if (idAlinhamento != null) ficha.IdAlinhamento = idAlinhamento;
        if (idSubraca != null) ficha.IdSubraca = idSubraca;
    }

    /// <summary>
    /// Substitui completamente a ficha temporária do jogador com uma nova ficha.
    /// </summary>
    /// <param name="jogadorId">ID do jogador.</param>
    /// <param name="fichaAtualizada">Nova ficha a ser salva.</param>
    public static void UpdateFicha(ulong jogadorId, FichaPersonagem fichaAtualizada)
    {
        _fichasTemporarias[jogadorId] = fichaAtualizada;
    }

    /// <summary>
    /// Obtém a ficha temporária do jogador ou cria uma nova, caso não exista.
    /// </summary>
    /// <param name="jogadorId">ID do jogador.</param>
    /// <returns>Ficha existente ou nova instância criada.</returns>
    public static FichaPersonagem GetOrCreateFicha(ulong jogadorId)
    {
        if (!_fichasTemporarias.TryGetValue(jogadorId, out var ficha))
        {
            ficha = new FichaPersonagem { IdJogador = jogadorId };
            _fichasTemporarias[jogadorId] = ficha;
        }

        return ficha;
    }
}
