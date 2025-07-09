using DnDBot.Bot.Models;
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

public static class ProficienciaDatabaseHelper
{
    private const string CaminhoJson = "Data/proficiencias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var sql = $@"
        CREATE TABLE IF NOT EXISTS Proficiencia (
            Id TEXT PRIMARY KEY,
            Tipo TEXT NOT NULL,
            TemEspecializacao INTEGER NOT NULL,
            BonusAdicional INTEGER NOT NULL,
            {SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "")}
        );";

        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }


    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo proficiencias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de proficiencias.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var proficiencias = JsonSerializer.Deserialize<List<Proficiencia>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (proficiencias == null)
        {
            Console.WriteLine("❌ Falha ao desserializar proficiencias.json.");
            return;
        }

        foreach (var prof in proficiencias)
        {
            if (await RegistroExisteAsync(connection, transaction, "Proficiencia", prof.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(prof);
            parametros["tipo"] = prof.Tipo.ToString();
            parametros["temEspecializacao"] = prof.TemEspecializacao ? 1 : 0;
            parametros["bonusAdicional"] = prof.BonusAdicional;

            var sql = $@"
                INSERT INTO Proficiencia (
                    Id, Tipo, TemEspecializacao, BonusAdicional,
                    {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "")}
                ) VALUES (
                    $id, $tipo, $temEspecializacao, $bonusAdicional,
                    {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "")}
                );";

            var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
            await cmd.ExecuteNonQueryAsync();
        }


        Console.WriteLine("✅ Proficiências populadas.");
    }
}
