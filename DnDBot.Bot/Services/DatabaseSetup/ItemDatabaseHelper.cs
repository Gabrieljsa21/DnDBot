using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;

public static class ItemDatabaseHelper
{
    private const string CaminhoJson = "Data/itens.json";

    public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
    {
        var itens = await JsonLoaderHelper.CarregarAsync<List<Item>>(CaminhoJson, "itens");

        if (itens == null || itens.Count == 0)
        {
            Console.WriteLine("❌ Nenhum item encontrado no JSON.");
            return;
        }

        foreach (var item in itens)
        {
            if (!Enum.IsDefined(typeof(CategoriaItem), item.Categoria))
            {
                Console.WriteLine($"⚠ Item '{item.Nome}' com Categoria inválida: {item.Categoria}");
                continue;
            }

            await InserirItem(connection, transaction, item);
        }

        Console.WriteLine("✅ Itens populados com sucesso.");
    }

    public static async Task InserirItem(SqliteConnection conn, SqliteTransaction tx, Item item)
    {
        if (await RegistroExisteAsync(conn, tx, "Item", item.Id))
            return;

        var parametros = GerarParametrosEntidadeBase(item);
        parametros["Id"] = item.Id;
        parametros["Categoria"] = item.Categoria.ToString();
        parametros["SubCategoria"] = item.SubCategoria.ToString();
        parametros["Raridade"] = item.Raridade.ToString();
        parametros["PesoUnitario"] = item.PesoUnitario;
        parametros["Empilhavel"] = item.Empilhavel ? 1 : 0;
        parametros["ValorCobre"] = item.ValorCobre;
        parametros["Equipavel"] = item.Equipavel ? 1 : 0;

        await InserirEntidadeFilhaAsync(conn, tx, "Item", parametros);
    }
}
