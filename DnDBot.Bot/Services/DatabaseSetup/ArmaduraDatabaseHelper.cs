using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Bot.Helpers.SqliteHelper;
using static System.Net.Mime.MediaTypeNames;

namespace DnDBot.Bot.Services.DatabaseSetup
{
    public static class ArmaduraDatabaseHelper
    {
        private const string CaminhoJsonArmaduras = "Data/armaduras.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            var definicoes = new Dictionary<string, string>
            {
                ["Armadura"] = string.Join(",\n", new[]
                {
                "Id TEXT PRIMARY KEY",
                "Tipo TEXT NOT NULL",                     // Enum como string
                "ClasseArmadura INTEGER NOT NULL",
                "PermiteFurtividade INTEGER NOT NULL",    // Bool como int (0/1)
                "PenalidadeFurtividade INTEGER",
                "Custo REAL",
                "RequisitoForca INTEGER",
                "DurabilidadeAtual INTEGER",
                "DurabilidadeMaxima INTEGER",
                "Fabricante TEXT",
            SqliteEntidadeBaseHelper.Campos.Replace("Id TEXT PRIMARY KEY,", "").Trim().TrimEnd(',')
        }),

                ["ArmaduraTag"] = @"
            ArmaduraId TEXT NOT NULL,
            Tag TEXT NOT NULL,
            PRIMARY KEY (ArmaduraId, Tag),
            FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE",

                ["ArmaduraPropriedadeEspecial"] = @"
            ArmaduraId TEXT NOT NULL,
            Propriedade TEXT NOT NULL,
            PRIMARY KEY (ArmaduraId, Propriedade),
            FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE",

                ["ArmaduraResistenciaDano"] = @"
            ArmaduraId TEXT NOT NULL,
            TipoDano TEXT NOT NULL,
            PRIMARY KEY (ArmaduraId, TipoDano),
            FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE",

                ["ArmaduraImunidadeDano"] = @"
            ArmaduraId TEXT NOT NULL,
            TipoDano TEXT NOT NULL,
            PRIMARY KEY (ArmaduraId, TipoDano),
            FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE"
            };

            foreach (var tabela in definicoes)
                await SqliteHelper.CriarTabelaAsync(cmd, tabela.Key, tabela.Value);
        }


        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            const string CaminhoJson = "Data/armaduras.json";

            if (!File.Exists(CaminhoJson))
            {
                Console.WriteLine("❌ Arquivo armaduras.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de armaduras.json...");

            var json = await File.ReadAllTextAsync(CaminhoJson, Encoding.UTF8);
            var armaduras = JsonSerializer.Deserialize<List<Armadura>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (armaduras == null || armaduras.Count == 0)
            {
                Console.WriteLine("❌ Nenhuma armadura encontrada no JSON.");
                return;
            }

            foreach (var armadura in armaduras)
            {
                await InserirArmadura(connection, transaction, armadura);
                await InserirTagsAsync(connection, transaction, "ArmaduraTag", "ArmaduraId", armadura.Id, armadura.Tags);
                await InserirPropriedades(connection, transaction, armadura.Id, armadura.PropriedadesEspeciais);
                await InserirTiposDano(connection, transaction, "ArmaduraResistenciaDano", armadura.Id, armadura.ResistenciasDano.Select(x => x.ToString()).ToList());
                await InserirTiposDano(connection, transaction, "ArmaduraImunidadeDano", armadura.Id, armadura.ImunidadesDano.Select(x => x.ToString()).ToList());
            }

            Console.WriteLine("✅ Armaduras populadas.");
        }

        private static async Task InserirArmadura(SqliteConnection conn, SqliteTransaction tx, Armadura armadura)
        {
            if (await RegistroExisteAsync(conn, tx, "Armadura", armadura.Id))
                return;

            var parametros = GerarParametrosEntidadeBase(armadura);
            parametros["tipo"] = armadura.Tipo.ToString();
            parametros["ca"] = armadura.ClasseArmadura;
            parametros["furtivo"] = armadura.PermiteFurtividade ? 1 : 0;
            parametros["penalidade"] = armadura.PenalidadeFurtividade;
            parametros["custo"] = armadura.Custo;
            parametros["forca"] = armadura.RequisitoForca;
            parametros["dAtual"] = armadura.DurabilidadeAtual;
            parametros["dMax"] = armadura.DurabilidadeMaxima;
            parametros["fabricante"] = armadura.Fabricante ?? "";

            var sql = $@"
        INSERT INTO Armadura (
            Tipo, ClasseArmadura, PermiteFurtividade, PenalidadeFurtividade,
            Custo, RequisitoForca,
            DurabilidadeAtual, DurabilidadeMaxima,
            {SqliteEntidadeBaseHelper.CamposInsert}
        ) VALUES (
            $tipo, $ca, $furtivo, $penalidade,
            $custo, $forca,
            $dAtual, $dMax,
            $fabricante
            {SqliteEntidadeBaseHelper.ValoresInsert}
        )";

            var cmd = CriarInsertCommand(conn, tx, sql, parametros);
            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task InserirPropriedades(SqliteConnection conn, SqliteTransaction tx, string armaduraId, List<string> propriedades)
        {
            if (propriedades == null) return;

            foreach (var prop in propriedades)
            {
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = @"
            INSERT OR IGNORE INTO ArmaduraPropriedadeEspecial (ArmaduraId, Propriedade)
            VALUES ($id, $prop)";
                cmd.Parameters.AddWithValue("$id", armaduraId);
                cmd.Parameters.AddWithValue("$prop", prop);
                await cmd.ExecuteNonQueryAsync();
            }
        }
        private static async Task InserirTiposDano(SqliteConnection conn, SqliteTransaction tx, string tabela, string armaduraId, List<string> tiposDano)
        {
            if (tiposDano == null) return;
            foreach (var tipo in tiposDano)
            {
                var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = $@"
            INSERT OR IGNORE INTO {tabela} (ArmaduraId, TipoDano)
            VALUES ($id, $tipo)";
                cmd.Parameters.AddWithValue("$id", armaduraId);
                cmd.Parameters.AddWithValue("$tipo", tipo);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}

