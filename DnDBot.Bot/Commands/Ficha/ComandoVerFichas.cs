using Discord;
using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável pelo comando /ficha_ver que exibe fichas salvas do usuário.
    /// </summary>
    public class ComandoVerFichas : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;

        /// <summary>
        /// Construtor com injeção do serviço de fichas.
        /// </summary>
        public ComandoVerFichas(FichaService fichaService)
        {
            _fichaService = fichaService;
        }

        /// <summary>
        /// Comando que exibe todas as fichas criadas pelo usuário atual.
        /// </summary>
        [SlashCommand("ficha_ver", "Mostra suas fichas de personagem")]
        public async Task VerFichasAsync()
        {
            var fichas = _fichaService.ObterFichasPorJogador(Context.User.Id);

            if (fichas == null || fichas.Count == 0)
            {
                await RespondAsync("❌ Você não tem nenhuma ficha criada.", ephemeral: true);
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle($"📘 Fichas de {Context.User.Username}")
                .WithColor(Color.Blue);

            foreach (var ficha in fichas)
            {
                embedBuilder.AddField(
                    ficha.Nome,
                    $"Raça: {ficha.Raca}\nClasse: {ficha.Classe}\nAntecedente: {ficha.Antecedente}\nAlinhamento: {ficha.Alinhamento}",
                    false);
            }

            await RespondAsync(embed: embedBuilder.Build(), ephemeral: true);
        }
    }
}
