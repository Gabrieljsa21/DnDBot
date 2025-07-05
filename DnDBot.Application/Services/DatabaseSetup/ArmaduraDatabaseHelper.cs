using DnDBot.Application.Helpers;
using DnDBot.Application.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static DnDBot.Application.Helpers.SqliteHelper;

namespace DnDBot.Application.Services.DatabaseSetup
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
            "Tipo TEXT NOT NULL",                     // Enum como string
            "ClasseArmadura INTEGER NOT NULL",
            "PermiteFurtividade INTEGER NOT NULL",    // Bool como int (0/1)
            "PenalidadeFurtividade INTEGER",
            "Peso REAL",
            "Custo REAL",
            "RequisitoForca INTEGER",
            "DurabilidadeAtual INTEGER",
            "DurabilidadeMaxima INTEGER",
            "EMagica INTEGER NOT NULL",               // Bool como int
            "BonusMagico INTEGER",
            "Raridade TEXT",
            "Fabricante TEXT",
            "Material TEXT",
            SqliteEntidadeBaseHelper.Campos
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
                await SqliteHelper.InserirTagsAsync(connection, transaction, "ArmaduraTag", "ArmaduraId", armadura.Id, armadura.Tags);
                await InserirPropriedades(connection, transaction, armadura.Id, armadura.PropriedadesEspeciais);
                await InserirTiposDano(connection, transaction, "ArmaduraResistenciaDano", armadura.Id, armadura.ResistenciasDano.Select(x => x.ToString()).ToList());
                await InserirTiposDano(connection, transaction, "ArmaduraImunidadeDano", armadura.Id, armadura.ImunidadesDano.Select(x => x.ToString()).ToList());
            }

            Console.WriteLine("✅ Armaduras populadas.");
        }

        private static async Task InserirArmadura(SqliteConnection conn, SqliteTransaction tx, Armadura armadura)
        {
            if (await SqliteHelper.RegistroExisteAsync(conn, tx, "Armadura", armadura.Id))
                return;

            var parametros = SqliteHelper.GerarParametrosEntidadeBase(armadura);
            parametros["tipo"] = armadura.Tipo.ToString();
            parametros["ca"] = armadura.ClasseArmadura;
            parametros["furtivo"] = armadura.PermiteFurtividade ? 1 : 0;
            parametros["penalidade"] = armadura.PenalidadeFurtividade;
            parametros["peso"] = armadura.Peso;
            parametros["custo"] = armadura.Custo;
            parametros["forca"] = armadura.RequisitoForca;
            parametros["dAtual"] = armadura.DurabilidadeAtual;
            parametros["dMax"] = armadura.DurabilidadeMaxima;
            parametros["magica"] = armadura.EMagica ? 1 : 0;
            parametros["bonus"] = armadura.BonusMagico;
            parametros["raridade"] = armadura.Raridade ?? "";
            parametros["fabricante"] = armadura.Fabricante ?? "";
            parametros["material"] = armadura.Material ?? "";

            var sql = $@"
        INSERT INTO Armadura (
            Tipo, ClasseArmadura, PermiteFurtividade, PenalidadeFurtividade,
            Peso, Custo, RequisitoForca,
            DurabilidadeAtual, DurabilidadeMaxima,
            EMagica, BonusMagico, Raridade, Fabricante, Material,
            {SqliteEntidadeBaseHelper.CamposInsert}
        ) VALUES (
            $tipo, $ca, $furtivo, $penalidade,
            $peso, $custo, $forca,
            $dAtual, $dMax,
            $magica, $bonus, $raridade, $fabricante, $material,
            {SqliteEntidadeBaseHelper.ValoresInsert}
        )";

            var cmd = SqliteHelper.CriarInsertCommand(conn, tx, sql, parametros);
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

