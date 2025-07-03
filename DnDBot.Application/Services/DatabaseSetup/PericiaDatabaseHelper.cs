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
    /// <summary>
    /// Helper estático para criar e popular a tabela Pericia no banco de dados SQLite.
    /// </summary>
    public static class PericiaDatabaseHelper
    {
        /// <summary>
        /// Caminho do arquivo JSON contendo os dados das perícias.
        /// </summary>
        private const string CaminhoJsonPericias = "Data/pericias.json";

        /// <summary>
        /// Cria a tabela Pericia no banco SQLite, caso ainda não exista.
        /// </summary>
        /// <param name="cmd">Comando SQLite para execução do SQL.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Pericia (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    AtributoBase INTEGER NOT NULL,
                    Tipo INTEGER NOT NULL,
                    Descricao TEXT,
                    EhProficiente INTEGER NOT NULL,
                    TemEspecializacao INTEGER NOT NULL,
                    BonusBase INTEGER NOT NULL,
                    BonusAdicional INTEGER NOT NULL,
                    Icone TEXT
                );";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula a tabela Pericia com dados lidos do arquivo JSON.
        /// Verifica se cada perícia já existe para evitar duplicação.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta para o banco.</param>
        /// <param name="transaction">Transação SQLite para operações atômicas.</param>
        /// <returns>Tarefa assíncrona representando a operação de inserção.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonPericias))
            {
                Console.WriteLine("❌ Arquivo pericias.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de pericias.json...");

            var json = await File.ReadAllTextAsync(CaminhoJsonPericias, Encoding.UTF8);
            var pericias = JsonSerializer.Deserialize<List<Pericia>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (pericias == null)
            {
                Console.WriteLine("❌ Falha ao desserializar pericias.json.");
                return;
            }

            foreach (var pericia in pericias)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Pericia WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", pericia.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Pericia (
                            Id, Nome, AtributoBase, Tipo, Descricao,
                            EhProficiente, TemEspecializacao, BonusBase,
                            BonusAdicional,  Icone)
                        VALUES (
                            $id, $nome, $atributoBase, $tipo, $descricao,
                            $ehProficiente, $temEspecializacao, $bonusBase,
                            $bonusAdicional, $icone);";

                    insertCmd.Parameters.AddWithValue("$id", pericia.Id);
                    insertCmd.Parameters.AddWithValue("$nome", pericia.Nome);
                    insertCmd.Parameters.AddWithValue("$atributoBase", (int)pericia.AtributoBase);
                    insertCmd.Parameters.AddWithValue("$tipo", (int)pericia.Tipo);
                    insertCmd.Parameters.AddWithValue("$descricao", pericia.Descricao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$ehProficiente", pericia.EhProficiente ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$temEspecializacao", pericia.TemEspecializacao ? 1 : 0);
                    insertCmd.Parameters.AddWithValue("$bonusBase", pericia.BonusBase);
                    insertCmd.Parameters.AddWithValue("$bonusAdicional", pericia.BonusAdicional);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine("✅ Perícias populadas.");
        }
    }
}
