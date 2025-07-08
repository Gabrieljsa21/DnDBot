using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Data.Sqlite;
using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha;

namespace DnDBot.Bot.Services
{
    /// <summary>
    /// Serviço responsável por acessar as raças e sub-raças do banco de dados.
    /// </summary>
    public class RacasService
    {
        private readonly DnDBotDbContext _db;

        /// <summary>
        /// Construtor do serviço de raças.
        /// </summary>
        /// <param name="db">Contexto do banco de dados.</param>
        public RacasService(DnDBotDbContext db)
        {
            _db = db;
            var connection = _db.Database.GetDbConnection();
            var path = new SqliteConnectionStringBuilder(connection.ConnectionString).DataSource;
        }

        /// <summary>
        /// Retorna todas as raças cadastradas, incluindo suas sub-raças.
        /// </summary>
        /// <returns>Lista de raças com sub-raças.</returns>
        public async Task<IReadOnlyList<Raca>> ObterRacasAsync()
        {
            return await _db.Raca
                .Include(r => r.SubRaca)
                .Include(r => r.RacaTags)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma raça específica com base no ID fornecido.
        /// </summary>
        /// <param name="id">ID da raça.</param>
        /// <returns>Objeto da raça correspondente ou null se não encontrada.</returns>
        public async Task<Raca> ObterRacaPorIdAsync(string id)
        {
            return await _db.Raca
                .Include(r => r.SubRaca)
                .ThenInclude(sr => sr.Idiomas)
                .Include(r => r.RacaTags)
                .FirstOrDefaultAsync(r => r.Id.ToLower() == id.ToLower());
        }

        /// <summary>
        /// Retorna uma raça específica com base no nome fornecido.
        /// </summary>
        /// <param name="nome">Nome da raça.</param>
        /// <returns>Objeto da raça correspondente ou null se não encontrada.</returns>
        public async Task<Raca> ObterRacaPorNomeAsync(string nome)
        {
            return await _db.Raca
                .Include(r => r.SubRaca)
                .Include(r => r.RacaTags)
                .FirstOrDefaultAsync(r => r.Nome.ToLower() == nome.ToLower());
        }

        /// <summary>
        /// Retorna todas as sub-raças disponíveis no banco de dados.
        /// </summary>
        /// <returns>Lista de sub-raças.</returns>
        public async Task<List<SubRaca>> ObterTodasSubracasAsync()
        {
            return await _db.SubRaca
                .Include(sr => sr.Idiomas)
                .Include(sr => sr.Proficiencias)
                .Include(sr => sr.Caracteristicas)
                .Include(sr => sr.Resistencias)
                .Include(sr => sr.MagiasRaciais)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma sub-raça específica com base no ID fornecido.
        /// </summary>
        /// <param name="idSubRaca">ID da sub-raça.</param>
        /// <returns>Objeto da sub-raça correspondente ou null se não encontrada.</returns>
        public async Task<SubRaca> ObterSubRacaPorIdAsync(string idSubRaca)
        {
            return await _db.SubRaca
                .FirstOrDefaultAsync(sr => sr.Id.ToLower() == idSubRaca.ToLower());
        }
    }
}
