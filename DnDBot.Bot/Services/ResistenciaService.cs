using DnDBot.Bot.Data;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services
{
    public class ResistenciaService
    {
        private readonly DnDBotDbContext _dbContext;
        private readonly FichaService _fichaService;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public ResistenciaService(DnDBotDbContext dbContext, FichaService fichaService)
        {
            _dbContext = dbContext;
            _fichaService = fichaService;
        }

        /// <summary>
        /// Carrega os resistencias associados a uma ficha específica.
        /// </summary>
        public async Task ObterFichaResistenciasAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.Resistencias)
                .LoadAsync();
        }

        /// <summary>
        /// Retorna todos os resistencias cadastrados no sistema.
        /// </summary>
        public async Task<List<Resistencia>> ObterTodosResistenciasAsync()
        {
            return await _dbContext.Resistencia
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }


        public async Task AdicionarResistenciasAsync(Guid fichaId, IEnumerable<string> resistenciaIds)
        {
            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) throw new InvalidOperationException("Ficha não encontrada");

            var todasResistencias = await ObterTodosResistenciasAsync(); // ou buscar apenas as necessárias
            var mapaResistencias = todasResistencias.ToDictionary(r => r.Id);

            foreach (var resistenciaId in resistenciaIds.Distinct())
            {
                if (!ficha.Resistencias.Any(r => r.ResistenciaId == resistenciaId) && mapaResistencias.TryGetValue(resistenciaId, out var resistencia))
                {
                    ficha.Resistencias.Add(new FichaPersonagemResistencia
                    {
                        FichaPersonagemId = ficha.Id,
                        ResistenciaId = resistencia.Id,
                        TipoDano = resistencia.TipoDano
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }




        public static async Task<List<TipoDano>> ObterResistenciasDisponiveisAsync(FichaPersonagem ficha, ResistenciaService resistenciaService)
        {
            await resistenciaService.ObterFichaResistenciasAsync(ficha);

            // Pega apenas os Tipos de Dano da lista de resistências cadastradas
            var todosTiposDano = (await resistenciaService.ObterTodosResistenciasAsync())
                .Select(r => r.TipoDano)
                .ToList();

            var conhecidos = ficha.Resistencias.Select(i => i.TipoDano).ToHashSet();

            return todosTiposDano
                .Where(tipo => !conhecidos.Contains(tipo))
                .ToList();
        }


        public static async Task<string> AdicionarResistenciasAsync(
    FichaPersonagem ficha,
    IEnumerable<string> resistenciaIds,
    ResistenciaService resistenciaService)
        {
            var todos = await resistenciaService.ObterTodosResistenciasAsync();

            // Verifica resistências já conhecidas pela ficha
            var conhecidos = ficha.Resistencias.Select(r => r.ResistenciaId).ToHashSet();

            // Filtra resistências ainda não adicionadas
            var selecionados = todos
                .Where(r => resistenciaIds.Contains(r.Id) && !conhecidos.Contains(r.Id))
                .ToList();

            if (!selecionados.Any())
                return null;

            var idsParaAdicionar = selecionados.Select(r => r.Id);

            await resistenciaService.AdicionarResistenciasAsync(ficha.Id, idsParaAdicionar);

            return string.Join(", ", selecionados.Select(r => r.Nome));
        }






    }
}
