using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Bot.Helpers;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services.EtapasFicha
{
    public class EtapaFinalizacaoFicha : IEtapaFicha
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly ClassesService _classesService;
        private readonly AntecedentesService _antecedentesService;
        private readonly AlinhamentosService _alinhamentosService;

        public EtapaFinalizacaoFicha(
            FichaService fichaService,
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService)
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
        }

        public Task<bool> EstaCompletaAsync(FichaPersonagem ficha)
        {
            if (ficha == null) return Task.FromResult(false);

            if (string.IsNullOrWhiteSpace(ficha.Nome)) return Task.FromResult(false);

            string[] invalidos = { "Não definida", "Não definido", null, "" };
            if (invalidos.Contains(ficha.RacaId)) return Task.FromResult(false);
            if (invalidos.Contains(ficha.ClasseId)) return Task.FromResult(false);
            if (invalidos.Contains(ficha.AntecedenteId)) return Task.FromResult(false);
            if (invalidos.Contains(ficha.AlinhamentoId)) return Task.FromResult(false);

            return Task.FromResult(true);
        }

        public async Task ExecutarAsync(FichaPersonagem ficha, SocketInteractionContext context, bool usarFollowUp = false)
        {
            if (!ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha))
                return;

            // Busca a ficha existente no banco
            var fichaExistente = await _fichaService.ObterFichaPorIdAsync(ficha.Id);

            if (fichaExistente != null)
            {
                // Atualiza apenas os campos necessários
                fichaExistente.RacaId = ficha.RacaId;
                fichaExistente.ClasseId = ficha.ClasseId;
                fichaExistente.AntecedenteId = ficha.AntecedenteId;
                fichaExistente.AlinhamentoId = ficha.AlinhamentoId;

                await _fichaService.SalvarAlteracoesAsync(); // Crie esse método se ainda não existir
            }
            else
            {
                // Se ainda não foi salva no banco, adiciona a ficha
                await _fichaService.AdicionarFichaAsync(ficha);
            }

            // Resumo da ficha
            string nomeRaca = (await _racasService.ObterRacaPorIdAsync(ficha.RacaId))?.Nome ?? ficha.RacaId;
            string nomeClasse = (await _classesService.ObterClassePorIdAsync(ficha.ClasseId))?.Nome ?? ficha.ClasseId;
            string nomeAntecedente = (await _antecedentesService.ObterAntecedentePorIdAsync(ficha.AntecedenteId))?.Nome ?? ficha.AntecedenteId;
            string nomeAlinhamento = (await _alinhamentosService.ObterAlinhamentoPorIdAsync(ficha.AlinhamentoId))?.Nome ?? ficha.AlinhamentoId;

            string resumo =
                $"**Nome:** {ficha.Nome}\n" +
                $"**Raça:** {nomeRaca}\n" +
                $"**Classe:** {nomeClasse}\n" +
                $"**Antecedente:** {nomeAntecedente}\n" +
                $"**Alinhamento:** {nomeAlinhamento}";

            if (!context.Interaction.HasResponded)
            {
                await context.Interaction.RespondAsync($"✅ Ficha do personagem finalizada com sucesso!\n\n{resumo}", ephemeral: true);
            }
            else
            {
                await context.Interaction.FollowupAsync($"✅ Ficha do personagem finalizada com sucesso!\n\n{resumo}", ephemeral: true);
            }
        }




    }
}
