using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por acessar e consultar as classes de personagens armazenadas no banco de dados.
    /// </summary>
    public class ClassesService
    {
        private readonly DnDBotDbContext _db;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados e exibe o caminho do arquivo SQLite usado.
        /// </summary>
        /// <param name="db">Contexto do banco de dados DnDBotDbContext.</param>
        public ClassesService(DnDBotDbContext db)
        {
            _db = db;
            var connection = _db.Database.GetDbConnection();
            var path = new SqliteConnectionStringBuilder(connection.ConnectionString).DataSource;
            Console.WriteLine("Banco SQLite usado: " + Path.GetFullPath(path));
        }

        /// <summary>
        /// Retorna a lista completa de classes disponíveis no banco.
        /// </summary>
        /// <returns>Lista de objetos <see cref="Classe"/>.</returns>
        public async Task<List<Classe>> ObterClassesAsync()
        {
            return await _db.Classe.ToListAsync();
        }

        /// <summary>
        /// Obtém uma classe pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">ID da classe a ser buscada.</param>
        /// <returns>Objeto <see cref="Classe"/> ou null se não encontrado.</returns>
        public async Task<Classe> ObterClassePorIdAsync(string id)
        {
            return await _db.Classe
                .FirstOrDefaultAsync(c => c.Id.ToLower() == id.ToLower());
        }

        /// <summary>
        /// Obtém uma classe pelo seu nome.
        /// </summary>
        /// <param name="nome">Nome da classe a ser buscada.</param>
        /// <returns>Objeto <see cref="Classe"/> ou null se não encontrado.</returns>
        public async Task<Classe> ObterClassePorNomeAsync(string nome)
        {
            return await _db.Classe
                .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower());
        }

        /// <summary>
        /// Retorna uma lista contendo apenas os nomes de todas as classes cadastradas.
        /// </summary>
        /// <returns>Lista de nomes de classes.</returns>
        public async Task<List<string>> ObterNomesAsync()
        {
            return await _db.Classe
                .Select(c => c.Nome)
                .ToListAsync();
        }

        /// <summary>
        /// Retorna uma lista contendo os IDs de todas as classes cadastradas.
        /// </summary>
        /// <returns>Lista de IDs de classes.</returns>
        public async Task<List<string>> ObterIdsAsync()
        {
            return await _db.Classe
                .Select(c => c.Id)
                .ToListAsync();
        }
    }
}
