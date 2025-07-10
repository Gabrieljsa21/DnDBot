using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public static class SubRacaResistenciaDatabaseHelper
{
    private const string CaminhoJson = "Data/subracasresistencias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS SubRacaResistencia (
                SubRacaId TEXT NOT NULL,
                ResistenciaId TEXT NOT NULL,
                PRIMARY KEY (SubRacaId, ResistenciaId),
                FOREIGN KEY (SubRacaId) REFERENCES SubRaca(Id) ON DELETE CASCADE,
                FOREIGN KEY (ResistenciaId) REFERENCES Resistencia(Id) ON DELETE CASCADE
            )";
        cmd.CommandText = sql;
        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo subracasresistencias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de subracasresistencias.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson);
        var resistenciasPorSubraca = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);

        if (resistenciasPorSubraca == null)
        {
            Console.WriteLine("❌ Erro ao desserializar subracasresistencias.json");
            return;
        }

        foreach (var kvp in resistenciasPorSubraca)
        {
            string subRacaId = kvp.Key;
            List<string> tiposDano = kvp.Value;

            foreach (var tipoDanoStr in tiposDano)
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

                var sql = "INSERT OR IGNORE INTO SubRacaResistencia (SubRacaId, ResistenciaId) VALUES ($subId, $resistenciaId)";
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("$subId", subRacaId);
                cmd.Parameters.AddWithValue("$resistenciaId", resistencia.Id);
                await cmd.ExecuteNonQueryAsync();
            }

        }

        Console.WriteLine("✅ Resistências de sub-raças populadas.");
    }
}
