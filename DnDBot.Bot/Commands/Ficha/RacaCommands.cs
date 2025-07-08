using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public class RacaCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly RacasService _racasService;

        public RacaCommands(RacasService racasService)
        {
            _racasService = racasService;
        }

        [SlashCommand("listar_racas", "Exibe a lista de raças disponíveis para criação de personagens.")]
        public async Task ListarRacas()
        {
            var racas = await _racasService.ObterRacasAsync();

            if (!racas.Any())
            {
                await RespondAsync("❌ Nenhuma raça encontrada no banco de dados.");
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle("📜 Raças disponíveis")
                .WithColor(Color.Blue);

            foreach (var raca in racas.Take(25)) // Discord permite até 25 fields por embed
            {
                var subracas = string.Join(", ", raca.SubRaca?.Select(sr => sr.Nome) ?? new string[0]);
                var descricao = raca.Descricao.Length > 150 ? raca.Descricao[..150] + "..." : raca.Descricao;

                embedBuilder.AddField($"🧬 {raca.Nome}", $"{descricao}\n**Sub-raças:** {subracas}", inline: false);
            }

            await RespondAsync(embed: embedBuilder.Build());
        }
    }
}
