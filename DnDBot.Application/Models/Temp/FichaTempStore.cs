using DnDBot.Application.Models.Ficha;
using System.Collections.Concurrent;

public static class FichaTempStore
{
    // Armazena fichas temporárias associadas ao ID do jogador
    private static readonly ConcurrentDictionary<ulong, FichaPersonagem> _fichasTemporarias = new();

    /// <summary>
    /// Salva ou sobrescreve completamente a ficha temporária de um jogador.
    /// </summary>
    public static void SaveFicha(ulong jogadorId, FichaPersonagem ficha)
    {
        _fichasTemporarias[jogadorId] = ficha;
    }

    /// <summary>
    /// Retorna a ficha temporária de um jogador, se existir.
    /// </summary>
    public static FichaPersonagem GetFicha(ulong jogadorId)
    {
        return _fichasTemporarias.TryGetValue(jogadorId, out var ficha) ? ficha : null;
    }

    /// <summary>
    /// Remove a ficha temporária de um jogador.
    /// </summary>
    public static void RemoveFicha(ulong jogadorId)
    {
        _fichasTemporarias.TryRemove(jogadorId, out _);
    }

    /// <summary>
    /// Salva apenas o nome da ficha, criando uma nova ficha básica.
    /// </summary>
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
    /// Atualiza campos específicos da ficha temporária, se fornecidos.
    /// </summary>
    public static void UpdateFicha(ulong idJogador,
        string idRaca = null,
        string idClasse = null,
        string idAntecedente = null,
        string idAlinhamento = null,
        string idSubraca = null)
    {
        if (!_fichasTemporarias.TryGetValue(idJogador, out var ficha))
            return;

        if (idRaca != null)
            ficha.IdRaca = idRaca;
        if (idClasse != null)
            ficha.IdClasse = idClasse;
        if (idAntecedente != null)
            ficha.IdAntecedente = idAntecedente;
        if (idAlinhamento != null)
            ficha.IdAlinhamento = idAlinhamento;
        if (idSubraca != null)
            ficha.IdSubraca = idSubraca;
    }

    /// <summary>
    /// Atualiza a ficha temporária inteira.
    /// </summary>
    public static void UpdateFicha(ulong jogadorId, FichaPersonagem fichaAtualizada)
    {
        _fichasTemporarias[jogadorId] = fichaAtualizada;
    }
}
