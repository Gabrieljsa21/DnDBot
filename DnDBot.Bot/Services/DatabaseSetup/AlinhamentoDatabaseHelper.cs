using DnDBot.Bot.Data;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class AlinhamentoDatabaseHelper
{
    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (AlinhamentosData.Alinhamentos is not { Count: > 0 })
        {
            Console.WriteLine("❌ Nenhum alinhamento encontrado nos dados.");
            return;
        }

        foreach (var alinhamento in AlinhamentosData.Alinhamentos)
        {
            if (await RegistroExisteAsync(connection, transaction, "Alinhamento", alinhamento.Id))
                continue;

            var parametros = GerarParametrosEntidadeBase(alinhamento);

            try
            {
                await InserirEntidadeFilhaAsync(connection, transaction, "Alinhamento", parametros);

                if (alinhamento.Tags?.Count > 0)
                    await InserirTagsAsync(connection, transaction, "AlinhamentoTag", "AlinhamentoId", alinhamento.Id, alinhamento.Tags);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao inserir alinhamento '{alinhamento.Id}': {ex.Message}");
            }
        }

        Console.WriteLine("✅ Alinhamentos populados com sucesso.");
    }
}
