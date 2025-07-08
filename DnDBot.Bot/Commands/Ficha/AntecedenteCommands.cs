using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services.Antecedentes;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo de comandos para interagir com os antecedentes de personagens.
    /// </summary>
    public class AntecedenteCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly AntecedentesService _antecedentesService;

        /// <summary>
        /// Construtor que recebe o serviço de antecedentes via injeção de dependência.
        /// </summary>
        /// <param name="antecedentesService">Serviço para acessar os antecedentes.</param>
        public AntecedenteCommands(AntecedentesService antecedentesService)
        {
            _antecedentesService = antecedentesService;
        }

        /// <summary>
        /// Comando slash para listar os antecedentes disponíveis para personagens.
        /// </summary>
        [SlashCommand("listar_antecedentes", "Exibe os antecedentes disponíveis para personagens.")]
        public async Task ListarAntecedentes()
        {
            var antecedentes = await _antecedentesService.ObterAntecedentesAsync();

            if (!antecedentes.Any())
            {
                await RespondAsync("❌ Nenhum antecedente encontrado no banco de dados.");
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle("📖 Antecedentes disponíveis")
                .WithColor(Color.DarkGreen);

            foreach (var antecedente in antecedentes.Take(25))
            {
                var descricao = antecedente.Descricao.Length > 150
                    ? antecedente.Descricao[..150] + "..."
                    : antecedente.Descricao;
                embedBuilder.AddField($"📜 {antecedente.Nome}", descricao, inline: false);
            }

            await RespondAsync(embed: embedBuilder.Build());
        }
    }
}
