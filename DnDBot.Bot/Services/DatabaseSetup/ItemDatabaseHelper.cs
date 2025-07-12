using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class ItemDatabaseHelper
{
    private const string CaminhoJson = "Data/itens.json";

    public static async Task CriarTabelaAsync(SqliteCommand cmd)
    {
        var definicoes = new Dictionary<string, string>
        {
            ["Item"] = @"
                Id TEXT PRIMARY KEY,
                Categoria TEXT NOT NULL,
                SubCategoria TEXT NOT NULL,
                Raridade TEXT NOT NULL,
                PesoUnitario REAL NOT NULL,
                Empilhavel INTEGER NOT NULL,
                ValorCobre INTEGER NOT NULL,
                Equipavel INTEGER NOT NULL,
                Discriminator TEXT NOT NULL,
                " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim()
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo itens.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo itens do JSON...");

        var textoJson = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        var listaItens = JsonSerializer.Deserialize<List<Item>>(textoJson, options);
        if (listaItens == null || listaItens.Count == 0)
        {
            Console.WriteLine("❌ Nenhum item encontrado.");
            return;
        }

        foreach (var item in listaItens)
        {
            // valida categoria
            if (!Enum.IsDefined(typeof(CategoriaItem), item.Categoria))
            {
                Console.WriteLine($"⚠ Item '{item.Nome}' tem Categoria inválida: '{item.Categoria}'");
                continue;
            }

            // insere na tabela base
            await InserirItem(connection, transaction, item, item.Categoria.ToString());
        }

        Console.WriteLine("✅ Itens populados.");
    }

    /// <summary>
    /// Insere um registro na tabela Item (somente colunas comuns).
    /// </summary>
    public static async Task InserirItem(
        SqliteConnection connection,
        SqliteTransaction transaction,
        Item item,
        string discriminator)
    {
        if (await RegistroExisteAsync(connection, transaction, "Item", item.Id))
            return;

        var parametros = GerarParametrosEntidadeBase(item);
        parametros["categoria"] = item.Categoria.ToString();
        parametros["subcategoria"] = item.SubCategoria.ToString();
        parametros["raridade"] = item.Raridade.ToString();
        parametros["pesoUnitario"] = item.PesoUnitario;
        parametros["empilhavel"] = item.Empilhavel ? 1 : 0;
        parametros["valorCobre"] = item.ValorCobre;
        parametros["equipavel"] = item.Equipavel ? 1 : 0;
        parametros["discriminator"] = discriminator;

        var sql = $@"
            INSERT INTO Item (
                Id, Categoria, SubCategoria, Raridade,
                PesoUnitario, Empilhavel, ValorCobre, Equipavel, Discriminator,
                {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
            ) VALUES (
                $id, $categoria, $subcategoria, $raridade,
                $pesoUnitario, $empilhavel, $valorCobre, $equipavel, $discriminator,
                {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
            );";

        await CriarInsertCommand(connection, transaction, sql, parametros)
            .ExecuteNonQueryAsync();
    }
}
