using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class RacaDatabaseHelper
{
    private const string CaminhoJson = "Data/racas.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Raca"] = SqliteEntidadeBaseHelper.Campos,
            ["SubRaca"] = string.Join(",\n", new[]
            {
                "Id TEXT PRIMARY KEY",
                "RacaId TEXT NOT NULL",
                "AlinhamentosComuns TEXT",
                "Tamanho TEXT",
                "Deslocamento INTEGER",
                "VisaoNoEscuro INTEGER",
                SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim().TrimEnd(','),
                "FOREIGN KEY (RacaId) REFERENCES Raca(Id) ON DELETE CASCADE"
            }),
            ["RacaTag"] = @"
                RacaId TEXT NOT NULL,
                Tag TEXT NOT NULL,
                PRIMARY KEY (RacaId, Tag),
                FOREIGN KEY (RacaId) REFERENCES Raca(Id) ON DELETE CASCADE",
            ["SubRacaAlinhamento"] = @"
                SubRacaId TEXT NOT NULL,
                AlinhamentoId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, AlinhamentoId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (AlinhamentoId) REFERENCES Alinhamento(Id) ON DELETE CASCADE"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo racas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de racas.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var racas = JsonSerializer.Deserialize<List<Raca>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (racas == null || racas.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma raça encontrada no JSON.");
            return;
        }

        foreach (var raca in racas)
        {
            await InserirRaca(connection, transaction, raca);
            await InserirSubRacas(connection, transaction, raca.Id, raca.SubRaca);
            await SqliteHelper.InserirTagsAsync(connection, transaction, "RacaTag", "RacaId", raca.Id, raca.Tags);
        }

        Console.WriteLine("✅ Raças e sub-raças populadas.");
    }

    private static async Task InserirRaca(SqliteConnection conn, SqliteTransaction tx, Raca raca)
    {
        if (await SqliteHelper.RegistroExisteAsync(conn, tx, "Raca", raca.Id)) return;

        var parametros = SqliteHelper.GerarParametrosEntidadeBase(raca);
        var sql = $"INSERT INTO Raca ({SqliteEntidadeBaseHelper.CamposInsert}) VALUES ({SqliteEntidadeBaseHelper.ValoresInsert})";
        var cmd = SqliteHelper.CriarInsertCommand(conn, tx, sql, parametros);
        await cmd.ExecuteNonQueryAsync();
    }

    private static async Task InserirSubRacas(SqliteConnection conn, SqliteTransaction tx, string racaId, IEnumerable<SubRaca> subracas)
    {
        if (subracas == null) return;

        foreach (var sub in subracas)
        {
            if (string.IsNullOrWhiteSpace(sub.Id))
                throw new InvalidOperationException("Sub-raça sem ID definido.");

            if (await SqliteHelper.RegistroExisteAsync(conn, tx, "SubRaca", sub.Id))
                continue;

            await InserirSubRaca(conn, tx, sub, racaId);
            await InserirSubRacaAlinhamentos(conn, tx, sub);
        }
    }

    private static async Task InserirSubRaca(SqliteConnection conn, SqliteTransaction tx, SubRaca sub, string racaId)
    {
        var parametros = SqliteHelper.GerarParametrosEntidadeBase(sub);
        parametros["id"] = sub.Id;
        parametros["racaId"] = racaId;
        parametros["tend"] = null;
        parametros["tam"] = sub.Tamanho;
        parametros["desloc"] = sub.Deslocamento;
        parametros["visao"] = sub.VisaoNoEscuro;

        var sql = $@"
            INSERT INTO SubRaca (
                Id, RacaId, Tamanho, Deslocamento, VisaoNoEscuro,
                {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "")}
            ) VALUES (
                $id, $racaId, $tam, $desloc, $visao,
                {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "")}
            )";

        var cmd = SqliteHelper.CriarInsertCommand(conn, tx, sql, parametros);
        await cmd.ExecuteNonQueryAsync();
    }

    private static async Task InserirSubRacaAlinhamentos(SqliteConnection conn, SqliteTransaction tx, SubRaca sub)
    {
        foreach (var alinhamento in sub.AlinhamentosComuns)
        {
            var alinhamentoId = alinhamento.AlinhamentoId ?? alinhamento.Alinhamento?.Id;

            if (string.IsNullOrWhiteSpace(alinhamentoId))
                throw new InvalidOperationException($"AlinhamentoId está nulo em SubRaca '{sub.Id}'.");

            var sql = "INSERT INTO SubRacaAlinhamento (SubRacaId, AlinhamentoId) VALUES ($subracaId, $alinhamentoId)";
            var cmd = SqliteHelper.CriarInsertCommand(conn, tx, sql, new Dictionary<string, object>
            {
                ["subracaId"] = sub.Id,
                ["alinhamentoId"] = alinhamentoId
            });
            await cmd.ExecuteNonQueryAsync();
        }
    }

}
