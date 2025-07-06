using Discord;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Inputs;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Antecedentes;
using DnDBot.Application.Services.Distribuicao;
using DnDBot.Bot.Helpers;
using System;
using System.Collections.Generic;
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

        public ComandoCriarFicha(
            FichaService fichaService,
            RacasService racasService,
            ClassesService classesService,
            AntecedentesService antecedentesService,
            AlinhamentosService alinhamentosService,
            DistribuicaoAtributosHandler atributosHandler)
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _classesService = classesService;
            _antecedentesService = antecedentesService;
            _alinhamentosService = alinhamentosService;
            _atributosHandler = atributosHandler;

        }

        /// <summary>
        /// Comando inicial que exibe o modal para inserir o nome do personagem.
        /// </summary>
        [SlashCommand("ficha_criar", "Inicia criação da ficha do personagem")]
        public async Task CriarFichaAsync()
        {
            var modal = new ModalBuilder()
                .WithTitle("Criar Personagem - Parte 1")
                .WithCustomId("modal_ficha_nome")
                .AddTextInput("Nome do personagem", "nome_personagem", TextInputStyle.Short, placeholder: "Ex: Zephyr", required: true);

            await RespondWithModalAsync(modal.Build());
        }

        /// <summary>
        /// Handler que processa o modal de nome e envia os selects.
        /// </summary>
        [ModalInteraction("modal_ficha_nome")]
        public async Task ModalFichaNomeHandler(ModalFichaNomeInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Nome))
            {
                await RespondAsync("❌ Nome inválido.", ephemeral: true);
                return;
            }

            // 🔒 Verifica se já existe uma ficha com o mesmo nome para este usuário
            var fichaExistente = await _fichaService.ObterFichaPorJogadorENomeAsync(Context.User.Id, input.Nome);
            if (fichaExistente != null)
            {
                await RespondAsync("❌ Você já possui uma ficha com esse nome. Escolha outro nome.", ephemeral: true);
                return;
            }

            FichaTempStore.SavePartialFicha(Context.User.Id, input.Nome);

            var racas = await _racasService.ObterRacasAsync();
            var classes = await _classesService.ObterClassesAsync();
            var antecedentes = await _antecedentesService.ObterAntecedentesAsync();
            var alinhamentos = await _alinhamentosService.ObterAlinhamentosAsync();

            var componentesBuilder = new ComponentBuilder()
                .WithSelectMenu(SelectMenuHelper.CriarSelectRaca(racas))
                .WithSelectMenu(SelectMenuHelper.CriarSelectClasse(classes))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAntecedente(antecedentes))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAlinhamento(alinhamentos));

            await RespondAsync("Agora escolha os demais detalhes do personagem:", components: componentesBuilder.Build(), ephemeral: true);
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona uma raça.
        /// </summary>
        [ComponentInteraction("select_raca")]
        public async Task SelectRacaHandler(string valor)
        {
            await AtualizarFichaCampoEFinalizar(
                valor,
                "Raça",
                _racasService.ObterRacaPorIdAsync,
                (ficha, id) => ficha.RacaId = id
            );
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona uma classe.
        /// </summary>
        [ComponentInteraction("select_classe")]
        public async Task SelectClasseHandler(string valor)
        {
            await AtualizarFichaCampoEFinalizar(
                valor,
                "Classe",
                _classesService.ObterClassePorIdAsync,
                (ficha, id) => ficha.ClasseId = id
            );
        }

        [ComponentInteraction("select_antecedente")]
        public async Task SelectAntecedenteHandler(string valor)
        {
            var antecedente = await _antecedentesService.ObterAntecedentePorIdAsync(valor);
            if (antecedente == null)
            {
                await RespondAsync("❌ Antecedente não encontrado.", ephemeral: true);
                return;
            }

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            ficha.AntecedenteId = valor;

            if (antecedente.RiquezaInicial != null)
            {
                foreach (var moeda in antecedente.RiquezaInicial)
                    ficha.BolsaDeMoedas.Adicionar(moeda);
            }

            if (!await TentarFinalizarFichaAsync(ficha))
                await DeferAsync(ephemeral: true);
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona um alinhamento.
        /// </summary>
        [ComponentInteraction("select_alinhamento")]
        public async Task SelectAlinhamentoHandler(string valor)
        {
            await AtualizarFichaCampoEFinalizar(
                valor,
                "Alinhamento",
                _alinhamentosService.ObterAlinhamentoPorIdAsync,
                (ficha, id) => ficha.AlinhamentoId = id
            );
        }

        /// <summary>
        /// Finaliza o processo de criação da ficha, salvando-a se todos os campos obrigatórios estiverem preenchidos.
        /// Também remove a ficha temporária da memória e solicita a escolha da sub-raça, caso a raça selecionada possua.
        /// </summary>
        /// <returns>
        /// <c>true</c> se a ficha foi considerada válida e finalizada com sucesso; <c>false</c> caso contrário.
        /// </returns>
        private async Task<bool> TentarFinalizarFichaAsync(FichaPersonagem ficha)
        {
            if (!FichaCompletaValida(ficha))
                return false;

            await _fichaService.AdicionarFichaAsync(ficha);
            FichaTempStore.RemoveFicha(Context.User.Id);

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

            await RespondAsync($"✅ Ficha do personagem criada com sucesso!\n\n{resumo}", ephemeral: true);

            var racaObj = await _racasService.ObterRacaPorIdAsync(ficha.RacaId);
            if (racaObj?.SubRaca?.Any() == true)
            {
                var selectSubraca = SelectMenuHelper.CriarSelectSubraca(racaObj.SubRaca);
                await FollowupAsync("Escolha a sub-raça do seu personagem:",
                    components: new ComponentBuilder().WithSelectMenu(selectSubraca).Build(),
                    ephemeral: true);
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

            ficha.Forca = dist.Atributos["Forca"];
            ficha.Destreza = dist.Atributos["Destreza"];
            ficha.Constituicao = dist.Atributos["Constituicao"];
            ficha.Inteligencia = dist.Atributos["Inteligencia"];
            ficha.Sabedoria = dist.Atributos["Sabedoria"];
            ficha.Carisma = dist.Atributos["Carisma"];

            await _fichaService.AtualizarFichaAsync(ficha);

            _atributosHandler.RemoverDistribuicao(Context.User.Id, ficha.Id);

            await FollowupAsync("✅ Distribuição de atributos concluída com sucesso!", ephemeral: true);
        }


        private async Task<bool> AtualizarFichaCampoEFinalizar<T>(
    string valor,
    string nomeEntidade,
    Func<string, Task<T>> buscarEntidadeAsync,
    Action<FichaPersonagem, string> atualizarCampo)
        {
            var entidade = await buscarEntidadeAsync(valor);
            if (entidade == null)
            {
                await RespondAsync($"❌ {nomeEntidade} não encontrada.", ephemeral: true);
                return false;
            }

            var ficha = FichaTempStore.GetOrCreateFicha(Context.User.Id);
            atualizarCampo(ficha, valor);

            if (!await TentarFinalizarFichaAsync(ficha))
                await DeferAsync(ephemeral: true);

            return true;
        }

    }
}
