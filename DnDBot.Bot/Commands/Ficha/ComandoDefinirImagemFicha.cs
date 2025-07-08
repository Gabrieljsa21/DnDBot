using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável pelo comando /ficha_ver_todas que exibe fichas salvas do usuário.
    /// </summary>
    public class ComandoDefinirImagemFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;

        /// <summary>
        /// Construtor com injeção do serviço de fichas.
        /// </summary>
        public ComandoDefinirImagemFicha(
            FichaService fichaService
        )
        {
            _fichaService = fichaService;
        }


        [SlashCommand("ficha_imagem", "Define a imagem da ficha pelo link")]
        public async Task DefinirImagemFichaAsync(string nomeFicha, string urlImagem)
        {
            var ficha = await _fichaService.ObterFichaPorJogadorENomeAsync(Context.User.Id, nomeFicha);
            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            if (!Uri.TryCreate(urlImagem, UriKind.Absolute, out var uriResult) ||
                !(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                await RespondAsync("❌ URL inválida.", ephemeral: true);
                return;
            }

            string[] extensoesValidas = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if (!extensoesValidas.Any(e => uriResult.AbsolutePath.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
            {
                await RespondAsync("❌ A URL deve apontar para uma imagem válida (.jpg, .png, etc).", ephemeral: true);
                return;
            }

            ficha.ImagemUrl = urlImagem;
            await _fichaService.AtualizarFichaAsync(ficha);

            await RespondAsync($"✅ Imagem da ficha '{ficha.Nome}' atualizada com sucesso!", ephemeral: true);
        }


    }
}
