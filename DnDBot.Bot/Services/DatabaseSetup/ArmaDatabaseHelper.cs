using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.ItensInventario;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class ArmaDatabaseHelper
    {
        private const string CaminhoJsonArmaCorpo = "Data/armascorpoacorpo.json";
        private const string CaminhoJsonArmaDistancia = "Data/armasadistancia.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["Arma"] = @"
                    Id TEXT PRIMARY KEY,
                    Tipo INTEGER NOT NULL,
                    CategoriaArma INTEGER NOT NULL,
                    DadoDano TEXT NOT NULL,
                    TipoDano INTEGER NOT NULL,
                    TipoDanoSecundario INTEGER NULL,
                    EhDuasMaos INTEGER NOT NULL,
                    EhLeve INTEGER NOT NULL,
                    EhVersatil INTEGER NOT NULL,
                    EhAgil INTEGER NOT NULL,
                    EhPesada INTEGER NOT NULL,
                    DadoDanoVersatil TEXT NULL,
                    DurabilidadeAtual INTEGER NOT NULL,
                    DurabilidadeMaxima INTEGER NOT NULL,
                    AlcanceEmMetros INTEGER NOT NULL,
                    FOREIGN KEY (Id) REFERENCES Item(Id) ON DELETE CASCADE
                ",
                ["ArmaTag"] = @"
                    ArmaId TEXT NOT NULL,
                    Tag TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, Tag),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                ",
                ["ArmaRequisitoAtributo"] = @"
                    ArmaId TEXT NOT NULL,
                    AtributoId TEXT NOT NULL,
                    ValorMinimo INTEGER NOT NULL,
                    PRIMARY KEY (ArmaId, AtributoId),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                "
            };

            foreach (var tabela in definicoes)
            {
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
            }
        }

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            await PopularArmasCorpoACorpoAsync(connection, transaction);
            await PopularArmasDistanciaAsync(connection, transaction);
        }

        private static async Task PopularArmasCorpoACorpoAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonArmaCorpo))
            {
                Console.WriteLine($"❌ Arquivo {CaminhoJsonArmaCorpo} não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo armas corpo a corpo do JSON...");

            var jsonTexto = await File.ReadAllTextAsync(CaminhoJsonArmaCorpo, Encoding.UTF8);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var listaArmas = JsonSerializer.Deserialize<List<ArmaCorpoACorpo>>(jsonTexto, options);
            if (listaArmas == null || listaArmas.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma arma corpo a corpo encontrada.");
                return;
            }

            foreach (var arma in listaArmas)
            {
                await ItemDatabaseHelper.InserirItem(connection, transaction, arma, discriminator: "Arma");
                await InserirArma(connection, transaction, arma);
                await InserirArmaTag(connection, transaction, arma);
                await InserirArmaRequisitoAtributo(connection, transaction, arma);
            }

            Console.WriteLine("✅ Armas corpo a corpo populadas.");
        }

        private static async Task PopularArmasDistanciaAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonArmaDistancia))
            {
                Console.WriteLine($"❌ Arquivo {CaminhoJsonArmaDistancia} não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo armas à distância do JSON...");

            var jsonTexto = await File.ReadAllTextAsync(CaminhoJsonArmaDistancia, Encoding.UTF8);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var listaArmas = JsonSerializer.Deserialize<List<ArmaADistancia>>(jsonTexto, options);
            if (listaArmas == null || listaArmas.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma arma à distância encontrada.");
                return;
            }

            foreach (var arma in listaArmas)
            {
                await ItemDatabaseHelper.InserirItem(connection, transaction, arma, discriminator: "Arma");
                await InserirArma(connection, transaction, arma);
                await InserirArmaTag(connection, transaction, arma);
                await InserirArmaRequisitoAtributo(connection, transaction, arma);
            }

            Console.WriteLine("✅ Armas à distância populadas.");
        }

        private static async Task InserirArma(SqliteConnection connection, SqliteTransaction transaction, Arma arma)
        {
            int alcanceEmMetros = 0;

            if (arma is ArmaADistancia armaDist)
            {
                // Usar AlcanceMaximo do JSON para alcance em metros
                alcanceEmMetros = armaDist.AlcanceMaximo;
            }

            var parametros = new Dictionary<string, object>
            {
                ["id"] = arma.Id,
                ["tipo"] = (int)arma.Tipo,
                ["categoriaArma"] = (int)arma.CategoriaArma,
                ["dadoDano"] = arma.DadoDano,
                ["tipoDano"] = (int)arma.TipoDano,
                ["tipoDanoSecundario"] = arma.TipoDanoSecundario.HasValue ? (object)(int)arma.TipoDanoSecundario.Value : DBNull.Value,
                ["ehDuasMaos"] = arma.EhDuasMaos ? 1 : 0,
                ["ehLeve"] = arma.EhLeve ? 1 : 0,
                ["ehVersatil"] = arma.EhVersatil ? 1 : 0,
                ["ehAgil"] = arma.EhAgil ? 1 : 0,
                ["ehPesada"] = arma.EhPesada ? 1 : 0,
                ["dadoDanoVersatil"] = string.IsNullOrEmpty(arma.DadoDanoVersatil) ? DBNull.Value : arma.DadoDanoVersatil,
                ["durabilidadeAtual"] = arma.DurabilidadeAtual,
                ["durabilidadeMaxima"] = arma.DurabilidadeMaxima,
                ["alcanceEmMetros"] = alcanceEmMetros
            };

            const string sql = @"
                INSERT OR IGNORE INTO Arma
                    (Id, Tipo, CategoriaArma, DadoDano, TipoDano, TipoDanoSecundario, EhDuasMaos, EhLeve, EhVersatil, EhAgil, EhPesada, DadoDanoVersatil, DurabilidadeAtual, DurabilidadeMaxima, AlcanceEmMetros)
                VALUES
                    ($id, $tipo, $categoriaArma, $dadoDano, $tipoDano, $tipoDanoSecundario, $ehDuasMaos, $ehLeve, $ehVersatil, $ehAgil, $ehPesada, $dadoDanoVersatil, $durabilidadeAtual, $durabilidadeMaxima, $alcanceEmMetros);";

            await SqliteHelper.CriarInsertCommand(connection, transaction, sql, parametros)
                .ExecuteNonQueryAsync();
        }

        private static async Task InserirArmaTag(SqliteConnection connection, SqliteTransaction transaction, Arma arma)
        {
            if (arma.Tags == null) return;

            const string sqlTag = @"
                INSERT OR IGNORE INTO ArmaTag
                    (ArmaId, Tag)
                VALUES
                    ($armaId, $tag);";

            foreach (var tag in arma.Tags)
            {
                var parametrosTag = new Dictionary<string, object>
                {
                    ["armaId"] = arma.Id,
                    ["tag"] = tag
                };
                await SqliteHelper.CriarInsertCommand(connection, transaction, sqlTag, parametrosTag)
                    .ExecuteNonQueryAsync();
            }
        }

        private static async Task InserirArmaRequisitoAtributo(SqliteConnection connection, SqliteTransaction transaction, Arma arma)
        {
            if (arma.RequisitosAtributos == null) return;

            const string sqlReq = @"
                INSERT OR IGNORE INTO ArmaRequisitoAtributo
                    (ArmaId, AtributoId, ValorMinimo)
                VALUES
                    ($armaId, $atributoId, $valorMinimo);";

            foreach (var requisito in arma.RequisitosAtributos)
            {
                var parametrosReq = new Dictionary<string, object>
                {
                    ["armaId"] = arma.Id,
                    ["atributoId"] = requisito.Atributo,
                    ["valorMinimo"] = requisito.Valor
                };
                await SqliteHelper.CriarInsertCommand(connection, transaction, sqlReq, parametrosReq)
                    .ExecuteNonQueryAsync();
            }
        }
    }
}
