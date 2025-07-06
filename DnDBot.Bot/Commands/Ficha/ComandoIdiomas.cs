using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo de interações relacionadas a idiomas da ficha.
    /// </summary>
    public class ComandoIdiomas : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly IdiomaService _idiomaService;

        public ComandoIdiomas(FichaService fichaService, IdiomaService idiomaService)
        {
            _fichaService = fichaService;
            _idiomaService = idiomaService;
        }


        [ComponentInteraction("*_idioma_adicional_*")]
        public async Task SelectIdiomaAdicionalHandler(string fichaIdStr, string indexStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await RespondAsync("❌ Ficha não encontrada ou acesso negado.", ephemeral: true);
                return;
            }

            // Obtém a escolha do usuário
            var selectedValue = (Context.Interaction as SocketMessageComponent)?.Data.Values.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(selectedValue))
            {
                await RespondAsync("❌ Nenhum idioma selecionado.", ephemeral: true);
                return;
            }

            await _idiomaService.ObterFichaIdiomasAsync(ficha);
            var todosIdiomas = await _idiomaService.ObterTodosIdiomasAsync();

            var idiomaEscolhido = todosIdiomas.FirstOrDefault(i => i.Id == selectedValue);
            if (idiomaEscolhido == null)
            {
                await RespondAsync("❌ Idioma selecionado não encontrado.", ephemeral: true);
                return;
            }

            if (ficha.Idiomas.Any(i => i.Id == idiomaEscolhido.Id && i.Id != "adicional"))
            {
                await RespondAsync("⚠️ Você já conhece esse idioma. Escolha outro diferente.", ephemeral: true);
                return;
            }

            var placeholder = ficha.Idiomas.FirstOrDefault(i => i.Id == "adicional");
            if (placeholder != null)
                ficha.Idiomas.Remove(placeholder);

            ficha.Idiomas.Add(idiomaEscolhido);
            await _fichaService.AtualizarFichaAsync(ficha);

            await RespondAsync($"🗣️ Você escolheu **{idiomaEscolhido.Nome}** como idioma adicional para **{ficha.Nome}**!", ephemeral: true);
        }

    }
}
