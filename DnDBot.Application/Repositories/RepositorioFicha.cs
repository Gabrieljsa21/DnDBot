using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;

namespace DnDBot.Application.Repositories
{
    /// <summary>
    /// Repositório responsável pela persistência das fichas de personagem no banco de dados SQLite.
    /// </summary>
    public class RepositorioFicha
    {
        /// <summary>
        /// String de conexão para o banco SQLite.
        /// </summary>
        private readonly string connectionString = @"Data Source=E:\source\repos\DnDBot\dndbot.db;";

        /// <summary>
        /// Insere uma nova ficha de personagem na tabela FichaPersonagem.
        /// </summary>
        /// <param name="ficha">Objeto FichaPersonagem contendo os dados a serem inseridos.</param>
        public void InserirFicha(FichaPersonagem ficha)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO FichaPersonagem (JogadorId, Nome, RacaId, SubracaId, ClasseId, AntecedenteId, AlinhamentoId)
                                VALUES (@jogadorId, @nome, @racaId, @subracaId, @classeId, @antecedenteId, @alinhamentoId)";

            command.Parameters.AddWithValue("@jogadorId", ficha.JogadorId);
            command.Parameters.AddWithValue("@nome", ficha.Nome);
            command.Parameters.AddWithValue("@racaId", ficha.RacaId);
            command.Parameters.AddWithValue("@subracaId", ficha.SubracaId);
            command.Parameters.AddWithValue("@classeId", ficha.ClasseId);
            command.Parameters.AddWithValue("@antecedenteId", ficha.AntecedenteId);
            command.Parameters.AddWithValue("@alinhamentoId", ficha.AlinhamentoId);

            command.ExecuteNonQuery();
        }

        // Outros métodos para buscar, atualizar e deletar fichas podem ser implementados aqui.
    }
}
