using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public static class IdiomaHelpers
    {
        public static async Task<FichaPersonagem> ObterFichaValidaAsync(Guid fichaId, ulong userId, FichaService fichaService)
        {
            var ficha = await fichaService.ObterFichaPorIdAsync(fichaId);
            return ficha != null && ficha.JogadorId == userId ? ficha : null;
        }

        public static async Task<List<Idioma>> ObterIdiomasDisponiveisAsync(FichaPersonagem ficha, IdiomaService idiomaService)
        {
            await idiomaService.ObterFichaIdiomasAsync(ficha);
            var todosIdiomas = await idiomaService.ObterTodosIdiomasAsync();
            var conhecidos = ficha.Idiomas.Select(i => i.Id).ToHashSet();
            return todosIdiomas.Where(i => !conhecidos.Contains(i.Id)).ToList();
        }

        public static async Task<string> AdicionarIdiomasAsync(FichaPersonagem ficha, IEnumerable<string> idiomaIds, IdiomaService idiomaService)
        {
            var todos = await idiomaService.ObterTodosIdiomasAsync();
            var conhecidos = ficha.Idiomas.Select(i => i.Id).ToHashSet();
            var selecionados = todos.Where(i => idiomaIds.Contains(i.Id) && !conhecidos.Contains(i.Id)).ToList();

            if (!selecionados.Any())
                return null;

            await idiomaService.AdicionarIdiomasAsync(ficha.Id, selecionados);
            return string.Join(", ", selecionados.Select(i => i.Nome));
        }
    }

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

            var ficha = await IdiomaHelpers.ObterFichaValidaAsync(fichaId, Context.User.Id, _fichaService);
            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada ou acesso negado.", ephemeral: true);
                return;
            }

            var selectedValue = (Context.Interaction as SocketMessageComponent)?.Data.Values.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(selectedValue))
            {
                await RespondAsync("❌ Nenhum idioma selecionado.", ephemeral: true);
                return;
            }

            var idiomaNome = await IdiomaHelpers.AdicionarIdiomasAsync(ficha, new[] { selectedValue }, _idiomaService);
            if (idiomaNome == null)
            {
                await RespondAsync("⚠️ Você já conhece esse idioma. Escolha outro diferente.", ephemeral: true);
                return;
            }

            var placeholder = ficha.Idiomas.FirstOrDefault(i => i.Id == "adicional");
            if (placeholder != null)
            {
                ficha.Idiomas.Remove(placeholder);
                await _fichaService.AtualizarFichaAsync(ficha);
            }

            await RespondAsync($"🗣️ Você escolheu **{idiomaNome}** como idioma adicional para **{ficha.Nome}**!", ephemeral: true);
        }

        [SlashCommand("adicionar_idiomas", "Adiciona idiomas extras à sua ficha")]
        public async Task AdicionarIdiomasAsync(
            [Summary("personagem", "Nome do personagem")] string nomePersonagem)
        {
            var userId = Context.User.Id;
            var ficha = await _fichaService.ObterFichaPorJogadorENomeAsync(userId, nomePersonagem);

            if (ficha == null)
            {
                await RespondAsync($"❌ Nenhuma ficha chamada **{nomePersonagem}** encontrada.", ephemeral: true);
                return;
            }

            var disponiveis = await IdiomaHelpers.ObterIdiomasDisponiveisAsync(ficha, _idiomaService);
            if (!disponiveis.Any())
            {
                await RespondAsync($"✅ A ficha **{ficha.Nome}** já conhece todos os idiomas disponíveis!", ephemeral: true);
                return;
            }

            var select = new SelectMenuBuilder()
                .WithCustomId($"select_idiomas_{ficha.Id}")
                .WithPlaceholder("Escolha os idiomas para adicionar")
                .WithMinValues(1)
                .WithMaxValues(Math.Min(5, disponiveis.Count));

            foreach (var idioma in disponiveis.Take(25))
            {
                SelectMenuHelper.AdicionarOpcaoSafe(select, idioma.Nome, idioma.Id, false, idioma.Descricao);
            }


            var builder = new ComponentBuilder().WithSelectMenu(select);

            await RespondAsync(
                $"🗣 **{ficha.Nome}**: escolha os idiomas para adicionar:",
                components: builder.Build(),
                ephemeral: true
            );
        }

        [ComponentInteraction("select_idiomas_*")]
        public async Task SelectIdiomasHandler(string fichaIdStr, string[] idiomaIds)
        {
            await DeferAsync(ephemeral: true);

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await FollowupAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var ficha = await IdiomaHelpers.ObterFichaValidaAsync(fichaId, Context.User.Id, _fichaService);
            if (ficha == null)
            {
                await FollowupAsync("❌ Ficha não encontrada ou acesso negado.", ephemeral: true);
                return;
            }

            var nomes = await IdiomaHelpers.AdicionarIdiomasAsync(ficha, idiomaIds, _idiomaService);
            if (nomes == null)
            {
                await FollowupAsync("⚠️ Todos os idiomas selecionados já estão presentes na ficha.", ephemeral: true);
                return;
            }

            await FollowupAsync($"✅ Adicionados **{nomes}** à ficha **{ficha.Nome}**!", ephemeral: true);
        }
    }
}
