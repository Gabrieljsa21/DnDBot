using Discord.Interactions;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Distribuicao;
using DnDBot.Bot.Services.EtapasFicha;
using System.Threading.Tasks;

public class EtapaDistribuicaoAtributos : IEtapaFicha
{
    private readonly DistribuicaoAtributosHandler _distHandler;
    private readonly FichaService _fichaService;

    public EtapaDistribuicaoAtributos(
        DistribuicaoAtributosHandler distHandler,
        FichaService fichaService)
    {
        _distHandler = distHandler;
        _fichaService = fichaService;
    }

    public Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
    {
        // Se os atributos já foram gravados na ficha, considera completa:
        bool atributosGravados =
            ficha.Forca > 0 &&
            ficha.Destreza > 0 &&
            ficha.Constituicao > 0 &&
            ficha.Inteligencia > 0 &&
            ficha.Sabedoria > 0 &&
            ficha.Carisma > 0;

        if (atributosGravados)
            return Task.FromResult(true);

        // Senão, volta ao critério original de ter usado todos os pontos
        var dist = _distHandler.ObterDistribuicao(ficha.JogadorId, ficha.Id);
        bool distribuiuTudo = dist != null && dist.PontosUsados == dist.PontosDisponiveis;
        return Task.FromResult(distribuiuTudo);
    }

    public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
    {
        if (await EstaCompletaAsync(ficha))
            return; // não reexibe se já salvou os atributos

        // (restante idêntico ao que você já tinha)
        ficha.EtapaAtual = EtapaCriacaoFicha.DistribuicaoAtributos;
        await _fichaService.AtualizarFichaAsync(ficha);

        var dist = _distHandler.ObterDistribuicao(context.User.Id, ficha.Id);
        _distHandler.InicializarDistribuicao(dist, ficha);

        var embed = _distHandler.ConstruirEmbedDistribuicao(dist, ficha);
        var components = _distHandler.ConstruirComponentesDistribuicao(dist, ficha.Id);

        if (!context.Interaction.HasResponded)
            await context.Interaction.RespondAsync(
                $"⚙️ **Distribuição de atributos — {ficha.Nome}**",
                embed: embed,
                components: components,
                ephemeral: true
            );
        else
            await context.Interaction.FollowupAsync(
                $"⚙️ **Distribuição de atributos — {ficha.Nome}**",
                embed: embed,
                components: components,
                ephemeral: true
            );
    }
}
