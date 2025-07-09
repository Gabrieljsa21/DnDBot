using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Enums;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class SubRacaIdiomaDatabaseHelper
{
    private const string CaminhoJson = "Data/subracasidiomas.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS SubRacaIdioma (
                SubRacaId TEXT NOT NULL,
                IdiomaId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, IdiomaId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (IdiomaId) REFERENCES Idioma(Id) ON DELETE CASCADE
            )";
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo subracasidiomas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de subracasidiomas.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var idiomasPorSubraca = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

        if (idiomasPorSubraca == null)
        {
            Console.WriteLine("❌ Erro ao desserializar subracasidiomas.json");
            return;
        }

        foreach (var kvp in idiomasPorSubraca)
        {
            string subRacaId = kvp.Key;
            List<string> idiomaIds = kvp.Value;

            foreach (var idiomaId in idiomaIds)
            {
                if (string.IsNullOrWhiteSpace(idiomaId))
                {
                    Console.WriteLine($"⚠ IdiomaId inválido para SubRaça {subRacaId}. Ignorado.");
                    continue;
                }

                var sql = "INSERT OR IGNORE INTO SubRacaIdioma (SubRacaId, IdiomaId) VALUES ($subId, $idiomaId)";
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("$subId", subRacaId);
                cmd.Parameters.AddWithValue("$idiomaId", idiomaId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        Console.WriteLine("✅ Idiomas de sub-raças populados.");
    }
}
