using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services
{
    public class MagiaService
    {
        private readonly DnDBotDbContext _dbContext;
        private readonly FichaService _fichaService;

        public MagiaService(DnDBotDbContext dbContext, FichaService fichaService)
        {
            _dbContext = dbContext;
            _fichaService = fichaService;
        }

        public async Task ObterFichaMagiasRaciaisAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.MagiasRaciais)
                .LoadAsync();
        }

        public async Task<List<Magia>> ObterTodasMagiasAsync()
        {
            return await _dbContext.Magia
                .OrderBy(m => m.Nome)
                .ToListAsync();
        }

        public async Task AdicionarMagiasAsync(Guid fichaId, IEnumerable<Magia> magias)
        {
            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) throw new InvalidOperationException("Ficha não encontrada");

            var ids = magias.Where(m => m != null).Select(m => m.Id).ToList();

            var magiasDb = await _dbContext.Magia
                .Where(m => ids.Contains(m.Id))
                .ToListAsync();

            foreach (var magia in magiasDb)
            {
                if (!ficha.MagiasRaciais.Any(fm => fm.MagiaId == magia.Id))
                {
                    ficha.MagiasRaciais.Add(new FichaPersonagemMagia
                    {
                        FichaPersonagemId = ficha.Id,
                        MagiaId = magia.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
