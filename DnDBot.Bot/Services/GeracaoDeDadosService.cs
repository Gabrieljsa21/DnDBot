using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Services.DatabaseSetup;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services
{
    /// <summary>
    /// Serviço responsável pela geração e popularização das tabelas do banco de dados.
    /// </summary>
    public class GeracaoDeDadosService
    {
        private readonly string _connectionString;
        private readonly DnDBotDbContext _db;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="GeracaoDeDadosService"/>.
        /// </summary>
        /// <param name="configuration">Configuração da aplicação contendo a connection string.</param>
        /// <param name="db">Contexto do banco de dados.</param>
        public GeracaoDeDadosService(IConfiguration configuration, DnDBotDbContext db)
        {
            _db = db;
            var connection = _db.Database.GetDbConnection();
            var path = new SqliteConnectionStringBuilder(connection.ConnectionString).DataSource;

            _connectionString = configuration.GetConnectionString("DnDBotDatabase")
                ?? throw new ArgumentNullException("Connection string 'DnDBotDatabase' não encontrada.");
        }

        /// <summary>
        /// Cria todas as tabelas necessárias no banco de dados, caso não existam.
        /// </summary>
        public async Task CriarTabelasAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            using var cmd = connection.CreateCommand();

            await ListarTabelasAsync();

            await IdiomaDatabaseHelper.CriarTabelaAsync(cmd);
            await MagiaDatabaseHelper.CriarTabelaAsync(cmd);
            await PericiaDatabaseHelper.CriarTabelaAsync(cmd);
            await ProficienciaDatabaseHelper.CriarTabelaAsync(cmd);
            await ArmaDatabaseHelper.CriarTabelaAsync(cmd);
            await ArmaduraDatabaseHelper.CriarTabelaAsync(cmd);
            await AlinhamentoDatabaseHelper.CriarTabelaAsync(cmd);
            await ResistenciaDatabaseHelper.CriarTabelaAsync(cmd);
            await CaracteristicaDatabaseHelper.CriarTabelaAsync(cmd);
            await RacaDatabaseHelper.CriarTabelaAsync(cmd);
            await ClasseDatabaseHelper.CriarTabelaAsync(cmd);
            await AntecedenteDatabaseHelper.CriarTabelaAsync(cmd);
            await FichaDatabaseHelper.CriarTabelaAsync(cmd);
            await SubRacaResistenciaDatabaseHelper.CriarTabelaAsync(cmd);
            await SubRacaIdiomaDatabaseHelper.CriarTabelaAsync(cmd);
            await SubRacaCaracteristicaDatabaseHelper.CriarTabelaAsync(cmd);
            await SubRacaProficienciaDatabaseHelper.CriarTabelaAsync(cmd);
            await SubRacaMagiaDatabaseHelper.CriarTabelaAsync(cmd);
        }

        /// <summary>
        /// Popula as tabelas do banco com os dados básicos lidos dos arquivos JSON.
        /// </summary>
        public async Task PopularDadosBaseAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction();

            await IdiomaDatabaseHelper.PopularAsync(connection, transaction);
            await MagiaDatabaseHelper.PopularAsync(connection, transaction);
            await PericiaDatabaseHelper.PopularAsync(connection, transaction);
            await ProficienciaDatabaseHelper.PopularAsync(connection, transaction);
            await ArmaDatabaseHelper.PopularAsync(connection, transaction);
            await ArmaduraDatabaseHelper.PopularAsync(connection, transaction);
            await AlinhamentoDatabaseHelper.PopularAsync(connection, transaction);
            await ResistenciaDatabaseHelper.PopularAsync(connection, transaction);
            await CaracteristicaDatabaseHelper.PopularAsync(connection, transaction);
            await RacaDatabaseHelper.PopularAsync(connection, transaction);
            await ClasseDatabaseHelper.PopularAsync(connection, transaction);
            await AntecedenteDatabaseHelper.PopularAsync(connection, transaction);
            await SubRacaResistenciaDatabaseHelper.PopularAsync(connection, transaction);
            await SubRacaIdiomaDatabaseHelper.PopularAsync(connection, transaction);
            await SubRacaCaracteristicaDatabaseHelper.PopularAsync(connection, transaction);
            await SubRacaProficienciaDatabaseHelper.PopularAsync(connection, transaction);
            await SubRacaMagiaDatabaseHelper.PopularAsync(connection, transaction);

            transaction.Commit();
            Console.WriteLine("Commit feito!");
        }

        /// <summary>
        /// Lista todas as tabelas presentes no banco de dados atual.
        /// </summary>
        public async Task ListarTabelasAsync()
        {
            using var conn = _db.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";

            using var reader = await cmd.ExecuteReaderAsync();
            Console.WriteLine("Tabelas no banco:");
            while (await reader.ReadAsync())
            {
                Console.WriteLine(reader.GetString(0));
            }
        }
    }
}
