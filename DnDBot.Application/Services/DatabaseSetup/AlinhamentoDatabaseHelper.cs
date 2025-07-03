using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace DnDBot.Application.Services.DatabaseSetup
{
    /// <summary>
    /// Helper estático para criação e povoamento da tabela Alinhamento no banco SQLite.
    /// </summary>
    public static class AlinhamentoDatabaseHelper
    {
        /// <summary>
        /// Cria a tabela Alinhamento no banco de dados se ela não existir.
        /// </summary>
        /// <param name="cmd">Comando SQLite já associado a uma conexão aberta.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task CriarTabelaAsync(SqliteCommand cmd)
        {
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Alinhamento (
                    Id TEXT PRIMARY KEY,
                    Nome TEXT NOT NULL,
                    Descricao TEXT NOT NULL
                );";
            await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Insere os dados iniciais de alinhamentos na tabela, caso ainda não existam.
        /// </summary>
        /// <param name="connection">Conexão SQLite aberta.</param>
        /// <param name="transaction">Transação SQLite para garantir atomicidade.</param>
        /// <returns>Tarefa assíncrona representando a operação.</returns>
        public static async Task PopularAsync(SqliteConnection connection, SqliteTransaction transaction)
        {
            foreach (var alinhamento in AlinhamentosData.Alinhamentos)
            {
                var existsCmd = connection.CreateCommand();
                existsCmd.Transaction = transaction;
                existsCmd.CommandText = "SELECT COUNT(*) FROM Alinhamento WHERE Id = $id";
                existsCmd.Parameters.AddWithValue("$id", alinhamento.Id);

                var count = Convert.ToInt32(await existsCmd.ExecuteScalarAsync());

                if (count == 0)
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.Transaction = transaction;
                    insertCmd.CommandText = @"
                        INSERT INTO Alinhamento (Id, Nome, Descricao)
                        VALUES ($id, $nome, $descricao)";
                    insertCmd.Parameters.AddWithValue("$id", alinhamento.Id);
                    insertCmd.Parameters.AddWithValue("$nome", alinhamento.Nome);
                    insertCmd.Parameters.AddWithValue("$descricao", alinhamento.Descricao);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
