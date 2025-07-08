using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo de comandos para interagir com as classes de personagens.
    /// </summary>
    public class ClasseCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly ClassesService _classesService;

        /// <summary>
        /// Construtor que recebe o serviço de classes via injeção de dependência.
        /// </summary>
        /// <param name="classesService">Serviço para acessar as classes.</param>
        public ClasseCommands(ClassesService classesService)
        {
            _classesService = classesService;
        }

        /// <summary>
        /// Comando slash para listar as classes disponíveis para criação de personagens.
        /// </summary>
        [SlashCommand("listar_classes", "Exibe a lista de classes disponíveis para criação de personagens.")]
        public async Task ListarClasses()
        {
            var classes = await _classesService.ObterClassesAsync();

            if (!classes.Any())
            {
                await RespondAsync("❌ Nenhuma classe encontrada no banco de dados.");
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle("📚 Classes disponíveis")
                .WithColor(Color.DarkRed);

            foreach (var classe in classes.Take(25))
            {
                var descricao = classe.Descricao.Length > 150
                    ? classe.Descricao[..150] + "..."
                    : classe.Descricao;
                embedBuilder.AddField($"🛡️ {classe.Nome}", descricao, inline: false);
            }

            await RespondAsync(embed: embedBuilder.Build());
        }
    }
}
