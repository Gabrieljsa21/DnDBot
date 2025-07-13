using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class RacaDatabaseHelper
{
    private const string CaminhoJson = "Data/racas.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var racas = await JsonLoaderHelper.CarregarAsync<List<Raca>>(CaminhoJson, "racas", options);

        if (racas == null || racas.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma raça encontrada no JSON.");
            return;
        }

        foreach (var raca in racas)
        {
            await InserirRaca(connection, transaction, raca);
            await InserirSubRacas(connection, transaction, raca.Id, raca.SubRaca);
            await InserirTagsAsync(connection, transaction, "RacaTag", "RacaId", raca.Id, raca.Tags);
        }

        Console.WriteLine("✅ Raças e sub-raças populadas.");
    }

    private static async Task InserirRaca(SqliteConnection conn, SqliteTransaction tx, Raca raca)
    {
        if (await RegistroExisteAsync(conn, tx, "Raca", raca.Id))
            return;

        var parametros = GerarParametrosEntidadeBase(raca);
        await InserirEntidadeFilhaAsync(conn, tx, "Raca", parametros);
    }

    private static async Task InserirSubRacas(SqliteConnection conn, SqliteTransaction tx, string racaId, IEnumerable<SubRaca> subracas)
    {
        if (subracas == null)
            return;

        foreach (var sub in subracas)
        {
            if (string.IsNullOrWhiteSpace(sub.Id))
                throw new InvalidOperationException("Sub-raça sem ID definido.");

            if (await RegistroExisteAsync(conn, tx, "SubRaca", sub.Id))
                continue;

            await InserirSubRaca(conn, tx, sub, racaId);
            await InserirSubRacaAlinhamentos(conn, tx, sub);
        }
    }

    private static async Task InserirSubRaca(SqliteConnection conn, SqliteTransaction tx, SubRaca sub, string racaId)
    {
        var parametros = GerarParametrosEntidadeBase(sub);
        parametros["Id"] = sub.Id;
        parametros["RacaId"] = racaId;
        parametros["Tamanho"] = sub.Tamanho;
        parametros["Deslocamento"] = sub.Deslocamento;
        parametros["VisaoNoEscuro"] = sub.VisaoNoEscuro;

        await InserirEntidadeFilhaAsync(conn, tx, "SubRaca", parametros);
    }

    private static async Task InserirSubRacaAlinhamentos(SqliteConnection conn, SqliteTransaction tx, SubRaca sub)
    {
        if (sub.AlinhamentosComuns == null) return;

        var alinhamentos = new List<(string subId, string alinhamentoId)>();

        foreach (var alinhamento in sub.AlinhamentosComuns)
        {
            var alinhamentoId = alinhamento.AlinhamentoId ?? alinhamento.Alinhamento?.Id;

            if (string.IsNullOrWhiteSpace(alinhamentoId))
            {
                Console.WriteLine($"⚠ AlinhamentoId ausente em SubRaca '{sub.Id}'. Ignorado.");
                continue;
            }

            alinhamentos.Add((sub.Id, alinhamentoId));
        }

        await InserirRelacionamentoSimplesAsync(
            conn,
            tx,
            "SubRacaAlinhamento",
            new[] { "SubRacaId", "AlinhamentoId" },
            alinhamentos,
            a => new object[] { a.subId, a.alinhamentoId }
        );
    }
}
