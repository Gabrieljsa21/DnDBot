using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DnDBot.Application.Models;

namespace DnDBot.Application.Services.DatabaseSetup
{
    /// <summary>
    /// Helper estático para criação e povoamento das tabelas relacionadas a armas no banco SQLite.
    /// </summary>
    public static class ArmaDatabaseHelper
    {
        private const string CaminhoJsonArmas = "Data/armas.json";

        /// <summary>
        /// Cria as tabelas necessárias para armazenar armas e suas relações auxiliares, caso não existam.
        /// </summary>
        /// <param name="cmd">Comando SQLite associado a uma conexão aberta.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Arma (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Tipo INTEGER NOT NULL,
                    Categoria INTEGER NOT NULL,
                    DadoDano TEXT,
                    TipoDano INTEGER NOT NULL,
                    TipoDanoSecundario INTEGER,
                    Peso REAL,
                    Custo DECIMAL,
                    Alcance INTEGER,
                    EhDuasMaos INTEGER,
                    EhLeve INTEGER,
                    EhVersatil INTEGER,
                    DadoDanoVersatil TEXT,
                    PodeSerArremessada INTEGER,
                    AlcanceArremesso INTEGER,
                    Descricao TEXT,
                    BonusMagico INTEGER,
                    DurabilidadeAtual INTEGER,
                    DurabilidadeMaxima INTEGER,
                    Icone TEXT,
                    Raridade TEXT,
                    Fabricante TEXT
                );

                CREATE TABLE IF NOT EXISTS Arma_Requisitos (
                    ArmaId TEXT NOT NULL,
                    Requisito TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, Requisito),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Arma_Tags (
                    ArmaId TEXT NOT NULL,
                    Tag TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, Tag),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Arma_PropriedadesEspeciais (
                    ArmaId TEXT NOT NULL,
                    Propriedade TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, Propriedade),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Arma_BonusContraTipos (
                    ArmaId TEXT NOT NULL,
                    TipoContra TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, TipoContra),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Arma_MagiasAssociadas (
                    ArmaId TEXT NOT NULL,
                    MagiaId TEXT NOT NULL,
                    PRIMARY KEY (ArmaId, MagiaId),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );

                CREATE TABLE IF NOT EXISTS Arma_RequisitosAtributos (
                    ArmaId TEXT NOT NULL,
                    Atributo INTEGER NOT NULL,
                    Valor INTEGER NOT NULL,
                    PRIMARY KEY (ArmaId, Atributo),
                    FOREIGN KEY (ArmaId) REFERENCES Arma(Id) ON DELETE CASCADE
                );
            ";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula as tabelas de armas e tabelas auxiliares com os dados lidos do arquivo JSON 'armas.json'.
        /// Insere apenas dados que ainda não existem para evitar duplicações.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para garantir atomicidade.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonArmas))
            {
                Console.WriteLine("❌ Arquivo armas.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de armas.json...");

            var json = await File.ReadAllTextAsync(CaminhoJsonArmas, Encoding.UTF8);
            var armas = JsonSerializer.Deserialize<List<Arma>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (armas == null)
            {
                Console.WriteLine("❌ Nenhuma arma encontrada no JSON.");
                return;
            }

            foreach (var arma in armas)
            {
                // Inserir arma principal
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Arma WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", arma.Id);
                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Arma (
                            Id, Nome, Tipo, Categoria, DadoDano, TipoDano, TipoDanoSecundario, Peso, Custo,
                            Alcance, EhDuasMaos, EhLeve, EhVersatil, DadoDanoVersatil, PodeSerArremessada, AlcanceArremesso,
                            Descricao, BonusMagico, DurabilidadeAtual, DurabilidadeMaxima, Icone, Raridade, Fabricante)
                        VALUES (
                            $id, $nome, $tipo, $categoria, $dadoDano, $tipoDano, $tipoDanoSecundario, $peso, $custo,
                            $alcance, $ehDuasMaos, $ehLeve, $ehVersatil, $dadoDanoVersatil, $podeSerArremessada, $alcanceArremesso,
                            $descricao, $bonusMagico, $durabilidadeAtual, $durabilidadeMaxima, $icone, $raridade, $fabricante)";
                    insertCmd.Parameters.AddWithValue("$id", arma.Id);
                    insertCmd.Parameters.AddWithValue("$nome", arma.Nome);
                    insertCmd.Parameters.AddWithValue("$tipo", (int)arma.Tipo);
                    insertCmd.Parameters.AddWithValue("$categoria", (int)arma.Categoria);
                    insertCmd.Parameters.AddWithValue("$dadoDano", arma.DadoDano ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$tipoDano", (int)arma.TipoDano);
                    insertCmd.Parameters.AddWithValue("$tipoDanoSecundario", arma.TipoDanoSecundario.HasValue ? (object)(int)arma.TipoDanoSecundario.Value : DBNull.Value);
                    insertCmd.Parameters.AddWithValue("$peso", arma.Peso);
                    insertCmd.Parameters.AddWithValue("$custo", arma.Custo);
                    insertCmd.Parameters.AddWithValue("$alcance", arma.Alcance.HasValue ? (object)arma.Alcance.Value : DBNull.Value);
                    insertCmd.Parameters.AddWithValue("$ehDuasMaos", arma.EhDuasMaos ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$ehLeve", arma.EhLeve ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$ehVersatil", arma.EhVersatil ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$dadoDanoVersatil", arma.DadoDanoVersatil ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$podeSerArremessada", arma.PodeSerArremessada ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$alcanceArremesso", arma.AlcanceArremesso.HasValue ? (object)arma.AlcanceArremesso.Value : DBNull.Value);
                    insertCmd.Parameters.AddWithValue("$descricao", arma.Descricao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$bonusMagico", arma.BonusMagico);
                    insertCmd.Parameters.AddWithValue("$durabilidadeAtual", arma.DurabilidadeAtual);
                    insertCmd.Parameters.AddWithValue("$durabilidadeMaxima", arma.DurabilidadeMaxima);
                    insertCmd.Parameters.AddWithValue("$icone", arma.Icone ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$raridade", arma.Raridade ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$fabricante", arma.Fabricante ?? string.Empty);
                    await insertCmd.ExecuteNonQueryAsync();
                }

                // Popular tabelas auxiliares (Requisitos, Tags, PropriedadesEspeciais, BonusContraTipos, MagiasAssociadas, RequisitosAtributos)
                // Insere somente registros que não existam, usando INSERT OR IGNORE.

                if (arma.Requisitos != null)
                {
                    foreach (var req in arma.Requisitos)
                    {
                        var cmdReq = connection.CreateCommand();
                        cmdReq.Transaction = transaction;
                        cmdReq.CommandText = @"
                            INSERT OR IGNORE INTO Arma_Requisitos (ArmaId, Requisito) VALUES ($id, $req)";
                        cmdReq.Parameters.AddWithValue("$id", arma.Id);
                        cmdReq.Parameters.AddWithValue("$req", req);
                        await cmdReq.ExecuteNonQueryAsync();
                    }
                }

                if (arma.Tags != null)
                {
                    foreach (var tag in arma.Tags)
                    {
                        var cmdTag = connection.CreateCommand();
                        cmdTag.Transaction = transaction;
                        cmdTag.CommandText = @"
                            INSERT OR IGNORE INTO Arma_Tags (ArmaId, Tag) VALUES ($id, $tag)";
                        cmdTag.Parameters.AddWithValue("$id", arma.Id);
                        cmdTag.Parameters.AddWithValue("$tag", tag);
                        await cmdTag.ExecuteNonQueryAsync();
                    }
                }

                if (arma.PropriedadesEspeciais != null)
                {
                    foreach (var prop in arma.PropriedadesEspeciais)
                    {
                        var cmdProp = connection.CreateCommand();
                        cmdProp.Transaction = transaction;
                        cmdProp.CommandText = @"
                            INSERT OR IGNORE INTO Arma_PropriedadesEspeciais (ArmaId, Propriedade) VALUES ($id, $prop)";
                        cmdProp.Parameters.AddWithValue("$id", arma.Id);
                        cmdProp.Parameters.AddWithValue("$prop", prop);
                        await cmdProp.ExecuteNonQueryAsync();
                    }
                }

                if (arma.BonusContraTipos != null)
                {
                    foreach (var bct in arma.BonusContraTipos)
                    {
                        var cmdBct = connection.CreateCommand();
                        cmdBct.Transaction = transaction;
                        cmdBct.CommandText = @"
                            INSERT OR IGNORE INTO Arma_BonusContraTipos (ArmaId, TipoContra) VALUES ($id, $tipo)";
                        cmdBct.Parameters.AddWithValue("$id", arma.Id);
                        cmdBct.Parameters.AddWithValue("$tipo", bct);
                        await cmdBct.ExecuteNonQueryAsync();
                    }
                }

                if (arma.MagiasAssociadas != null)
                {
                    foreach (var magia in arma.MagiasAssociadas)
                    {
                        var cmdMagia = connection.CreateCommand();
                        cmdMagia.Transaction = transaction;
                        cmdMagia.CommandText = @"
                            INSERT OR IGNORE INTO Arma_MagiasAssociadas (ArmaId, MagiaId) VALUES ($id, $magia)";
                        cmdMagia.Parameters.AddWithValue("$id", arma.Id);
                        cmdMagia.Parameters.AddWithValue("$magia", magia);
                        await cmdMagia.ExecuteNonQueryAsync();
                    }
                }

                if (arma.RequisitosAtributos != null)
                {
                    foreach (var reqAtributo in arma.RequisitosAtributos)
                    {
                        var cmdReqAtributo = connection.CreateCommand();
                        cmdReqAtributo.Transaction = transaction;
                        cmdReqAtributo.CommandText = @"
                            INSERT OR IGNORE INTO Arma_RequisitosAtributos (ArmaId, Atributo, Valor) VALUES ($id, $atributo, $valor)";
                        cmdReqAtributo.Parameters.AddWithValue("$id", arma.Id);
                        cmdReqAtributo.Parameters.AddWithValue("$atributo", (int)reqAtributo.Atributo);
                        cmdReqAtributo.Parameters.AddWithValue("$valor", reqAtributo.Valor);

                        await cmdReqAtributo.ExecuteNonQueryAsync();
                    }
                }
            }

            Console.WriteLine("✅ Armas e dados relacionados populados.");
        }
    }
}
