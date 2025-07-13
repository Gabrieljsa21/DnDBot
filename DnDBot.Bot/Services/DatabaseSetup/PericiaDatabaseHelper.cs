using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class PericiaDatabaseHelper
{
    private const string CaminhoJson = "Data/pericias.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var pericias = await JsonLoaderHelper.CarregarAsync<List<Pericia>>(CaminhoJson, "pericias", options);

        if (pericias == null || pericias.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma perícia encontrada no JSON.");
            return;
        }

        foreach (var pericia in pericias)
        {
            if (await RegistroExisteAsync(connection, transaction, "Pericia", pericia.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(pericia);
            parametros["Id"] = pericia.Id;
            parametros["AtributoBase"] = pericia.AtributoBase.ToString();

            await InserirEntidadeFilhaAsync(connection, transaction, "Pericia", parametros);

            await InserirDificuldadesAsync(connection, transaction, pericia.Id, pericia.Dificuldades);
        }

        Console.WriteLine("✅ Perícias populadas.");
    }

    private static async Task InserirDificuldadesAsync(SqliteConnection conn, SqliteTransaction tx, string periciaId, IEnumerable<DificuldadePericia> dificuldades)
    {
        if (dificuldades == null)
            return;

        foreach (var d in dificuldades)
        {
            var parametros = new Dictionary<string, object>
            {
                ["PericiaId"] = periciaId,
                ["Tipo"] = d.Tipo ?? "",
                ["Valor"] = d.Valor
            };

            await InserirEntidadeFilhaAsync(conn, tx, "DificuldadePericia", parametros);
        }
    }
}
