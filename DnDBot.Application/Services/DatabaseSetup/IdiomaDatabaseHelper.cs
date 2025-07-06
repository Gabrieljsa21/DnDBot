using DnDBot.Application.Helpers;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;

public static class IdiomaDatabaseHelper
{
    private const string CaminhoJson = "Data/idiomas.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicaoIdioma = string.Join(",\n", new[]
        {
            "Id TEXT PRIMARY KEY",
            "Categoria TEXT NOT NULL",
            SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim().TrimEnd(',')
        });

        await SqliteHelper.CriarTabelaAsync(cmd, "Idioma", definicaoIdioma);
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo idiomas.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo dados de idiomas.json...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var idiomas = JsonSerializer.Deserialize<List<Idioma>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        });

        if (idiomas == null || idiomas.Count == 0)
        {
            Console.WriteLine("❌ Nenhum idioma encontrado no JSON.");
            return;
        }

        foreach (var idioma in idiomas)
        {
            if (await SqliteHelper.RegistroExisteAsync(connection, transaction, "Idioma", idioma.Id))
                continue;

            var parametros = SqliteHelper.GerarParametrosEntidadeBase(idioma);
            parametros["categoria"] = idioma.Categoria.ToString();

            var sql = $@"
                INSERT INTO Idioma (
                    Id, Categoria,
                    {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "")}
                ) VALUES (
                    $id, $categoria,
                    {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "")}
                )";

            var cmd = SqliteHelper.CriarInsertCommand(connection, transaction, sql, parametros);
            await cmd.ExecuteNonQueryAsync();
        }

        Console.WriteLine("✅ Idiomas populados com sucesso.");
    }
}
