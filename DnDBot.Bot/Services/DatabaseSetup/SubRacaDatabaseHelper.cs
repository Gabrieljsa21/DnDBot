using DnDBot.Bot.Data;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class SubRacaDatabaseHelper
{
    private const string CaminhoJsonCaracteristicas = "Data/subracascaracteristicas.json";
    private const string CaminhoJsonIdiomas = "Data/subracasidiomas.json";
    private const string CaminhoJsonMagias = "Data/subracasmagias.json";
    private const string CaminhoJsonProficiencias = "Data/subracasproficiencias.json";
    private const string CaminhoJsonResistencias = "Data/subracasresistencias.json";

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        var caracteristicasPorSubraca = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonCaracteristicas, "subracascaracteristicas") ?? new();
        var idiomasPorSubraca = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonIdiomas, "subracasidiomas") ?? new();
        var magiasPorSubraca = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonMagias, "subracasmagias") ?? new();
        var profsPorSubraca = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonProficiencias, "subracasproficiencias") ?? new();
        var resistenciasPorSubraca = await JsonLoaderHelper.CarregarAsync<Dictionary<string, List<string>>>(CaminhoJsonResistencias, "subracasresistencias") ?? new();

        var todasSubRacas = new HashSet<string>();
        foreach (var d in new[] { caracteristicasPorSubraca, idiomasPorSubraca, magiasPorSubraca, profsPorSubraca, resistenciasPorSubraca })
            foreach (var key in d.Keys)
                todasSubRacas.Add(key);

        foreach (var subRacaId in todasSubRacas)
        {
            // Características
            if (caracteristicasPorSubraca.TryGetValue(subRacaId, out var caracteristicaIds))
            {
                var validas = caracteristicaIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
                await SqliteHelper.InserirRelacionamentoSimplesAsync(conn, tx, "SubRacaCaracteristica", new[] { "SubRacaId", "CaracteristicaId" }, validas, id => new object[] { subRacaId, id });
            }

            // Idiomas
            if (idiomasPorSubraca.TryGetValue(subRacaId, out var idiomaIds))
            {
                var validas = idiomaIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
                await SqliteHelper.InserirRelacionamentoSimplesAsync(conn, tx, "SubRacaIdioma", new[] { "SubRacaId", "IdiomaId" }, validas, id => new object[] { subRacaId, id });
            }

            // Magias
            if (magiasPorSubraca.TryGetValue(subRacaId, out var magiaIds))
            {
                var validas = magiaIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
                await SqliteHelper.InserirRelacionamentoSimplesAsync(conn, tx, "SubRacaMagia", new[] { "SubRacaId", "MagiaId" }, validas, id => new object[] { subRacaId, id });
            }

            // Proficiências
            if (profsPorSubraca.TryGetValue(subRacaId, out var profIds))
            {
                var validas = profIds.Where(id => !string.IsNullOrWhiteSpace(id)).ToList();
                await SqliteHelper.InserirRelacionamentoSimplesAsync(conn, tx, "SubRacaProficiencia", new[] { "SubRacaId", "ProficienciaId" }, validas, id => new object[] { subRacaId, id });
            }

            // Resistências
            if (resistenciasPorSubraca.TryGetValue(subRacaId, out var tiposDanoStr))
            {
                foreach (var tipoDanoStr in tiposDanoStr)
                {
                    if (!Enum.TryParse<TipoDano>(tipoDanoStr, ignoreCase: true, out var tipoDanoEnum))
                    {
                        Console.WriteLine($"⚠ Tipo de dano inválido: {tipoDanoStr} para SubRaça {subRacaId}. Ignorado.");
                        continue;
                    }

                    var resistencia = ResistenciasData.Resistencias.FirstOrDefault(r => r.TipoDano == tipoDanoEnum);
                    if (resistencia == null)
                    {
                        Console.WriteLine($"❌ Nenhuma resistência encontrada para tipo de dano: {tipoDanoEnum}");
                        continue;
                    }

                    var parametros = new Dictionary<string, object>
                    {
                        ["SubRacaId"] = subRacaId,
                        ["ResistenciaId"] = resistencia.Id
                    };

                    await SqliteHelper.InserirEntidadeFilhaAsync(conn, tx, "SubRacaResistencia", parametros);
                }
            }
        }

        Console.WriteLine("✅ Relacionamentos de sub-raças (características, idiomas, magias, proficiências, resistências) populados.");
    }
}
