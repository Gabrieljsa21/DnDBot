using DnDBot.Application.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDBot.Application.Services.DatabaseSetup
{
    public static class ArmaduraDatabaseHelper
    {
        private const string CaminhoJsonArmaduras = "Data/armaduras.json";

        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Armadura (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Tipo INTEGER NOT NULL,
                    ClasseArmadura INTEGER NOT NULL,
                    PermiteFurtividade INTEGER NOT NULL,
                    PenalidadeFurtividade INTEGER NOT NULL,
                    Peso REAL NOT NULL,
                    Custo REAL NOT NULL,
                    RequisitoForca INTEGER NOT NULL,
                    DurabilidadeAtual INTEGER NOT NULL,
                    DurabilidadeMaxima INTEGER NOT NULL,
                    EMagica INTEGER NOT NULL,
                    BonusMagico INTEGER NOT NULL,
                    Raridade TEXT,
                    Fabricante TEXT,
                    Material TEXT,
                    DescricaoDetalhada TEXT,
                    Icone TEXT
                );

                CREATE TABLE IF NOT EXISTS Armadura_Propriedades (
                    ArmaduraId TEXT NOT NULL,
                    Propriedade TEXT NOT NULL,
                    PRIMARY KEY (ArmaduraId, Propriedade),
                    FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Armadura_Resistencias (
                    ArmaduraId TEXT NOT NULL,
                    TipoDano INTEGER NOT NULL,
                    PRIMARY KEY (ArmaduraId, TipoDano),
                    FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Armadura_Imunidades (
                    ArmaduraId TEXT NOT NULL,
                    TipoDano INTEGER NOT NULL,
                    PRIMARY KEY (ArmaduraId, TipoDano),
                    FOREIGN KEY (ArmaduraId) REFERENCES Armadura(Id) ON DELETE CASCADE
                );
            ";

            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonArmaduras))
            {
                Console.WriteLine("❌ Arquivo armaduras.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de armaduras.json...");

            var json = await File.ReadAllTextAsync(CaminhoJsonArmaduras, Encoding.UTF8);
            var armaduras = JsonSerializer.Deserialize<List<Armadura>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (armaduras == null) return;

            foreach (var armadura in armaduras)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Armadura WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", armadura.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Armadura (
                            Id, Nome, Tipo, ClasseArmadura, PermiteFurtividade,
                            PenalidadeFurtividade, Peso, Custo, RequisitoForca,
                            DurabilidadeAtual, DurabilidadeMaxima, EMagica, BonusMagico,
                            Raridade, Fabricante, Material, DescricaoDetalhada, Icone)
                        VALUES (
                            $id, $nome, $tipo, $classeArmadura, $permiteFurtividade,
                            $penalidadeFurtividade, $peso, $custo, $requisitoForca,
                            $durabilidadeAtual, $durabilidadeMaxima, $emagica, $bonusMagico,
                            $raridade, $fabricante, $material, $descricaoDetalhada, $icone
                        )";
                    insertCmd.Parameters.AddWithValue("$id", armadura.Id);
                    insertCmd.Parameters.AddWithValue("$nome", armadura.Nome);
                    insertCmd.Parameters.AddWithValue("$tipo", (int)armadura.Tipo);
                    insertCmd.Parameters.AddWithValue("$classeArmadura", armadura.ClasseArmadura);
                    insertCmd.Parameters.AddWithValue("$permiteFurtividade", armadura.PermiteFurtividade ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$penalidadeFurtividade", armadura.PenalidadeFurtividade);
                    insertCmd.Parameters.AddWithValue("$peso", armadura.Peso);
                    insertCmd.Parameters.AddWithValue("$custo", armadura.Custo);
                    insertCmd.Parameters.AddWithValue("$requisitoForca", armadura.RequisitoForca);
                    insertCmd.Parameters.AddWithValue("$durabilidadeAtual", armadura.DurabilidadeAtual);
                    insertCmd.Parameters.AddWithValue("$durabilidadeMaxima", armadura.DurabilidadeMaxima);
                    insertCmd.Parameters.AddWithValue("$emagica", armadura.EMagica ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$bonusMagico", armadura.BonusMagico);
                    insertCmd.Parameters.AddWithValue("$raridade", armadura.Raridade ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$fabricante", armadura.Fabricante ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$material", armadura.Material ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$descricaoDetalhada", armadura.DescricaoDetalhada ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$icone", armadura.Icone ?? string.Empty);

                    await insertCmd.ExecuteNonQueryAsync();
                }

                // Propriedades especiais
                if (armadura.PropriedadesEspeciais != null)
                {
                    foreach (var prop in armadura.PropriedadesEspeciais)
                    {
                        var propExistsCmd = connection.CreateCommand();
                        propExistsCmd.Transaction = transaction;
                        propExistsCmd.CommandText = @"
                            SELECT COUNT(*) FROM Armadura_Propriedades
                            WHERE ArmaduraId = $armaduraId AND Propriedade = $propriedade";
                        propExistsCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                        propExistsCmd.Parameters.AddWithValue("$propriedade", prop);

                        var propCount = Convert.ToInt32(await propExistsCmd.ExecuteScalarAsync());
                        if (propCount == 0)
                        {
                            var insertPropCmd = connection.CreateCommand();
                            insertPropCmd.Transaction = transaction;
                            insertPropCmd.CommandText = @"
                                INSERT INTO Armadura_Propriedades (ArmaduraId, Propriedade)
                                VALUES ($armaduraId, $propriedade)";
                            insertPropCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                            insertPropCmd.Parameters.AddWithValue("$propriedade", prop);
                            await insertPropCmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Resistências a dano
                if (armadura.ResistenciasDano != null)
                {
                    foreach (var tipoDano in armadura.ResistenciasDano)
                    {
                        var resExistsCmd = connection.CreateCommand();
                        resExistsCmd.Transaction = transaction;
                        resExistsCmd.CommandText = @"
                            SELECT COUNT(*) FROM Armadura_Resistencias
                            WHERE ArmaduraId = $armaduraId AND TipoDano = $tipoDano";
                        resExistsCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                        resExistsCmd.Parameters.AddWithValue("$tipoDano", (int)tipoDano);

                        var resCount = Convert.ToInt32(await resExistsCmd.ExecuteScalarAsync());
                        if (resCount == 0)
                        {
                            var insertResCmd = connection.CreateCommand();
                            insertResCmd.Transaction = transaction;
                            insertResCmd.CommandText = @"
                                INSERT INTO Armadura_Resistencias (ArmaduraId, TipoDano)
                                VALUES ($armaduraId, $tipoDano)";
                            insertResCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                            insertResCmd.Parameters.AddWithValue("$tipoDano", (int)tipoDano);
                            await insertResCmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Imunidades a dano
                if (armadura.ImunidadesDano != null)
                {
                    foreach (var tipoDano in armadura.ImunidadesDano)
                    {
                        var imnExistsCmd = connection.CreateCommand();
                        imnExistsCmd.Transaction = transaction;
                        imnExistsCmd.CommandText = @"
                            SELECT COUNT(*) FROM Armadura_Imunidades
                            WHERE ArmaduraId = $armaduraId AND TipoDano = $tipoDano";
                        imnExistsCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                        imnExistsCmd.Parameters.AddWithValue("$tipoDano", (int)tipoDano);

                        var imnCount = Convert.ToInt32(await imnExistsCmd.ExecuteScalarAsync());
                        if (imnCount == 0)
                        {
                            var insertImnCmd = connection.CreateCommand();
                            insertImnCmd.Transaction = transaction;
                            insertImnCmd.CommandText = @"
                                INSERT INTO Armadura_Imunidades (ArmaduraId, TipoDano)
                                VALUES ($armaduraId, $tipoDano)";
                            insertImnCmd.Parameters.AddWithValue("$armaduraId", armadura.Id);
                            insertImnCmd.Parameters.AddWithValue("$tipoDano", (int)tipoDano);
                            await insertImnCmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }

            Console.WriteLine("✅ Armaduras populadas.");
        }
    }
}
