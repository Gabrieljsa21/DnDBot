using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services
{
    public class ProficienciaService
    {
        private readonly DnDBotDbContext _dbContext;
        private readonly FichaService _fichaService;

        public ProficienciaService(DnDBotDbContext dbContext, FichaService fichaService)
        {
            _dbContext = dbContext;
            _fichaService = fichaService;
        }

        public async Task ObterFichaProficienciasAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.Proficiencias)
                .LoadAsync();
        }

        public async Task<List<Proficiencia>> ObterTodasProficienciasAsync()
        {
            return await _dbContext.Proficiencia
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task AdicionarProficienciasAsync(Guid fichaId, IEnumerable<Proficiencia> proficiencias)
        {
            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) throw new InvalidOperationException("Ficha não encontrada");

            var ids = proficiencias.Where(p => p != null).Select(p => p.Id).ToList();

            var proficienciasDb = await _dbContext.Proficiencia
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();

            foreach (var prof in proficienciasDb)
            {
                if (!ficha.Proficiencias.Any(fp => fp.ProficienciaId == prof.Id))
                {
                    ficha.Proficiencias.Add(new FichaPersonagemProficiencia
                    {
                        FichaPersonagemId = ficha.Id,
                        ProficienciaId = prof.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        private static async Task<bool> VerificarPericiaExiste(SqliteConnection connection, SqliteTransaction transaction, string periciaId)
        {
            const string sql = "SELECT 1 FROM Pericia WHERE Id = $id LIMIT 1;";
            using var cmd = new SqliteCommand(sql, connection, transaction);
            cmd.Parameters.AddWithValue("$id", periciaId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null;
        }
    }
}
