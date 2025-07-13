using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class IdiomaDatabaseHelper
{
    private const string CaminhoJson = "Data/idiomas.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var idiomas = await JsonLoaderHelper.CarregarAsync<List<Idioma>>(CaminhoJson, "idiomas");

        if (idiomas == null || idiomas.Count == 0)
        {
            Console.WriteLine("❌ Nenhum idioma encontrado no JSON.");
            return;
        }

        foreach (var idioma in idiomas)
        {
            if (await RegistroExisteAsync(connection, transaction, "Idioma", idioma.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(idioma);
            parametros["Id"] = idioma.Id;
            parametros["Categoria"] = idioma.Categoria.ToString();

            await InserirEntidadeFilhaAsync(connection, transaction, "Idioma", parametros);
        }

        Console.WriteLine("✅ Idiomas populados com sucesso.");
    }
}
