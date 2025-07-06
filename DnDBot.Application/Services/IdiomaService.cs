using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Application.Services
{
    public class IdiomaService
    {
        private readonly DnDBotDbContext _dbContext;

        /// <summary>
        /// Construtor que recebe o contexto do banco de dados via injeção de dependência.
        /// </summary>
        public IdiomaService(DnDBotDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return await _dbContext.Idiomas
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }
    }
}
