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
    public class IdiomaService
    {
        private readonly DnDBotDbContext _dbContext;
        private readonly FichaService _fichaService;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public IdiomaService(DnDBotDbContext dbContext, FichaService fichaService)
        {
            _dbContext = dbContext;
            _fichaService = fichaService;
        }

        /// <summary>
        /// Carrega os idiomas associados a uma ficha específica.
        /// </summary>
        public async Task ObterFichaIdiomasAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.Idiomas)
                .LoadAsync();
        }

        /// <summary>
        /// Retorna todos os idiomas cadastrados no sistema.
        /// </summary>
        public async Task<List<Idioma>> ObterTodosIdiomasAsync()
        {
            return await _dbContext.Idioma
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }

        public async Task AdicionarIdiomasAsync(Guid fichaId, IEnumerable<Idioma> idiomas)
        {
            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) throw new InvalidOperationException("Ficha não encontrada");

            var idiomaIds = idiomas
                .Where(i => i != null)
                .Select(i => i.Id)
                .ToList();

            var idiomasDb = await _dbContext.Idioma
                .Where(i => idiomaIds.Contains(i.Id))
                .ToListAsync();

            foreach (var idioma in idiomasDb)
            {
                if (!ficha.Idiomas.Any(fi => fi.IdiomaId == idioma.Id))
                {
                    ficha.Idiomas.Add(new FichaPersonagemIdioma
                    {
                        FichaPersonagemId = ficha.Id,
                        IdiomaId = idioma.Id
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }


        public static async Task<FichaPersonagem> ObterFichaValidaAsync(Guid fichaId, ulong userId, FichaService fichaService)
        {
            var ficha = await fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null || ficha.JogadorId != userId)
                return null;

            return ficha;
        }

        public static async Task<List<Idioma>> ObterIdiomasDisponiveisAsync(FichaPersonagem ficha, IdiomaService idiomaService)
        {
            await idiomaService.ObterFichaIdiomasAsync(ficha);
            var todosIdiomas = await idiomaService.ObterTodosIdiomasAsync();

            var conhecidos = ficha.Idiomas.Select(i => i.IdiomaId).ToHashSet();
            return todosIdiomas
                .Where(i => !conhecidos.Contains(i.Id))
                .ToList();
        }

        public static async Task<string> AdicionarIdiomasAsync(FichaPersonagem ficha, IEnumerable<string> idiomaIds, IdiomaService idiomaService)
        {
            var todos = await idiomaService.ObterTodosIdiomasAsync();
            var conhecidos = ficha.Idiomas.Select(i => i.IdiomaId).ToHashSet();

            var selecionados = todos
                .Where(i => idiomaIds.Contains(i.Id) && !conhecidos.Contains(i.Id))
                .ToList();

            if (!selecionados.Any())
                return null;

            await idiomaService.AdicionarIdiomasAsync(ficha.Id, selecionados);
            return string.Join(", ", selecionados.Select(i => i.Nome));
        }


    }
}
