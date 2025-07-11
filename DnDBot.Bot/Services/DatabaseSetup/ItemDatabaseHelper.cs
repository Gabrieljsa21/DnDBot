using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // Tabela base de itens
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
            " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

            // Tabela específica para ferramentas
            ["Ferramenta"] = @"
            Id TEXT PRIMARY KEY,
            RequerProficiencia INTEGER NOT NULL DEFAULT 0,
            FOREIGN KEY (Id) REFERENCES Item(Id) ON DELETE CASCADE",

            // Tabela para tags de ferramenta (muitos-para-muitos)
            ["FerramentaTag"] = @"
            FerramentaId TEXT NOT NULL,
            Tag TEXT NOT NULL,
            PRIMARY KEY (FerramentaId, Tag),
            FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE",

            // Tabela para perícias associadas a ferramenta (muitos-para-muitos)
            ["FerramentaPericia"] = @"
            FerramentaId TEXT NOT NULL,
            PericiaId TEXT NOT NULL,
            PRIMARY KEY (FerramentaId, PericiaId),
            FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE"
        };

        foreach (var tabela in definicoes)
            await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
    }


    public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
    {
        if (!File.Exists(CaminhoJson))
        {
            Console.WriteLine("❌ Arquivo itens.json não encontrado.");
            return;
        }

        Console.WriteLine("📥 Lendo itens do JSON...");

        var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        var itens = JsonSerializer.Deserialize<List<Item>>(json, options);

        if (itens == null || itens.Count == 0)
        {
            Console.WriteLine("❌ Nenhum item encontrado.");
            return;
        }

        foreach (var item in itens)
        {
            if (!Enum.IsDefined(typeof(CategoriaItem), item.Categoria))
            {
                Console.WriteLine($"⚠ Item '{item.Nome}' tem Categoria inválida: '{item.Categoria}'");
                continue;
            }

            if (await RegistroExisteAsync(conn, tx, "Item", item.Id))
                continue;

            var parametrosItem = GerarParametrosEntidadeBase(item);
            parametrosItem["categoria"] = item.Categoria.ToString();
            parametrosItem["subcategoria"] = item.SubCategoria.ToString();
            parametrosItem["raridade"] = item.Raridade.ToString();
            parametrosItem["pesoUnitario"] = item.PesoUnitario;
            parametrosItem["empilhavel"] = item.Empilhavel ? 1 : 0;
            parametrosItem["valorCobre"] = item.ValorCobre;
            parametrosItem["equipavel"] = item.Equipavel ? 1 : 0;
            parametrosItem["discriminator"] = item.Categoria == CategoriaItem.Ferramenta ? "Ferramenta" : "Item";

            var sqlItem = $@"
            INSERT INTO Item (
                Id, Categoria, SubCategoria, Raridade, PesoUnitario, Empilhavel, ValorCobre, Equipavel, Discriminator,
                {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
            )
            VALUES (
                $id, $categoria, $subcategoria, $raridade, $pesoUnitario, $empilhavel, $valorCobre, $equipavel, $discriminator,
                {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
            );";

            var cmdItem = SqliteHelper.CriarInsertCommand(conn, tx, sqlItem, parametrosItem);
            await cmdItem.ExecuteNonQueryAsync();

            // Se for Ferramenta, desserialize para acessar campos extras
            if (item.Categoria == CategoriaItem.Ferramenta)
            {
                // Desserializa o item como Ferramenta (reaproveita JSON original para evitar problemas)
                var ferramenta = JsonSerializer.Deserialize<Ferramenta>(
                    JsonSerializer.Serialize(item, options), options);

                if (ferramenta == null)
                    continue;

                var parametrosFerramenta = new Dictionary<string, object>
                {
                    ["id"] = ferramenta.Id,
                    ["requerProficiencia"] = ferramenta.RequerProficiencia ? 1 : 0
                };

                var sqlFerramenta = "INSERT INTO Ferramenta (Id, RequerProficiencia) VALUES ($id, $requerProficiencia);";
                var cmdFerramenta = SqliteHelper.CriarInsertCommand(conn, tx, sqlFerramenta, parametrosFerramenta);
                await cmdFerramenta.ExecuteNonQueryAsync();

                // Tags
                if (ferramenta.Tags != null)
                {
                    foreach (var tag in ferramenta.Tags)
                    {
                        var parametrosTag = new Dictionary<string, object>
                        {
                            ["ferramentaId"] = ferramenta.Id,
                            ["tag"] = tag
                        };

                        var sqlTag = "INSERT INTO FerramentaTag (FerramentaId, Tag) VALUES ($ferramentaId, $tag);";
                        var cmdTag = SqliteHelper.CriarInsertCommand(conn, tx, sqlTag, parametrosTag);
                        await cmdTag.ExecuteNonQueryAsync();
                    }
                }

                // Perícias
                if (ferramenta.PericiasAssociadas != null)
                {
                    foreach (var pericia in ferramenta.PericiasAssociadas)
                    {
                        var parametrosPericia = new Dictionary<string, object>
                        {
                            ["ferramentaId"] = ferramenta.Id,
                            ["periciaId"] = pericia.PericiaId
                        };

                        var sqlPericia = "INSERT INTO FerramentaPericia (FerramentaId, PericiaId) VALUES ($ferramentaId, $periciaId);";
                        var cmdPericia = SqliteHelper.CriarInsertCommand(conn, tx, sqlPericia, parametrosPericia);
                        await cmdPericia.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        Console.WriteLine("✅ Itens e ferramentas populados.");
    }


}
