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
        private readonly string connectionString = @"Data Source=D:\source\repos\DnDBot\dndbot.db;";

        /// <summary>
        /// Insere uma nova ficha de personagem na tabela FichaPersonagem.
        /// </summary>
        /// <param name="ficha">Objeto FichaPersonagem contendo os dados a serem inseridos.</param>
        public void InserirFicha(FichaPersonagem ficha)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO FichaPersonagem (IdJogador, Nome, IdRaca, IdSubraca, IdClasse, IdAntecedente, IdAlinhamento)
                                VALUES (@idJogador, @nome, @idRaca, @idSubraca, @idClasse, @idAntecedente, @idAlinhamento)";

            command.Parameters.AddWithValue("@idJogador", ficha.IdJogador);
            command.Parameters.AddWithValue("@nome", ficha.Nome);
            command.Parameters.AddWithValue("@idRaca", ficha.IdRaca);
            command.Parameters.AddWithValue("@idSubraca", ficha.IdSubraca);
            command.Parameters.AddWithValue("@idClasse", ficha.IdClasse);
            command.Parameters.AddWithValue("@idAntecedente", ficha.IdAntecedente);
            command.Parameters.AddWithValue("@idAlinhamento", ficha.IdAlinhamento);

            command.ExecuteNonQuery();
        }

        // Outros métodos para buscar, atualizar e deletar fichas podem ser implementados aqui.
    }
}
