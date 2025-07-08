using DnDBot.Application.Models;
using DnDBot.Bot.Data;
using DnDBot.Bot.Helpers;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class AlinhamentoDatabaseHelper
{
    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Alinhamento"] = SqliteEntidadeBaseHelper.Campos,
            ["AlinhamentoTag"] = @"
                AlinhamentoId TEXT NOT NULL,
                Tag TEXT NOT NULL,
                PRIMARY KEY (AlinhamentoId, Tag),
                FOREIGN KEY (AlinhamentoId) REFERENCES Alinhamento(Id) ON DELETE CASCADE"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (AlinhamentosData.Alinhamentos is not { Count: > 0 })
        {
            Console.WriteLine("❌ Nenhum alinhamento encontrado nos dados.");
            return;
        }

        foreach (var alinhamento in AlinhamentosData.Alinhamentos)
        {
            if (await SqliteHelper.RegistroExisteAsync(connection, transaction, "Alinhamento", alinhamento.Id))
                continue;

            var parametros = SqliteHelper.GerarParametrosEntidadeBase(alinhamento);

            var sql = $@"
                INSERT INTO Alinhamento (
                    {SqliteEntidadeBaseHelper.CamposInsert}
                ) VALUES (
                    {SqliteEntidadeBaseHelper.ValoresInsert}
                )";

            var cmd = SqliteHelper.CriarInsertCommand(connection, transaction, sql, parametros);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inserindo alinhamento {alinhamento.Id}: {ex.Message}");
                throw;
            }


            await SqliteHelper.InserirTagsAsync(connection, transaction, "AlinhamentoTag", "AlinhamentoId", alinhamento.Id, alinhamento.Tags);
        }

        Console.WriteLine("✅ Alinhamentos populados com sucesso.");
    }
}
