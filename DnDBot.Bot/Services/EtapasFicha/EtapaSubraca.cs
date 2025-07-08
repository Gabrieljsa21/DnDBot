using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class EtapaSubraca : IEtapaFicha
    {
        private readonly RacasService _racasService;

        public EtapaSubraca(RacasService racasService)
        {
            _racasService = racasService;
        }

        public async Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
        {
            var raca = await _racasService.ObterRacaPorIdAsync(ficha.RacaId);

            // Se a raça não tiver sub-raças, já está completa
            if (raca?.SubRaca?.Any() != true)
                return true;

            // Se tiver sub-raças, considera completa só quando SubracaId for diferente do placeholder
            return !string.IsNullOrWhiteSpace(ficha.SubracaId)
                && !string.Equals(ficha.SubracaId, "Não definida", StringComparison.OrdinalIgnoreCase);
        }

        public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            ficha.EtapaAtual = EtapaCriacaoFicha.Subraca;
            var raca = await _racasService.ObterRacaPorIdAsync(ficha.RacaId);
            if (raca?.SubRaca?.Any() != true) return;

            string customId = $"select_subraca";

            var selectSubraca = SelectMenuHelper.CriarSelectSubraca(raca.SubRaca, customId);

            await context.Interaction.FollowupAsync(
                "Escolha a sub-raça do seu personagem:",
                components: new ComponentBuilder().WithSelectMenu(selectSubraca).Build(),
                ephemeral: true
            );


        }

    }
}
