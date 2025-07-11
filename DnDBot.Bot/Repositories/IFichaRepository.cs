using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Threading.Tasks;

namespace DnDBot.Bot.Repositories
{
    public interface IFichaRepository
    {
        Task<FichaPersonagem> ObterFichaPorIdAsync(Guid id);
        Task InserirFichaAsync(FichaPersonagem ficha);
        Task<Inventario> ObterInventarioPorFichaIdAsync(Guid fichaId);
        Task AtualizarInventarioAsync(Guid fichaId, Inventario inventario);
        // outros métodos necessários
    }
}
