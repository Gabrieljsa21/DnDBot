using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

public static class SubRacaProficienciaDatabaseHelper
{
    private const string CaminhoJson = "Data/subracasproficiencias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS SubRacaProficiencia (
                SubRacaId TEXT NOT NULL,
                ProficienciaId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, ProficienciaId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (ProficienciaId) REFERENCES Proficiencia(Id) ON DELETE CASCADE
            )";
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo subracasproficiencias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de subracasproficiencias.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var profsPorSubraca = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

        if (profsPorSubraca == null)
        {
            Console.WriteLine("❌ Erro ao desserializar subracasproficiencias.json");
            return;
        }

        foreach (var prof in profsPorSubraca)
        {
            string subRacaId = prof.Key;
            List<string> profIds = prof.Value;

            foreach (var profId in profIds)
            {

                var sql = @"
                    INSERT OR IGNORE INTO SubRacaProficiencia 
                        (SubRacaId, ProficienciaId) 
                    VALUES 
                        ($subId, $profId)";
                using var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("$subId", subRacaId);
                cmd.Parameters.AddWithValue("$profId", profId);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        Console.WriteLine("✅ Proficiências de sub‑raças populadas.");
    }
}
