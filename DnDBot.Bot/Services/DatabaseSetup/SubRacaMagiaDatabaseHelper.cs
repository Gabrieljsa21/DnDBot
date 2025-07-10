using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Enums;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class SubRacaMagiaDatabaseHelper
{
    private const string CaminhoJson = "Data/subracasmagias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS SubRacaMagia (
                SubRacaId TEXT NOT NULL,
                MagiaId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, MagiaId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (MagiaId) REFERENCES Magia(Id) ON DELETE CASCADE
            )";
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo subracasmagias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de subracasmagias.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var magiasPorSubraca = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

        if (magiasPorSubraca == null)
        {
            Console.WriteLine("❌ Erro ao desserializar subracasmagias.json");
            return;
        }

        foreach (var kvp in magiasPorSubraca)
        {
            string subRacaId = kvp.Key;
            List<string> magiaIds = kvp.Value;

            foreach (var magiaId in magiaIds)
            {
                if (string.IsNullOrWhiteSpace(magiaId))
                {
                    Console.WriteLine($"⚠ MagiaId inválido para SubRaça {subRacaId}. Ignorado.");
                    continue;
                }

                var sql = "INSERT OR IGNORE INTO SubRacaMagia (SubRacaId, MagiaId) VALUES ($subId, $magiaId)";
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("$subId", subRacaId);
                    cmd.Parameters.AddWithValue("$magiaId", magiaId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        Console.WriteLine("✅ Magias raciais de sub-raças populadas.");
    }
}
