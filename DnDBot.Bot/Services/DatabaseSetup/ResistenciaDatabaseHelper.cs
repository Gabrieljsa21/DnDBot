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
    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Resistencia"] = SqliteEntidadeBaseHelper.Campos + @",
                TipoDano INTEGER NOT NULL,
                Nome TEXT NOT NULL,
                Descricao TEXT"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

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

            // Gera os parâmetros base
            var parametros = SqliteHelper.GerarParametrosEntidadeBase(resistencia);

            // Adiciona o TipoDano que é específico de Resistencia
            parametros["tipoDano"] = (int)resistencia.TipoDano;

            var sql = $@"
            INSERT INTO Resistencia (
                {SqliteEntidadeBaseHelper.CamposInsert},
                TipoDano
            ) VALUES (
                {SqliteEntidadeBaseHelper.ValoresInsert},
                $tipoDano
            )";

            var cmd = SqliteHelper.CriarInsertCommand(connection, transaction, sql, parametros);

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inserindo resistência {resistencia.Id}: {ex.Message}");
                throw;
            }
        }

        Console.WriteLine("✅ Resistências populadas com sucesso.");
    }

}
