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

public static class ProficienciaDatabaseHelper
{
    private const string CaminhoJson = "Data/proficiencias.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        var proficiencias = await JsonLoaderHelper.CarregarAsync<List<Proficiencia>>(CaminhoJson, "proficiencias", options);

        if (proficiencias == null || proficiencias.Count == 0)
        {
            Console.WriteLine("❌ Nenhuma proficiência encontrada no JSON.");
            return;
        }

        foreach (var prof in proficiencias)
        {
            if (await RegistroExisteAsync(connection, transaction, "Proficiencia", prof.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(prof);
            parametros["Id"] = prof.Id;
            parametros["Tipo"] = prof.Tipo.ToString();
            parametros["TemEspecializacao"] = prof.TemEspecializacao ? 1 : 0;
            parametros["BonusAdicional"] = prof.BonusAdicional;

            await InserirEntidadeFilhaAsync(connection, transaction, "Proficiencia", parametros);
        }

        Console.WriteLine("✅ Proficiências populadas.");
    }
}
