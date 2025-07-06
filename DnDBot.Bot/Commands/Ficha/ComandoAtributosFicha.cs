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
    public class ComandoAtributosFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly DistribuicaoAtributosService _atributosService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;
        private readonly IdiomaService _idiomaService;

        public ComandoAtributosFicha(
            FichaService fichaService,
            DistribuicaoAtributosService atributosService,
            DistribuicaoAtributosHandler atributosHandler,
            IdiomaService idiomaService)
        {
            _fichaService = fichaService;
            _atributosService = atributosService;
            _atributosHandler = atributosHandler;
            _idiomaService = idiomaService;
        }

        [SlashCommand("ficha_atributos", "Escolha uma ficha para distribuir os atributos")]
        public async Task FichaAtributosCommand()
        {
            Console.WriteLine($"[LOG] Slash command /ficha_atributos acionado por {Context.User.Id}");

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);

            if (fichas == null || !fichas.Any())
            {
                Console.WriteLine($"[LOG] Nenhuma ficha encontrada para jogador {Context.User.Id}");
                await RespondAsync("❌ Você ainda não tem fichas criadas. Use /ficha_criar primeiro.", ephemeral: true);
                return;
            }

            Console.WriteLine($"[LOG] {fichas.Count()} fichas encontradas para jogador {Context.User.Id}");

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

        [ComponentInteraction("select_ficha_atributo")]
        public async Task SelectFichaAtributoHandler(string fichaIdStr)
        {
            Console.WriteLine($"[LOG] Ficha selecionada para distribuição: {fichaIdStr} por {Context.User.Id}");

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                Console.WriteLine($"[ERRO] ID de ficha inválido recebido: {fichaIdStr}");
                await RespondAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);
            var ficha = fichas.FirstOrDefault(f => f.Id == fichaId);

            if (ficha == null)
            {
                Console.WriteLine($"[ERRO] Ficha com ID {fichaId} não encontrada para jogador {Context.User.Id}");
                await RespondAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            _atributosHandler.InicializarDistribuicao(dist, ficha);

            Console.WriteLine($"[LOG] Interface de distribuição inicializada para ficha '{ficha.Nome}' ({ficha.Id})");

            var embed = _atributosHandler.ConstruirEmbedDistribuicao(dist);
            var components = _atributosHandler.ConstruirComponentesDistribuicao(dist, ficha.Id);

            await RespondAsync($"Distribua seus pontos de atributo na ficha **{ficha.Nome}**:", embed: embed, components: components, ephemeral: true);
        }

        [ComponentInteraction("atributo_*_*_*")]
        public async Task AtributoHandler(string direcao, string atributo, string fichaIdStr)
        {
            Console.WriteLine($"[LOG] Botão clicado: {direcao} {atributo}_{fichaIdStr} por usuário {Context.User.Id}");

            await DeferAsync(ephemeral: true);  // Evita timeout

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                Console.WriteLine($"[ERRO] fichaId '{fichaIdStr}' inválido.");
                await FollowupAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);
            var ficha = fichas.FirstOrDefault(f => f.Id == fichaId);
            if (ficha == null)
            {
                Console.WriteLine($"[ERRO] Ficha {fichaId} não encontrada.");
                await FollowupAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            int delta = direcao == "mais" ? +1 : -1;
            Console.WriteLine($"[LOG] Ajustando atributo {atributo} em {delta} ponto(s) na ficha '{ficha.Nome}' ({ficha.Id})");

            bool alterou = _atributosHandler.TentarAjustarAtributo(Context.User.Id, ficha.Id, atributo, delta);
            if (!alterou)
            {
                Console.WriteLine($"[ERRO] Ajuste inválido para {atributo} em ficha {ficha.Id}");
                await FollowupAsync("❌ Não foi possível alterar o atributo.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            var embed = _atributosHandler.ConstruirEmbedDistribuicao(dist);
            var components = _atributosHandler.ConstruirComponentesDistribuicao(dist, ficha.Id);

            await Context.Interaction.ModifyOriginalResponseAsync(msg =>
            {
                msg.Embed = embed;
                msg.Components = components;
            });

            Console.WriteLine($"[LOG] Atributo {atributo} alterado com sucesso. Atualizando mensagem.");
        }


        // Botão para concluir a distribuição
        [ComponentInteraction("concluir_distribuicao_*")]
        public async Task ConcluirDistribuicao(string fichaIdStr)
        {
            Console.WriteLine($"[LOG] Botão concluir_distribuicao_{fichaIdStr} clicado por usuário {Context.User.Id}");

            await DeferAsync(ephemeral: true); // evita timeout

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await FollowupAsync("❌ ID da ficha inválido.", ephemeral: true);
                return;
            }

            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);
            var ficha = fichas.FirstOrDefault(f => f.Id == fichaId);

            if (ficha == null)
            {
                await FollowupAsync("❌ Ficha não encontrada para salvar atributos.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);

            if (dist.PontosUsados > dist.PontosDisponiveis)
            {
                await FollowupAsync("❌ Você usou mais pontos do que o permitido.", ephemeral: true);
                return;
            }

            // Atualiza atributos
            ficha.Forca = dist.Atributos["Forca"];
            ficha.Destreza = dist.Atributos["Destreza"];
            ficha.Constituicao = dist.Atributos["Constituicao"];
            ficha.Inteligencia = dist.Atributos["Inteligencia"];
            ficha.Sabedoria = dist.Atributos["Sabedoria"];
            ficha.Carisma = dist.Atributos["Carisma"];

            await _fichaService.AtualizarFichaAsync(ficha);
            _atributosHandler.RemoverDistribuicao(Context.User.Id, ficha.Id);

            // Atualiza a mensagem original com os atributos finais
            var embedFinal = _atributosHandler.ConstruirEmbedDistribuicao(dist);
            await Context.Interaction.ModifyOriginalResponseAsync(msg =>
            {
                msg.Content = $"✅ Distribuição finalizada para a ficha **{ficha.Nome}**!";
                msg.Embed = embedFinal;
                msg.Components = new ComponentBuilder().Build(); // remove botões
            });

            // Verifica idiomas adicionais
            await _idiomaService.ObterFichaIdiomasAsync(ficha);
            int qtdAdicionais = ficha.Idiomas.Count(i => i.Id == "adicional");

            if (qtdAdicionais > 0)
            {
                var todosIdiomas = await _idiomaService.ObterTodosIdiomasAsync();
                var conhecidos = ficha.Idiomas.Where(i => i.Id != "adicional").Select(i => i.Id).ToHashSet();

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

                await FollowupAsync(
                    text: $"🌐 Você tem **{qtdAdicionais} idioma(s) adicional(is)** para escolher:",
                    components: builder.Build(),
                    ephemeral: true
                );
            }
        }


    }
}
