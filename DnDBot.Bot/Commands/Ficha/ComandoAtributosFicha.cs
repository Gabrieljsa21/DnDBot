using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public class ComandoAtributosFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly DistribuicaoAtributosService _atributosService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;

        public ComandoAtributosFicha(
            FichaService fichaService,
            DistribuicaoAtributosService atributosService,
            DistribuicaoAtributosHandler atributosHandler)
        {
            _fichaService = fichaService;
            _atributosService = atributosService;
            _atributosHandler = atributosHandler;
        }

        // /ficha_atributos - lista as fichas e permite escolher
        [SlashCommand("ficha_atributos", "Escolha uma ficha para distribuir os atributos")]
        public async Task FichaAtributosCommand()
        {
            var fichas = _fichaService.ObterFichasPorJogador(Context.User.Id);

            if (fichas == null || !fichas.Any())
            {
                await RespondAsync("❌ Você ainda não tem fichas criadas. Use /ficha_criar primeiro.", ephemeral: true);
                return;
            }

            var options = fichas.Select(f => new SelectMenuOptionBuilder
            {
                Label = f.Nome,
                Value = f.Id.ToString()
            }).ToList();

            var menu = new SelectMenuBuilder()
                .WithPlaceholder("Escolha uma ficha")
                .WithCustomId("select_ficha_atributo")
                .WithOptions(options);

            var builder = new ComponentBuilder().WithSelectMenu(menu);

            await RespondAsync("Selecione a ficha para distribuir atributos:", components: builder.Build(), ephemeral: true);
        }

        // Quando o jogador escolhe a ficha
        [ComponentInteraction("select_ficha_atributo")]
        public async Task SelectFichaAtributoHandler(string fichaIdStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var ficha = _fichaService.ObterFichasPorJogador(Context.User.Id)
                .FirstOrDefault(f => f.Id == fichaId);

            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            _atributosHandler.InicializarDistribuicao(dist, ficha);

            var embed = _atributosHandler.ConstruirEmbedDistribuicao(dist);
            var components = _atributosHandler.ConstruirComponentesDistribuicao(dist);

            await RespondAsync($"Distribua seus pontos de atributo na ficha **{ficha.Nome}**:", embed: embed, components: components, ephemeral: true);
        }

        // Handler genérico para os botões + e -
        [ComponentInteraction("atributo_*_*")]
        public async Task AtributoHandler(string direcao, string atributo)
        {
            Console.WriteLine($"Botão clicado: {direcao} {atributo}");

            await DeferAsync(ephemeral: true);  // Deferir a resposta (evita erro de timeout/expiração)

            var fichas = _fichaService.ObterFichasPorJogador(Context.User.Id);
            var ficha = fichas.OrderByDescending(f => f.DataAlteracao).FirstOrDefault();

            if (ficha == null)
            {
                await FollowupAsync("❌ Nenhuma ficha encontrada para distribuir atributos.", ephemeral: true);
                return;
            }

            int delta = direcao == "mais" ? +1 : -1;

            bool alterou = _atributosHandler.TentarAjustarAtributo(Context.User.Id, ficha.Id, atributo, delta);
            if (!alterou)
            {
                await FollowupAsync("❌ Não foi possível alterar o atributo.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            var embed = _atributosHandler.ConstruirEmbedDistribuicao(dist);
            var components = _atributosHandler.ConstruirComponentesDistribuicao(dist);

            // Agora modifique a resposta original deferida
            await Context.Interaction.ModifyOriginalResponseAsync(msg =>
            {
                msg.Embed = embed;
                msg.Components = components;
            });
        }

    }
}
