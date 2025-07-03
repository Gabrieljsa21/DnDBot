using DnDBot.Application.Models.Ficha;
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
    /// Helper estático para criação e povoamento da tabela Classe no banco SQLite.
    /// </summary>
    public static class ClasseDatabaseHelper
    {
        /// <summary>
        /// Caminho do arquivo JSON com os dados das classes.
        /// </summary>
        private const string CaminhoJson = "Data/classes.json";

        /// <summary>
        /// Cria a tabela Classe caso não exista, com seus campos básicos.
        /// </summary>
        /// <param name="cmd">Comando SQLite associado a uma conexão aberta.</param>
        /// <returns>Tarefa assíncrona representando a criação da tabela.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Classe (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Descricao TEXT,
                    DadoVida TEXT,
                    Fonte TEXT,
                    ImagemUrl TEXT,
                    IconeUrl TEXT,
                    PapelTatico TEXT,
                    IdHabilidadeConjuracao TEXT,
                    UsaMagiaPreparada INTEGER
                );
            ";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula a tabela Classe com dados provenientes do arquivo JSON, evitando duplicações.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para operações atômicas.</param>
        /// <returns>Tarefa assíncrona que realiza o povoamento da tabela.</returns>
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
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Classe WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", classe.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Classe (
                            Id, Nome, Descricao, DadoVida, Fonte, ImagemUrl,
                            IconeUrl, PapelTatico, IdHabilidadeConjuracao, UsaMagiaPreparada)
                        VALUES (
                            $id, $nome, $descricao, $dadoVida, $fonte, $imagemUrl,
                            $iconeUrl, $papelTatico, $habilidade, $usaMagia)";
                    insertCmd.Parameters.AddWithValue("$id", classe.Id);
                    insertCmd.Parameters.AddWithValue("$nome", classe.Nome ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$descricao", classe.Descricao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$dadoVida", classe.DadoVida ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$fonte", classe.Fonte ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$imagemUrl", classe.ImagemUrl ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$iconeUrl", classe.IconeUrl ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$papelTatico", classe.PapelTatico ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$habilidade", classe.IdHabilidadeConjuracao ?? string.Empty);
                    insertCmd.Parameters.AddWithValue("$usaMagia", classe.UsaMagiaPreparada ? 1 : 0);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }

            Console.WriteLine("✅ Classes populadas.");
        }
    }
}
