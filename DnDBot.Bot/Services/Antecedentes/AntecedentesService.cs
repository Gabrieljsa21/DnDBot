using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DnDBot.Bot.Data;
using DnDBot.Bot.Models.AntecedenteModels;

namespace DnDBot.Bot.Services.Antecedentes
{
    /// <summary>
    /// Serviço responsável pelo gerenciamento e acesso aos antecedentes.
    /// </summary>
    public class AntecedentesService
    {
        private readonly DnDBotDbContext _context;

        /// <summary>
        /// Inicializa uma nova instância do serviço com o contexto do banco.
        /// </summary>
        /// <param name="context">Contexto do banco de dados DnDBotDbContext.</param>
        public AntecedentesService(DnDBotDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém a lista completa de antecedentes cadastrados.
        /// </summary>
        /// <returns>Lista somente leitura de antecedentes.</returns>
        public async Task<IReadOnlyList<Antecedente>> ObterAntecedentesAsync()
        {
            return await _context.Antecedente
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Busca um antecedente pelo seu identificador (ID).
        /// A busca ignora diferenças entre maiúsculas e minúsculas.
        /// </summary>
        /// <param name="id">ID do antecedente a ser buscado.</param>
        /// <returns>Objeto Antecedente encontrado ou null se não existir.</returns>
        public async Task<Antecedente> ObterAntecedentePorIdAsync(string id)
        {
            return await _context.Antecedente
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id.ToLower() == id.ToLower());
        }

        /// <summary>
        /// Busca um antecedente pelo nome exato.
        /// A busca ignora diferenças entre maiúsculas e minúsculas.
        /// </summary>
        /// <param name="nome">Nome do antecedente a ser buscado.</param>
        /// <returns>Objeto Antecedente encontrado ou null se não existir.</returns>
        public async Task<Antecedente> ObterAntecedentePorNomeAsync(string nome)
        {
            return await _context.Antecedente
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Nome.ToLower() == nome.ToLower());
        }
    }
}
