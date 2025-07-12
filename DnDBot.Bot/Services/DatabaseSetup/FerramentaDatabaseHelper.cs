using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha.Auxiliares;
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

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class FerramentaDatabaseHelper
    {
        private const string CaminhoJson = "Data/ferramentas.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["Ferramenta"] = @"
                    Id TEXT PRIMARY KEY,
                    RequerProficiencia INTEGER NOT NULL DEFAULT 0,
                    FOREIGN KEY (Id) REFERENCES Item(Id) ON DELETE CASCADE
                ",
                ["FerramentaTag"] = @"
                    FerramentaId TEXT NOT NULL,
                    Tag TEXT NOT NULL,
                    PRIMARY KEY (FerramentaId, Tag),
                    FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE
                ",
                ["FerramentaPericia"] = @"
                    FerramentaId TEXT NOT NULL,
                    PericiaId TEXT NOT NULL,
                    PRIMARY KEY (FerramentaId, PericiaId),
                    FOREIGN KEY (FerramentaId) REFERENCES Ferramenta(Id) ON DELETE CASCADE
                "
            };

            foreach (var tabela in definicoes)
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJson))
            {
                Console.WriteLine("❌ Arquivo ferramentas.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo ferramentas do JSON...");

            var jsonTexto = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var listaFerramentas = JsonSerializer.Deserialize<List<Ferramenta>>(jsonTexto, options);
            if (listaFerramentas == null || listaFerramentas.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma ferramenta encontrada.");
                return;
            }

            foreach (var ferramenta in listaFerramentas)
            {
                ferramenta.PericiasAssociadas = ferramenta.PericiasIds
                    .Select(pid => new FerramentaPericia
                    {
                        FerramentaId = ferramenta.Id,
                        PericiaId = pid
                    })
                    .ToList();

                await ItemDatabaseHelper.InserirItem(connection,transaction,ferramenta,discriminator: "Ferramenta");
                await InserirFerramenta(connection, transaction, ferramenta);
                await InserirFerramentaTag(connection, transaction, ferramenta);
                await InserirFerramentaPericia(connection, transaction, ferramenta);
            }

            Console.WriteLine("✅ Ferramentas populadas.");
        }

        private static async Task InserirFerramenta(SqliteConnection connection, SqliteTransaction transaction, Ferramenta ferramenta)
        {
            var parametros = new Dictionary<string, object>
            {
                ["id"] = ferramenta.Id,
                ["requerProficiencia"] = ferramenta.RequerProficiencia ? 1 : 0
            };

            const string sql = @"
                INSERT OR IGNORE INTO Ferramenta
                    (Id, RequerProficiencia)
                VALUES
                    ($id, $requerProficiencia);";

            await CriarInsertCommand(connection, transaction, sql, parametros)
                .ExecuteNonQueryAsync();
        }

        private static async Task InserirFerramentaTag(SqliteConnection connection, SqliteTransaction transaction, Ferramenta ferramenta)
        {
            if (ferramenta.Tags == null) return;

            const string sqlTag = @"
                INSERT OR IGNORE INTO FerramentaTag
                    (FerramentaId, Tag)
                VALUES
                    ($ferramentaId, $tag);";

            foreach (var tag in ferramenta.Tags)
            {
                var parametrosTag = new Dictionary<string, object>
                {
                    ["ferramentaId"] = ferramenta.Id,
                    ["tag"] = tag
                };
                await CriarInsertCommand(connection, transaction, sqlTag, parametrosTag)
                    .ExecuteNonQueryAsync();
            }
        }

        private static async Task InserirFerramentaPericia(SqliteConnection connection, SqliteTransaction transaction, Ferramenta ferramenta)
        {
            if (ferramenta.PericiasAssociadas == null) return;

            const string sqlPericia = @"
                INSERT OR IGNORE INTO FerramentaPericia
                    (FerramentaId, PericiaId)
                VALUES
                    ($ferramentaId, $periciaId);";

            foreach (var pericia in ferramenta.PericiasAssociadas)
            {
                var parametrosPericia = new Dictionary<string, object>
                {
                    ["ferramentaId"] = ferramenta.Id,
                    ["periciaId"] = pericia.PericiaId
                };
                await CriarInsertCommand(connection, transaction, sqlPericia, parametrosPericia)
                    .ExecuteNonQueryAsync();
            }
        }
    }
}
