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
    public class CaracteristicaService
    {
        private readonly DnDBotDbContext _dbContext;
        private readonly FichaService _fichaService;

        public CaracteristicaService(DnDBotDbContext dbContext, FichaService fichaService)
        {
            _dbContext = dbContext;
            _fichaService = fichaService;
        }

        public async Task ObterFichaCaracteristicasAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.Caracteristicas)
                .LoadAsync();
        }

        public async Task<List<Caracteristica>> ObterTodasCaracteristicasAsync()
        {
            return await _dbContext.Caracteristica
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }

        public async Task AdicionarCaracteristicasAsync(Guid fichaId, IEnumerable<Caracteristica> caracteristicas)
        {
            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) throw new InvalidOperationException("Ficha não encontrada");

            var ids = caracteristicas.Where(c => c != null).Select(c => c.Id).ToList();

            var caracteristicasDb = await _dbContext.Caracteristica
                .Where(c => ids.Contains(c.Id))
                .ToListAsync();

            foreach (var caracteristica in caracteristicasDb)
            {
                if (!ficha.Caracteristicas.Any(fc => fc.CaracteristicaId == caracteristica.Id))
                {
                    ficha.Caracteristicas.Add(new FichaPersonagemCaracteristica
                    {
                        FichaPersonagemId = ficha.Id,
                        CaracteristicaId = caracteristica.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
