﻿using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Concurrent;

public static class FichaTempStore
{
    private static readonly ConcurrentDictionary<ulong, FichaPersonagem> _fichasTemporarias = new();

    public static void SaveFicha(ulong jogadorId, FichaPersonagem ficha)
    {
        _fichasTemporarias[jogadorId] = ficha;
        Console.WriteLine($"[LOG] Ficha salva temporariamente para jogador {jogadorId} (Nome: {ficha.Nome}, ID: {ficha.Id})");
    }

    public static FichaPersonagem GetFicha(ulong jogadorId)
    {
        if (_fichasTemporarias.TryGetValue(jogadorId, out var ficha))
        {
            Console.WriteLine($"[LOG] Ficha recuperada para jogador {jogadorId} (Nome: {ficha.Nome}, ID: {ficha.Id})");
            return ficha;
        }

        Console.WriteLine($"[LOG] Nenhuma ficha temporária encontrada para jogador {jogadorId}");
        return null;
    }

    public static void RemoveFicha(ulong jogadorId)
    {
        if (_fichasTemporarias.TryRemove(jogadorId, out var ficha))
            Console.WriteLine($"[LOG] Ficha removida da memória para jogador {jogadorId} (Nome: {ficha.Nome})");
        else
            Console.WriteLine($"[LOG] Tentativa de remover ficha falhou: nenhuma ficha encontrada para jogador {jogadorId}");
    }

    public static void SavePartialFicha(ulong jogadorId, string nome)
    {
        var ficha = new FichaPersonagem
        {
            JogadorId = jogadorId,
            Nome = nome,
            RacaId = "Não definida",
            ClasseId = "Não definida",
            AntecedenteId = "Não definido",
            AlinhamentoId = "Não definido",
            EtapaAtual = EtapaCriacaoFicha.RacaClasseAntecedenteAlinhamento
        };

        SaveFicha(jogadorId, ficha);
        Console.WriteLine($"[LOG] Ficha parcial criada para jogador {jogadorId} com nome '{nome}'");
    }

    public static void UpdateFicha(
        ulong idJogador,
        string idRaca = null,
        string idClasse = null,
        string idAntecedente = null,
        string idAlinhamento = null,
        string idSubraca = null,
        EtapaCriacaoFicha? novaEtapa = null)
    {
        if (!_fichasTemporarias.TryGetValue(idJogador, out var ficha))
        {
            Console.WriteLine($"[LOG] Não foi possível atualizar ficha: ficha não encontrada para jogador {idJogador}");
            return;
        }

        if (idRaca != null) { ficha.RacaId = idRaca; Console.WriteLine($"[LOG] Raça atualizada para {idRaca}"); }
        if (idClasse != null) { ficha.ClasseId = idClasse; Console.WriteLine($"[LOG] Classe atualizada para {idClasse}"); }
        if (idAntecedente != null) { ficha.AntecedenteId = idAntecedente; Console.WriteLine($"[LOG] Antecedente atualizado para {idAntecedente}"); }
        if (idAlinhamento != null) { ficha.AlinhamentoId = idAlinhamento; Console.WriteLine($"[LOG] Alinhamento atualizado para {idAlinhamento}"); }
        if (idSubraca != null) { ficha.SubracaId = idSubraca; Console.WriteLine($"[LOG] Sub-raça atualizada para {idSubraca}"); }

        if (novaEtapa.HasValue)
        {
            ficha.EtapaAtual = novaEtapa.Value;
            Console.WriteLine($"[LOG] Etapa atualizada para {ficha.EtapaAtual}");
        }
    }

    public static void UpdateFicha(ulong jogadorId, FichaPersonagem fichaAtualizada)
    {
        _fichasTemporarias[jogadorId] = fichaAtualizada;
    }

    public static FichaPersonagem GetOrCreateFicha(ulong jogadorId)
    {
        var ficha = _fichasTemporarias.GetOrAdd(jogadorId, id => new FichaPersonagem
        {
            JogadorId = id,
            EtapaAtual = EtapaCriacaoFicha.Inicio
        });
        
        return ficha;
    }
}
