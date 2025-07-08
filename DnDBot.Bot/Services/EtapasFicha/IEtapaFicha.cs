using System.Threading.Tasks;
using Discord.Interactions;
using DnDBot.Bot.Models.Ficha;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public interface IEtapaFicha
    {
        Task<bool> EstaCompletaAsync(FichaPersonagem ficha);
        Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false);
    }


}
