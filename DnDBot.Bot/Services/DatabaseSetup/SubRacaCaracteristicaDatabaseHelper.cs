using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class SubRacaCaracteristicaDatabaseHelper
{
    private const string CaminhoJson = "Data/subracascaracteristicas.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS SubRacaCaracteristica (
                SubRacaId TEXT NOT NULL,
                CaracteristicaId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, CaracteristicaId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (CaracteristicaId) REFERENCES Caracteristica(Id) ON DELETE CASCADE
            )";
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo subracascaracteristicas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de subracascaracteristicas.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Dictionary<string, List<string>> caracteristicasPorSubraca = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json, options);

        if (caracteristicasPorSubraca == null)
        {
            Console.WriteLine("❌ Erro ao desserializar subracascaracteristicas.json");
            return;
        }

        foreach (var kvp in caracteristicasPorSubraca)
        {
            string subRacaId = kvp.Key;
            List<string> caracteristicaIds = kvp.Value;

            foreach (var caracteristicaId in caracteristicaIds)
            {
                var sql = "INSERT OR IGNORE INTO SubRacaCaracteristica (SubRacaId, CaracteristicaId) VALUES ($subId, $caracteristicaId)";
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("$subId", subRacaId);
                cmd.Parameters.AddWithValue("$caracteristicaId", caracteristicaId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        Console.WriteLine("✅ Características de sub-raças populadas.");
    }
}
