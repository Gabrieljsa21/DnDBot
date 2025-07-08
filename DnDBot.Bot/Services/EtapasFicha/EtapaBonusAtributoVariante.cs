using System.Linq;
using System.Threading.Tasks;
using Discord.Interactions;
using Discord;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Services;
using DnDBot.Bot.Models.Ficha;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class EtapaBonusAtributoVariante : IEtapaFicha
    {
        private readonly FichaService _fichaService;

        public EtapaBonusAtributoVariante(FichaService fichaService)
        {
            _fichaService = fichaService;
        }

        public Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
        {
            bool completa = ficha.BonusAtributos.Any(b => b.Origem == "VarianteCustomBonus");
            return Task.FromResult(completa);
        }

        public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            ficha.EtapaAtual = EtapaCriacaoFicha.BonusAtributos;
            var atributos = new[]
            {
                "Forca", "Destreza", "Constituicao",
                "Inteligencia", "Sabedoria", "Carisma"
            };

            var menu2 = new SelectMenuBuilder()
                .WithCustomId($"bonus_atributo_2_{ficha.Id}")
                .WithPlaceholder("Escolha o atributo que receberá +2")
                .WithMinValues(1).WithMaxValues(1);

            var menu1 = new SelectMenuBuilder()
                .WithCustomId($"bonus_atributo_1_{ficha.Id}")
                .WithPlaceholder("Escolha o atributo que receberá +1")
                .WithMinValues(1).WithMaxValues(1);

            foreach (var atributo in atributos)
            {
                menu2.AddOption(atributo, atributo);
                menu1.AddOption(atributo, atributo);
            }

            var builder = new ComponentBuilder()
                .WithSelectMenu(menu2)
                .WithSelectMenu(menu1);

            await context.Interaction.FollowupAsync(
                "🌟 Escolha seus bônus raciais:\n+2 em um atributo e +1 em outro (diferentes).",
                components: builder.Build(),
                ephemeral: true
            );
        }
    }
}
