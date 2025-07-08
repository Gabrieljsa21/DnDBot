using Discord;
using Discord.Interactions;
using DnDBot.Bot.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public class ComandoExcluirFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;

        public ComandoExcluirFicha(FichaService fichaService)
        {
            _fichaService = fichaService;
        }

        [SlashCommand("ficha_excluir", "Exclui uma ficha do seu personagem")]
        public async Task ExcluirFichaAsync()
        {
            var fichas = await _fichaService.ObterFichasPorJogadorAsync(Context.User.Id);

            if (fichas == null || fichas.Count == 0)
            {
                await RespondAsync("❌ Você não tem nenhuma ficha para excluir.", ephemeral: true);
                return;
            }

            var menu = new SelectMenuBuilder()
                .WithCustomId("dropdown_ficha_excluir")
                .WithPlaceholder("Selecione a ficha que deseja excluir");

            foreach (var ficha in fichas)
            {
                menu.AddOption(ficha.Nome, ficha.Id.ToString());
            }

            var builder = new ComponentBuilder().WithSelectMenu(menu);

            await RespondAsync("⚠️ Escolha a ficha que deseja **excluir**:", components: builder.Build(), ephemeral: true);
        }

        [ComponentInteraction("dropdown_ficha_excluir")]
        public async Task DropdownExcluirSelecionada(string fichaIdStr)
        {
            await DeferAsync(ephemeral: true);

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await FollowupAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                await FollowupAsync("❌ Ficha não encontrada ou acesso negado.", ephemeral: true);
                return;
            }

            Console.WriteLine($"[EXCLUSÃO] Usuário {Context.User.Username} solicitou exclusão da ficha '{ficha.Nome}' ({ficha.Id})");

            var embed = new EmbedBuilder()
                .WithTitle($"🗑️ Excluir Ficha: {ficha.Nome}")
                .WithColor(Color.Red)
                .AddField("Raça", ficha.RacaId ?? "Não definida", true)
                .AddField("Classe", ficha.ClasseId ?? "Não definida", true)
                .AddField("Antecedente", ficha.AntecedenteId ?? "Não definido", true)
                .AddField("Criado em", ficha.CriadoEm.ToLocalTime().ToString("dd/MM/yyyy HH:mm"), false)
                .WithFooter("Esta ação não pode ser desfeita!");

            var builder = new ComponentBuilder()
                .WithButton("❌ Confirmar exclusão", $"btn_excluir_ficha_{fichaId}", ButtonStyle.Danger);

            await FollowupAsync(embed: embed.Build(), components: builder.Build(), ephemeral: true);
        }

        [ComponentInteraction("btn_excluir_ficha_*")]
        public async Task ConfirmarExclusaoHandler(string fichaIdStr)
        {
            await DeferAsync(ephemeral: true);

            if (!Guid.TryParse(fichaIdStr, out var fichaId))
            {
                await FollowupAsync("❌ ID inválido.", ephemeral: true);
                return;
            }

            var ficha = await _fichaService.ObterFichaPorIdAsync(fichaId);
            if (ficha == null || ficha.JogadorId != Context.User.Id)
            {
                Console.WriteLine($"[EXCLUSÃO] Falha: Ficha {fichaId} não encontrada para exclusão.");
                await FollowupAsync("❌ Ficha não encontrada ou você não tem permissão para excluí-la.", ephemeral: true);
                return;
            }

            var sucesso = await _fichaService.ExcluirFichaAsync(fichaId);

            if (sucesso)
            {
                Console.WriteLine($"[EXCLUSÃO] Ficha '{ficha.Nome}' ({fichaId}) excluída com sucesso.");

                // Atualiza a mensagem original de confirmação
                await ModifyOriginalResponseAsync(msg =>
                {
                    msg.Content = $"✅ A ficha **{ficha.Nome}** foi excluída com sucesso!";
                    msg.Embed = null;
                    msg.Components = new ComponentBuilder().Build(); // remove botões
                });
            }
            else
            {
                Console.WriteLine($"[EXCLUSÃO] Erro ao tentar excluir ficha {fichaId}.");

                await ModifyOriginalResponseAsync(msg =>
                {
                    msg.Content = "❌ Ocorreu um erro ao tentar excluir a ficha.";
                    msg.Embed = null;
                    msg.Components = new ComponentBuilder().Build(); // remove botões
                });
            }
        }

    }
}
