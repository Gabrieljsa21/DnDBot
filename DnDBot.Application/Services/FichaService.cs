using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço para gerenciamento das fichas de personagens dos jogadores.
    /// </summary>
    public class FichaService
    {
        private readonly DnDBotDbContext _dbContext;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        /// <param name="dbContext">Contexto do banco de dados DnDBotDbContext.</param>
        public FichaService(DnDBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Cria uma ficha básica com dados iniciais padrão.
        /// </summary>
        /// <param name="nome">Nome da ficha/personagem.</param>
        /// <param name="jogadorId">ID do jogador dono da ficha.</param>
        /// <returns>A ficha criada.</returns>
        public async Task<FichaPersonagem> CriarFichaBasicaAsync(string nome, ulong jogadorId)
        {
            var ficha = new FichaPersonagem
            {
                Nome = nome,
                JogadorId = jogadorId,
                RacaId = "Não definida",
                ClasseId = "Não definida",
                AntecedenteId = "Não definido",
                AlinhamentoId = "Não definido",
                CriadoEm = DateTime.UtcNow,
                ModificadoEm = DateTime.UtcNow
            };

            _dbContext.FichaPersonagem.Add(ficha);
            await _dbContext.SaveChangesAsync();
            return ficha;
        }

        /// <summary>
        /// Adiciona uma ficha já criada ao banco de dados.
        /// </summary>
        /// <param name="ficha">Ficha a ser adicionada.</param>
        public async Task AdicionarFichaAsync(FichaPersonagem ficha)
        {
            if (ficha.Inventario != null)
            {
                ficha.Inventario.Id = Guid.NewGuid().ToString(); // Garante ID único
            }

            _dbContext.FichaPersonagem.Add(ficha);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém todas as fichas associadas a um jogador pelo seu ID.
        /// </summary>
        /// <param name="jogadorId">ID do jogador.</param>
        /// <returns>Lista de fichas do jogador.</returns>
        public async Task<List<FichaPersonagem>> ObterFichasPorJogadorAsync(ulong jogadorId)
        {
            return await _dbContext.FichaPersonagem
                .Where(f => f.JogadorId == jogadorId)
                .ToListAsync();
        }

        /// <summary>
        /// Busca uma ficha específica pelo jogador e nome da ficha.
        /// </summary>
        /// <param name="jogadorId">ID do jogador.</param>
        /// <param name="nome">Nome da ficha.</param>
        /// <returns>A ficha encontrada ou null se não existir.</returns>
        public async Task<FichaPersonagem?> ObterFichaPorJogadorENomeAsync(ulong jogadorId, string nome)
        {
            return await _dbContext.FichaPersonagem
                .FirstOrDefaultAsync(f => f.JogadorId == jogadorId && f.Nome.ToLower() == nome.ToLower());
        }

        /// <summary>
        /// Atualiza os dados de uma ficha existente no banco.
        /// Atualiza também a data da última alteração para o momento atual.
        /// </summary>
        /// <param name="fichaAtualizada">Ficha com dados atualizados.</param>
        public async Task AtualizarFichaAsync(FichaPersonagem fichaAtualizada)
        {
            fichaAtualizada.ModificadoEm = DateTime.UtcNow;
            _dbContext.FichaPersonagem.Update(fichaAtualizada);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Obtém a ficha mais recentemente criada por um jogador.
        /// </summary>
        /// <param name="jogadorId">ID do jogador.</param>
        /// <returns>A ficha mais nova do jogador, ou null se nenhuma existir.</returns>
        public async Task<FichaPersonagem?> ObterUltimaFichaDoJogadorAsync(ulong jogadorId)
        {
            return await _dbContext.FichaPersonagem
                .Where(f => f.JogadorId == jogadorId)
                .OrderByDescending(f => f.CriadoEm)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Busca uma ficha específica pelo seu Id.
        /// </summary>
        /// <param name="id">Id da ficha.</param>
        /// <returns>A ficha encontrada ou null se não existir.</returns>
        public async Task<FichaPersonagem?> ObterFichaPorIdAsync(Guid id)
        {
            return await _dbContext.FichaPersonagem
                .Include(f => f.Idiomas)
                .FirstOrDefaultAsync(f => f.Id == id);
        }


        /// <summary>
        /// Formata o atributo com valor total e modificador (ex: "16 (+3)").
        /// </summary>
        public string FormatarAtributo(FichaPersonagem ficha, string atributo)
        {
            int total = ficha.ObterTotalComBonus(atributo);
            int mod = ficha.ObterModificador(atributo);
            string modStr = mod >= 0 ? $"+{mod}" : mod.ToString();
            return $"{total} ({modStr})";
        }

        public async Task CarregarIdiomasAsync(FichaPersonagem ficha)
        {
            await _dbContext.Entry(ficha)
                .Collection(f => f.Idiomas)
                .LoadAsync();
        }

    }
}
