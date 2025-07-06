using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Inventario;
using DnDBot.Application.Models.ItensInventario;
using System;
using System.Threading.Tasks;

namespace DnDBot.Application.Repositories
{
    public interface IFichaRepository
    {
        Task<FichaPersonagem?> ObterFichaPorIdAsync(Guid id);
        Task InserirFichaAsync(FichaPersonagem ficha);
        Task<Inventario?> ObterInventarioPorFichaIdAsync(Guid fichaId);
        Task AtualizarInventarioAsync(Guid fichaId, Inventario inventario);
        // outros métodos necessários
    }
}
