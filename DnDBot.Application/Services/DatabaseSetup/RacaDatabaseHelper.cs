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
    /// Classe estática auxiliar para criação e população das tabelas Raca e SubRaca no banco SQLite.
    /// </summary>
    public static class RacaDatabaseHelper
    {
        /// <summary>
        /// Caminho do arquivo JSON que contém os dados das raças.
        /// </summary>
        private const string CaminhoJsonRacas = "Data/racas.json";

        /// <summary>
        /// Cria as tabelas Raca e SubRaca no banco, se ainda não existirem.
        /// </summary>
        /// <param name="cmd">Comando SQLite para execução dos comandos SQL.</param>
        /// <returns>Tarefa assíncrona para a operação de criação.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Raca (
                    Id TEXT PRIMARY KEY,
                    Fonte TEXT,
                    Nome TEXT NOT NULL,
                    Descricao TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS SubRaca (
                    Id TEXT PRIMARY KEY,
                    IdRaca TEXT NOT NULL,
                    Nome TEXT NOT NULL,
                    Descricao TEXT NOT NULL,
                    TendenciasComuns TEXT,
                    Tamanho TEXT,
                    Deslocamento INTEGER,
                    VisaoNoEscuro INTEGER,
                    IconeUrl TEXT,
                    ImagemUrl TEXT
                );";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Popula as tabelas Raca e SubRaca a partir dos dados do arquivo JSON.
        /// Realiza verificações para evitar inserções duplicadas.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para operações atômicas.</param>
        /// <returns>Tarefa assíncrona para a operação de inserção.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            if (!File.Exists(CaminhoJsonRacas))
            {
                Console.WriteLine("❌ Arquivo racas.json não encontrado.");
                return;
            }

            Console.WriteLine("📥 Lendo dados de racas.json...");

            var json = await File.ReadAllTextAsync(CaminhoJsonRacas, Encoding.UTF8);
            var racas = JsonSerializer.Deserialize<List<Raca>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            });

            if (racas == null) return;

            foreach (var raca in racas)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Raca WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", raca.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Raca (Id, Fonte, Nome, Descricao)
                        VALUES ($id, $fonte, $nome, $descricao)";
                    insertCmd.Parameters.AddWithValue("$id", raca.Id);
                    insertCmd.Parameters.AddWithValue("$fonte", raca.Fonte);
                    insertCmd.Parameters.AddWithValue("$nome", raca.Nome);
                    insertCmd.Parameters.AddWithValue("$descricao", raca.Descricao);
                    await insertCmd.ExecuteNonQueryAsync();
                }

                if (raca.SubRaca != null)
                {
                    foreach (var sub in raca.SubRaca)
                    {
                        var subExistsCmd = connection.CreateCommand();
                        subExistsCmd.Transaction = transaction;
                        subExistsCmd.CommandText = "SELECT COUNT(*) FROM SubRaca WHERE Id = $id";
                        subExistsCmd.Parameters.AddWithValue("$id", sub.Id);

                        var subCount = Convert.ToInt32(await subExistsCmd.ExecuteScalarAsync());

                        if (subCount == 0)
                        {
                            var insertSub = connection.CreateCommand();
                            insertSub.Transaction = transaction;
                            insertSub.CommandText = @"
                                INSERT INTO SubRaca (
                                    Id, IdRaca, Nome, Descricao,
                                    TendenciasComuns, Tamanho, Deslocamento,
                                    VisaoNoEscuro, IconeUrl, ImagemUrl)
                                VALUES (
                                    $id, $idRaca, $nome, $descricao,
                                    $tendencias, $tamanho, $deslocamento,
                                    $visao, $icone, $imagem)";
                            insertSub.Parameters.AddWithValue("$id", sub.Id);
                            insertSub.Parameters.AddWithValue("$idRaca", raca.Id);
                            insertSub.Parameters.AddWithValue("$nome", sub.Nome);
                            insertSub.Parameters.AddWithValue("$descricao", sub.Descricao);
                            insertSub.Parameters.AddWithValue("$tendencias", sub.TendenciasComuns ?? string.Empty);
                            insertSub.Parameters.AddWithValue("$tamanho", sub.Tamanho ?? string.Empty);
                            insertSub.Parameters.AddWithValue("$deslocamento", sub.Deslocamento);
                            insertSub.Parameters.AddWithValue("$visao", sub.VisaoNoEscuro);
                            insertSub.Parameters.AddWithValue("$icone", sub.IconeUrl ?? string.Empty);
                            insertSub.Parameters.AddWithValue("$imagem", sub.ImagemUrl ?? string.Empty);
                            await insertSub.ExecuteNonQueryAsync();
                        }
                    }
                }
            }

            Console.WriteLine("✅ Raças e sub-raças populadas.");
        }
    }
}
