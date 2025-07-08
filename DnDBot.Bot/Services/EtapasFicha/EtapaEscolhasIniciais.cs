using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class EtapaEscolhasIniciais : IEtapaFicha
    {
        private readonly RacasService _racasService;
        private readonly ClassesService _classesService;
        private readonly AntecedentesService _antecedentesService;
        private readonly AlinhamentosService _alinhamentosService;

        public EtapaEscolhasIniciais(
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService)
        {
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
        }

        public Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
        {
            // Usa o validador central do controlador para evitar duplicação
            return Task.FromResult(ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha));
        }

        public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            ficha.EtapaAtual = EtapaCriacaoFicha.RacaClasseAntecedenteAlinhamento;

            var racas = await _racasService.ObterRacasAsync();
            var classes = await _classesService.ObterClassesAsync();
            var antecedentes = await _antecedentesService.ObterAntecedentesAsync();
            var alinhamentos = await _alinhamentosService.ObterAlinhamentosAsync();

            var componentesBuilder = new ComponentBuilder()
                .WithSelectMenu(SelectMenuHelper.CriarSelectRaca(racas))
                .WithSelectMenu(SelectMenuHelper.CriarSelectClasse(classes))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAntecedente(antecedentes))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAlinhamento(alinhamentos));

            await context.Interaction.FollowupAsync(
                text: "Agora escolha os demais detalhes do personagem:",
                components: componentesBuilder.Build(),
                ephemeral: true
            );
        }
    }

}
