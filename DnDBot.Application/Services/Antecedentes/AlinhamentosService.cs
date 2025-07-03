using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;

namespace DnDBot.Application.Services.Antecedentes
{
    /// <summary>
    /// Serviço para gerenciamento dos alinhamentos no sistema.
    /// </summary>
    public class AlinhamentosService
    {
        private readonly DnDBotDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do serviço com o contexto do banco.
        /// </summary>
        /// <param name="context">Contexto do banco de dados DnDBotDbContext.</param>
        public AlinhamentosService(DnDBotDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista completa de alinhamentos cadastrados.
        /// </summary>
        /// <returns>Lista somente leitura de alinhamentos.</returns>
        public async Task<IReadOnlyList<Alinhamento>> ObterAlinhamentosAsync()
        {
            return await _context.Alinhamento.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Busca um alinhamento pelo seu identificador (ID).
        /// A busca ignora diferenças entre maiúsculas e minúsculas.
        /// </summary>
        /// <param name="id">ID do alinhamento a ser buscado.</param>
        /// <returns>Objeto Alinhamento encontrado ou null se não existir.</returns>
        public async Task<Alinhamento?> ObterAlinhamentoPorIdAsync(string id)
        {
            return await _context.Alinhamento
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id.ToLower() == id.ToLower());
        }

        /// <summary>
        /// Busca um alinhamento pelo nome exato.
        /// </summary>
        /// <param name="nome">Nome do alinhamento a ser buscado.</param>
        /// <returns>Objeto Alinhamento encontrado ou null se não existir.</returns>
        public async Task<Alinhamento?> ObterAlinhamentoPorNomeAsync(string nome)
        {
            return await _context.Alinhamento
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Nome == nome);
        }
    }
}
