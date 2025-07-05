using DnDBot.Application.Helpers;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;

namespace DnDBot.Application.Services.DatabaseSetup
{
    public static class ClasseDatabaseHelper
    {
        private const string CaminhoJson = "Data/classes.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["Classe"] = @"
                    Id TEXT PRIMARY KEY,
                    DadoVida TEXT,
                    PapelTatico TEXT,
                    IdHabilidadeConjuracao TEXT,
                    UsaMagiaPreparada INTEGER,
                    " + SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim(),

                ["ClasseTag"] = @"
                    ClasseId TEXT NOT NULL,
                    Tag TEXT NOT NULL,
                    PRIMARY KEY (ClasseId, Tag),
                    FOREIGN KEY (ClasseId) REFERENCES Classe(Id) ON DELETE CASCADE"
            };

            foreach (var tabela in definicoes)
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJson))
            {
                Console.WriteLine("❌ Arquivo classes.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de classes.json...");

            var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
            var classes = JsonSerializer.Deserialize<List<Classe>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (classes == null) return;

            foreach (var classe in classes)
            {
                if (await RegistroExisteAsync(connection, transaction, "Classe", classe.Id))
                    continue;

                var parametros = GerarParametrosEntidadeBase(classe);
                parametros["dadoVida"] = classe.DadoVida ?? "";
                parametros["papelTatico"] = classe.PapelTatico ?? "";
                parametros["idHabilidadeConjuracao"] = classe.IdHabilidadeConjuracao ?? "";
                parametros["usaMagiaPreparada"] = classe.UsaMagiaPreparada ? 1 : 0;

                var sql = $@"
                    INSERT INTO Classe (
                        Id, DadoVida, PapelTatico, IdHabilidadeConjuracao, UsaMagiaPreparada,
                        {SqliteEntidadeBaseHelper.CamposInsert.Replace("Id,", "").Trim()}
                    ) VALUES (
                        $id, $dadoVida, $papelTatico, $idHabilidadeConjuracao, $usaMagiaPreparada,
                        {SqliteEntidadeBaseHelper.ValoresInsert.Replace("$id,", "").Trim()}
                    )";

                var cmd = CriarInsertCommand(connection, transaction, sql, parametros);
                await cmd.ExecuteNonQueryAsync();

                await InserirTagsAsync(connection, transaction, "ClasseTag", "ClasseId", classe.Id, classe.Tags);
            }

            Console.WriteLine("✅ Classes populadas com sucesso.");
        }
    }
}
