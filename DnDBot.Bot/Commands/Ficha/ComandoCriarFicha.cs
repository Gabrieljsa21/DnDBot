using Discord;
using Discord.Interactions;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Services;
using DnDBot.Bot.Services.Antecedentes;
using DnDBot.Bot.Services.Distribuicao;
using DnDBot.Bot.Services.EtapasFicha;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    public class ComandoCriarFicha : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly ClassesService _classesService;
        private readonly AntecedentesService _antecedentesService;
        private readonly AlinhamentosService _alinhamentosService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;
        private readonly ControladorEtapasFicha _controladorEtapasFicha;
        private readonly IdiomaService _idiomaService;
        private readonly ResistenciaService _resistenciaService;
        private readonly CaracteristicaService _caracteristicaService;
        private readonly ProficienciaService _proficienciaService;
        private readonly MagiaService _magiaService;

        public ComandoCriarFicha(
            FichaService fichaService,
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService,
            DistribuicaoAtributosHandler atributosHandler,
            ControladorEtapasFicha controladorEtapasFicha,
            IdiomaService idiomaService,
            ResistenciaService resistenciaService,
            CaracteristicaService caracteristicaService,
            ProficienciaService proficienciaService,
            MagiaService magiaService
            )
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
            _atributosHandler = atributosHandler;
            _controladorEtapasFicha = controladorEtapasFicha;
            _idiomaService = idiomaService;
            _resistenciaService = resistenciaService;
            _caracteristicaService = caracteristicaService;
            _proficienciaService = proficienciaService;
            _magiaService = magiaService;
        }

        /// <summary>
        /// Comando inicial que exibe o modal para inserir o nome do personagem.
        /// </summary>
        [SlashCommand("ficha_criar", "Cria uma ficha com o nome especificado")]
        public async Task CriarFichaAsync(
            [Summary("nome", "Nome do personagem")] string nomePersonagem)
        {
            var usuarioId = Context.User.Id;
            FichaTempStore.RemoveFicha(Context.User.Id);

            // 1) Validação básica do nome
            if (string.IsNullOrWhiteSpace(nomePersonagem))
            {
                await RespondAsync("❌ Nome inválido.", ephemeral: true);
                return;
            }

            // 2) Verifica existência de ficha com mesmo nome
            var fichaExistente = await _fichaService.ObterFichaPorJogadorENomeAsync(usuarioId, nomePersonagem);
            if (fichaExistente != null)
            {
                await RespondAsync("❌ Você já possui uma ficha com esse nome. Escolha outro.", ephemeral: true);
                return;
            }

            // 3) Cria ou obtém a ficha temporária
            var ficha = FichaTempStore.GetOrCreateFicha(usuarioId);
            ficha.Nome = nomePersonagem;

            // 4) (Opcional) persistir imediatamente no banco ou apenas manter em memória
            var existente = await _fichaService.ObterFichaPorIdAsync(ficha.Id);
            if (existente == null)
            {
                await _fichaService.AdicionarFichaAsync(ficha);
            }
            else
            {
                await _fichaService.AtualizarFichaAsync(ficha);
            }


            // 5) Confirmação e disparo da próxima etapa
            await RespondAsync($"✅ Nome **{nomePersonagem}** registrado! Agora, prossiga para as próximas escolhas...", ephemeral: true);
            await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: true);
        }

        [ComponentInteraction("select_raca")]
        public async Task SelectRacaHandler(string valor)
        {
            await DeferAsync(ephemeral: true);

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            var raca = await _racasService.ObterRacaPorIdAsync(valor);
            if (raca == null)
            {
                await FollowupAsync("❌ Raça não encontrada.", ephemeral: true);
                return;
            }

            ficha.RacaId = valor;

            if (ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha))
            {
                var finalizado = await TentarFinalizarFichaAsync(ficha);
                if (finalizado)
                    await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: false);
            }
            else
            {
                // Apenas retorna; não responde para evitar erros de interação duplicada
                return;
            }
        }

        [ComponentInteraction("select_classe")]
        public async Task SelectClasseHandler(string valor)
        {
            await DeferAsync(ephemeral: true);

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            var classe = await _classesService.ObterClassePorIdAsync(valor);
            if (classe == null)
            {
                await FollowupAsync("❌ Classe não encontrada.", ephemeral: true);
                return;
            }

            ficha.ClasseId = valor;

            if (ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha))
            {
                var finalizado = await TentarFinalizarFichaAsync(ficha);
                if (finalizado)
                    await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: false);
            }
            else
            {
                return;
            }
        }

        [ComponentInteraction("select_antecedente")]
        public async Task SelectAntecedenteHandler(string valor)
        {
            await DeferAsync(ephemeral: true);

            var antecedente = await _antecedentesService.ObterAntecedentePorIdAsync(valor);
            if (antecedente == null)
            {
                await FollowupAsync("❌ Antecedente não encontrado.", ephemeral: true);
                return;
            }

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            ficha.AntecedenteId = valor;

            if (antecedente.Moedas != null)
            {
                foreach (var moeda in antecedente.Moedas)
                    ficha.BolsaDeMoedas.Adicionar(moeda.Moeda);
            }

            if (ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha))
            {
                var finalizado = await TentarFinalizarFichaAsync(ficha);
                if (finalizado)
                    await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: false);
            }
            else
            {
                return;
            }
        }

        [ComponentInteraction("select_alinhamento")]
        public async Task SelectAlinhamentoHandler(string valor)
        {
            await DeferAsync(ephemeral: true);

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            var alinhamento = await _alinhamentosService.ObterAlinhamentoPorIdAsync(valor);
            if (alinhamento == null)
            {
                await FollowupAsync("❌ Alinhamento não encontrado.", ephemeral: true);
                return;
            }

            ficha.AlinhamentoId = valor;

            if (ControladorEtapasFicha.ValidadorFicha.EstaCompleta(ficha))
            {
                var finalizado = await TentarFinalizarFichaAsync(ficha);
                if (finalizado)
                    await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: false);
            }
            else
            {
                return;
            }
        }

        [ComponentInteraction("select_subraca")]
        public async Task SelectSubracaHandler(string valor)
        {
            await DeferAsync(ephemeral: true);

            var ficha = FichaTempStore.GetFicha(Context.User.Id);
            if (ficha == null)
            {
                await FollowupAsync("❌ Ficha não encontrada.", ephemeral: true);
                return;
            }

            var raca = await _racasService.ObterRacaPorIdAsync(ficha.RacaId);
            var subraca = raca?.SubRaca?.FirstOrDefault(s => s.Id == valor);
            if (subraca == null)
            {
                await FollowupAsync("❌ Sub‑raça inválida para a raça escolhida.", ephemeral: true);
                return;
            }

            ficha.SubracaId = valor;
            //await _fichaService.AtualizarFichaAsync(ficha);

            if (subraca.Idiomas != null && subraca.Idiomas.Any())
            {
                var idiomasEntidades = subraca.Idiomas
                    .Select(sr => sr.Idioma)
                    .ToList();

                await _idiomaService.AdicionarIdiomasAsync(ficha.Id, idiomasEntidades);
            }

            if (subraca.Caracteristicas != null && subraca.Caracteristicas.Any())
            {
                var caracteristicas = subraca.Caracteristicas
                    .Select(sr => sr.Caracteristica)
                    .Where(c => c != null)
                    .ToList();

                await _caracteristicaService.AdicionarCaracteristicasAsync(ficha.Id, caracteristicas);
            }

            if (subraca.Proficiencias != null && subraca.Proficiencias.Any())
            {
                var proficiencias = subraca.Proficiencias
                    .Select(sr => sr.Proficiencia)
                    .Where(p => p != null)
                    .ToList();

                await _proficienciaService.AdicionarProficienciasAsync(ficha.Id, proficiencias);
            }

            if (subraca.MagiasRaciais != null && subraca.MagiasRaciais.Any())
            {
                var magias = subraca.MagiasRaciais
                    .Select(sr => sr.Magia)
                    .Where(m => m != null)
                    .ToList();

                await _magiaService.AdicionarMagiasAsync(ficha.Id, magias);
            }

            if (subraca.Resistencias != null && subraca.Resistencias.Any())
            {
                var ids = subraca.Resistencias
                    .Where(sr => sr.ResistenciaId != null)
                    .Select(sr => sr.ResistenciaId);

                await _resistenciaService.AdicionarResistenciasAsync(ficha.Id, ids);

            }



            await _controladorEtapasFicha.ProcessarProximaEtapaAsync(ficha, Context, usarFollowUp: true);
        }




        /// <summary>
        /// Tenta finalizar a ficha salvando e removendo temporário. Retorna true se finalizou.
        /// </summary>
        private async Task<bool> TentarFinalizarFichaAsync(FichaPersonagem ficha)
        {
            if (!FichaCompletaValida(ficha))
                return false;

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

            // Verifica se já foi respondido ou deferido
            if (Context.Interaction.HasResponded)
            {
                await Context.Interaction.FollowupAsync($"✅ Ficha do personagem criada com sucesso!\n\n{resumo}", ephemeral: true);
            }
            else
            {
                await RespondAsync($"✅ Ficha do personagem criada com sucesso!\n\n{resumo}", ephemeral: true);
            }

            return true;
        }


        private bool FichaCompletaValida(FichaPersonagem ficha)
        {
            if (ficha == null) return false;

            if (string.IsNullOrWhiteSpace(ficha.Nome)) return false;

            string[] invalidos = { "Não definida", "Não definido" };
            if (invalidos.Contains(ficha.RacaId)) return false;
            if (invalidos.Contains(ficha.ClasseId)) return false;
            if (invalidos.Contains(ficha.AntecedenteId)) return false;
            if (invalidos.Contains(ficha.AlinhamentoId)) return false;

            return true;
        }
    }
}
