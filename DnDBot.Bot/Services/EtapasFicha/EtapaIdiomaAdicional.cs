using Discord;
using Discord.Interactions;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class EtapaIdiomaAdicional : IEtapaFicha
    {
        private readonly IdiomaService _idiomaService;

        public EtapaIdiomaAdicional(IdiomaService idiomaService)
        {
            _idiomaService = idiomaService;
        }

        public Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
        {
            // Completo se não tiver idiomas "adicional" pendentes
            bool completa = !ficha.Idiomas.Select(x => x.Idioma).Any(i => i.Id == "adicional");
            return Task.FromResult(completa);
        }

        public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            ficha.EtapaAtual = EtapaCriacaoFicha.Idiomas;
            int qtdAdicionais = ficha.Idiomas.Select(x => x.Idioma).Count(i => i.Id == "adicional");
            if (qtdAdicionais == 0)
                return;

            var todosIdiomas = await _idiomaService.ObterTodosIdiomasAsync();
            var conhecidos = ficha.Idiomas.Select(x => x.Idioma).Where(i => i.Id != "adicional").Select(i => i.Id).ToHashSet();

            var disponiveis = todosIdiomas
                .Where(i => i.Id != "adicional" && !conhecidos.Contains(i.Id))
                .ToList();

            var builder = new ComponentBuilder();

            for (int i = 0; i < qtdAdicionais; i++)
            {
                var menu = new SelectMenuBuilder()
                    .WithCustomId($"{ficha.Id}_idioma_adicional_{i + 1}")
                    .WithPlaceholder($"Escolha o idioma adicional {i + 1}")
                    .WithMinValues(1)
                    .WithMaxValues(1);

                foreach (var idioma in disponiveis.Take(25))
                {
                    menu.AddOption(idioma.Nome, idioma.Id);
                }

                builder.WithSelectMenu(menu);
            }

            await context.Interaction.FollowupAsync(
                text: $"🌐 Você tem **{qtdAdicionais} idioma(s) adicional(is)** para escolher:",
                components: builder.Build(),
                ephemeral: true
            );
        }
    }
}
