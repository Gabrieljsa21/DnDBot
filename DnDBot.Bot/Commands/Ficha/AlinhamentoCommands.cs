using Discord.Interactions;
using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo de interação responsável por comandos relacionados aos alinhamentos.
    /// </summary>
    public class AlinhamentoCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly string _connectionString;

        /// <summary>
        /// Construtor que recebe a configuração da aplicação.
        /// </summary>
        /// <param name="config">Instância da configuração para obter a connection string.</param>
        public AlinhamentoCommands(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DnDBotDatabase");
        }

        /// <summary>
        /// Comando do Discord que lista todos os alinhamentos salvos no banco de dados.
        /// </summary>
        [SlashCommand("listar_alinhamentos", "Lista todos os alinhamentos salvos no banco.")]
        public async Task ListarAlinhamentosAsync()
        {
            var alinhamentos = new List<Alinhamento>();

            // Conexão com o banco SQLite
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            // Comando SQL para selecionar todos os alinhamentos
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Nome, Descricao FROM Alinhamento";

            // Leitura dos dados
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                alinhamentos.Add(new Alinhamento
                {
                    Id = reader.GetString(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2)
                });
            }

            // Verificação se há registros
            if (alinhamentos.Count == 0)
            {
                await RespondAsync("Nenhum alinhamento encontrado no banco.");
                return;
            }

            // Construção da mensagem de resposta
            var mensagem = "Alinhamentos encontrados:\n";
            foreach (var a in alinhamentos)
            {
                mensagem += $"- **{a.Nome}** ({a.Id}): {a.Descricao}\n";
            }

            await RespondAsync(mensagem, ephemeral: true);
        }
    }
}
