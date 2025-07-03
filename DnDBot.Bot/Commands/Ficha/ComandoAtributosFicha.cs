using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Distribuicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo de comandos para gerenciar a distribuição de atributos em fichas de personagem.
    /// </summary>
    public class ComandoAtributosFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly DistribuicaoAtributosService _atributosService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;

        /// <summary>
        /// Construtor que recebe os serviços necessários via injeção de dependência.
        /// </summary>
        /// <param name="fichaService">Serviço para manipulação das fichas.</param>
        /// <param name="atributosService">Serviço para manipulação da distribuição de atributos.</param>
        /// <param name="atributosHandler">Handler para gerenciamento da UI e lógica de distribuição de atributos.</param>
        public ComandoAtributosFicha(
            FichaService fichaService,
            DistribuicaoAtributosService atributosService,
            DistribuicaoAtributosHandler atributosHandler)
        {
            _fichaService = fichaService;
            _atributosService = atributosService;
            _atributosHandler = atributosHandler;
        }

        /// <summary>
        /// Comando Slash que lista as fichas do usuário para que ele escolha uma para distribuir atributos.
        /// </summary>
        [SlashCommand("ficha_atributos", "Escolha uma ficha para distribuir os atributos")]
        public async Task FichaAtributosCommand()
        {
            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);

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

        /// <summary>
        /// Handler chamado quando o usuário seleciona uma ficha para distribuição de atributos.
        /// Inicializa a distribuição e exibe a interface para ajuste.
        /// </summary>
        /// <param name="fichaIdStr">ID da ficha selecionada, como string.</param>
        [ComponentInteraction("select_ficha_atributo")]
        public async Task SelectFichaAtributoHandler(string fichaIdStr)
        {
            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await RespondAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);
            var ficha = fichas.FirstOrDefault(f => f.Id == fichaId);

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

        /// <summary>
        /// Handler genérico para os botões de ajuste de atributos (incrementar/decrementar).
        /// Espera um customId no formato "atributo_direcao_atributo" (ex: atributo_mais_Forca).
        /// </summary>
        /// <param name="direcao">Direção do ajuste: "mais" ou "menos".</param>
        /// <param name="atributo">Nome do atributo a ser ajustado.</param>
        [ComponentInteraction("atributo_*_*")]
        public async Task AtributoHandler(string direcao, string atributo)
        {
            Console.WriteLine($"Botão clicado: {direcao} {atributo}");

            await DeferAsync(ephemeral: true);  // Evita timeout da interação

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);
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

            // Atualiza a mensagem original com o embed e componentes atualizados
            await Context.Interaction.ModifyOriginalResponseAsync(msg =>
            {
                msg.Embed = embed;
                msg.Components = components;
            });
        }
    }
}
