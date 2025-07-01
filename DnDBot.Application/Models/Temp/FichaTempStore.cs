using DnDBot.Application.Models;
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
            JogadorId = jogadorId,
            Nome = nome,
            Raca = "Não definida",
            Classe = "Não definida",
            Antecedente = "Não definido",
            Alinhamento = "Não definido"
        };

        SaveFicha(jogadorId, ficha);
    }

    /// <summary>
    /// Atualiza campos específicos da ficha temporária, se fornecidos.
    /// </summary>
    public static void UpdateFicha(ulong jogadorId,
        string raca = null,
        string classe = null,
        string antecedente = null,
        string alinhamento = null,
        string subraca = null)
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
        if (subraca != null)
            ficha.Subraca = subraca;
    }

    /// <summary>
    /// Atualiza a ficha temporária inteira.
    /// </summary>
    public static void UpdateFicha(ulong jogadorId, FichaPersonagem fichaAtualizada)
    {
        _fichasTemporarias[jogadorId] = fichaAtualizada;
    }
}
