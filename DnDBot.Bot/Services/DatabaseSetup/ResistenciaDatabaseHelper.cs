using DnDBot.Bot.Data;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class ResistenciaDatabaseHelper
{

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (ResistenciasData.Resistencias is not { Count: > 0 })
        {
            Console.WriteLine("❌ Nenhuma resistência encontrada nos dados.");
            return;
        }

        foreach (var resistencia in ResistenciasData.Resistencias)
        {
            if (await SqliteHelper.RegistroExisteAsync(connection, transaction, "Resistencia", resistencia.Id))
                continue;

            var parametros = SqliteHelper.GerarParametrosEntidadeBase(resistencia);
            parametros["TipoDano"] = (int)resistencia.TipoDano;

            try
            {
                await SqliteHelper.InserirEntidadeFilhaAsync(connection, transaction, "Resistencia", parametros);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao inserir resistência '{resistencia.Id}': {ex.Message}");
            }
        }

        Console.WriteLine("✅ Resistências populadas com sucesso.");
    }

}
