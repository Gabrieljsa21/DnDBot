using DnDBot.Bot.Helpers;
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

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class EscudoDatabaseHelper
    {
        private const string CaminhoJson = "Data/escudos.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["Escudo"] = string.Join(",\n", new[]
                {
                    "Id TEXT PRIMARY KEY",
                    "BonusCA INTEGER NOT NULL DEFAULT 2",
                    "DurabilidadeAtual INTEGER",
                    "DurabilidadeMaxima INTEGER",
                    "Fabricante TEXT",
                    SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim().TrimEnd(',')
                }),

                ["EscudoPropriedadeEspecial"] = @"
                    EscudoId TEXT NOT NULL,
                    Propriedade TEXT NOT NULL,
                    PRIMARY KEY (EscudoId, Propriedade),
                    FOREIGN KEY (EscudoId) REFERENCES Escudo(Id) ON DELETE CASCADE"
            };

            foreach (var tabela in definicoes)
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }

        public static async Task PopularAsync(SqliteConnection conn, SqliteTransaction tx)
        {
            if (!File.Exists(CaminhoJson))
            {
                Console.WriteLine("❌ Arquivo escudos.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de escudos.json...");

            var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
            var escudos = JsonSerializer.Deserialize<List<Escudo>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            });

            if (escudos == null || escudos.Count == 0)
            {
                Console.WriteLine("❌ Nenhum escudo encontrado no JSON.");
                return;
            }

            foreach (var escudo in escudos)
            {
                if (await RegistroExisteAsync(conn, tx, "Escudo", escudo.Id))
                    continue;

                var parametros = GerarParametrosEntidadeBase(escudo);
                parametros["bonusCA"] = escudo.BonusCA;
                parametros["dAtual"] = escudo.DurabilidadeAtual;
                parametros["dMax"] = escudo.DurabilidadeMaxima;
                parametros["fabricante"] = escudo.Fabricante ?? "";

                var sql = $@"
                    INSERT INTO Escudo (
                        BonusCA, DurabilidadeAtual, DurabilidadeMaxima, Fabricante,
                        {SqliteEntidadeBaseHelper.CamposInsert}
                    )
                    VALUES (
                        $bonusCA, $dAtual, $dMax, $fabricante,
                        {SqliteEntidadeBaseHelper.ValoresInsert}
                    );";

                var cmd = CriarInsertCommand(conn, tx, sql, parametros);
                await cmd.ExecuteNonQueryAsync();

                if (escudo.PropriedadesEspeciais?.Count > 0)
                {
                    foreach (var prop in escudo.PropriedadesEspeciais)
                    {
                        var cmdProp = conn.CreateCommand();
                        cmdProp.Transaction = tx;
                        cmdProp.CommandText = @"
                            INSERT OR IGNORE INTO EscudoPropriedadeEspecial (EscudoId, Propriedade)
                            VALUES ($id, $prop);";
                        cmdProp.Parameters.AddWithValue("$id", escudo.Id);
                        cmdProp.Parameters.AddWithValue("$prop", prop);
                        await cmdProp.ExecuteNonQueryAsync();
                    }
                }
            }

            Console.WriteLine("✅ Escudos populados.");
        }
    }
}
