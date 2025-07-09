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

public static class PericiaDatabaseHelper
{
    private const string CaminhoJson = "Data/pericias.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var comandos = new[]
        {
            $@"
            CREATE TABLE IF NOT EXISTS Pericia (
                Id TEXT PRIMARY KEY,
                AtributoBase TEXT NOT NULL,
                {SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "")}
            );",
            @"
            CREATE TABLE IF NOT EXISTS DificuldadePericia (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Tipo TEXT NOT NULL,
                Valor INTEGER NOT NULL,
                PericiaId TEXT NOT NULL,
                FOREIGN KEY (PericiaId) REFERENCES Pericia(Id) ON DELETE CASCADE
            );"
        };

        foreach (var sql in comandos)
        {
            cmd.CommandText = sql;
            await cmd.ExecuteNonQueryAsync();
        }
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo pericias.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de pericias.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var pericias = JsonSerializer.Deserialize<List<Pericia>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (pericias == null)
        {
            Console.WriteLine("❌ Falha ao desserializar pericias.json.");
            return;
        }

        foreach (var pericia in pericias)
        {
            if (await RegistroExisteAsync(connection, transaction, "Pericia", pericia.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(pericia);
            parametros["atributoBase"] = pericia.AtributoBase.ToString();

            var sql = $@"
                INSERT INTO Pericia (
                    Id, AtributoBase,
                    {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "")}
                ) VALUES (
                    $id, $atributoBase,
                    {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "")}
                );";

            var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
            await cmd.ExecuteNonQueryAsync();

            await InserirDificuldades(connection, transaction, pericia.Id, pericia.Dificuldades);
        }

        Console.WriteLine("✅ Perícias populadas.");
    }

    private static async Task InserirDificuldades(SqliteConnection conn, SqliteTransaction tx, string periciaId, IEnumerable<DificuldadePericia> dificuldades)
    {
        foreach (var d in dificuldades ?? Array.Empty<DificuldadePericia>())
        {
            var insert = conn.CreateCommand();
            insert.Transaction = tx;
            insert.CommandText = @"
                INSERT OR IGNORE INTO DificuldadePericia (Tipo, Valor, PericiaId)
                VALUES ($tipo, $valor, $pid);";
            insert.Parameters.AddWithValue("$tipo", d.Tipo ?? "");
            insert.Parameters.AddWithValue("$valor", d.Valor);
            insert.Parameters.AddWithValue("$pid", periciaId);
            await insert.ExecuteNonQueryAsync();
        }
    }
}
