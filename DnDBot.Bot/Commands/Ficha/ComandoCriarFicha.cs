using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
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

            FichaTempStore.SavePartialFicha(Context.User.Id, input.Nome);

            var componentesBuilder = new ComponentBuilder()
                .WithSelectMenu(SelectMenuHelper.CriarSelectRaca(_racasService.ObterRacas()))
                .WithSelectMenu(SelectMenuHelper.CriarSelectClasse(_classesService.ObterClasses()))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAntecedente(_antecedentesService.ObterAntecedentes()))
                .WithSelectMenu(SelectMenuHelper.CriarSelectAlinhamento(_alinhamentosService.ObterAlinhamentos()));

            await RespondAsync("Agora escolha os demais detalhes do personagem:", components: componentesBuilder.Build(), ephemeral: true);
        }
        private bool FichaCompletaValida(FichaPersonagem ficha)
        {
            if (ficha == null) return false;

            if (string.IsNullOrWhiteSpace(ficha.Nome)) return false;

            string[] invalidos = { "Não definida", "Não definido" };
            if (invalidos.Contains(ficha.Raca)) return false;
            if (invalidos.Contains(ficha.Classe)) return false;
            if (invalidos.Contains(ficha.Antecedente)) return false;
            if (invalidos.Contains(ficha.Alinhamento)) return false;

            return true;
        }

        /// <summary>
        /// Finaliza o processo de criação da ficha, salvando-a se todos os campos obrigatórios estiverem preenchidos.
        /// Também remove a ficha temporária da memória e solicita a escolha da sub-raça, caso a raça selecionada possua.
        /// </summary>
        /// <returns>
        /// <c>true</c> se a ficha foi considerada válida e finalizada com sucesso; <c>false</c> caso contrário.
        /// </returns>
        private async Task<bool> TentarFinalizarFichaAsync()
        {
            var ficha = FichaTempStore.GetFicha(Context.User.Id);

            if (FichaCompletaValida(ficha))
            {
                _fichaService.AdicionarFicha(ficha);
                FichaTempStore.RemoveFicha(Context.User.Id);

                string resumo =
                    $"**Nome:** {ficha.Nome}\n" +
                    $"**Raça:** {ficha.Raca}\n" +
                    $"**Classe:** {ficha.Classe}\n" +
                    $"**Antecedente:** {ficha.Antecedente}\n" +
                    $"**Alinhamento:** {ficha.Alinhamento}";

                await RespondAsync($"✅ Ficha do personagem criada com sucesso!\n\n{resumo}", ephemeral: true);

                // Se a raça possuir sub-raças, solicita a seleção ao usuário
                var racaObj = _racasService.ObterRacaPorNome(ficha.Raca);
                if (racaObj?.SubRacas != null && racaObj.SubRacas.Any())
                {
                    var selectSubraca = SelectMenuHelper.CriarSelectSubraca(racaObj.SubRacas);
                    await FollowupAsync("Escolha a sub-raça do seu personagem:", components: new ComponentBuilder().WithSelectMenu(selectSubraca).Build(), ephemeral: true);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona uma raça.
        /// </summary>
        [ComponentInteraction("select_raca")]
        public async Task SelectRacaHandler(string valor)
        {
            FichaTempStore.UpdateFicha(Context.User.Id, raca: valor);

            if (!await TentarFinalizarFichaAsync())
                await DeferAsync(ephemeral: true);
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona uma classe.
        /// </summary>
        [ComponentInteraction("select_classe")]
        public async Task SelectClasseHandler(string valor)
        {
            FichaTempStore.UpdateFicha(Context.User.Id, classe: valor);

            if (!await TentarFinalizarFichaAsync())
                await DeferAsync(ephemeral: true);
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona um antecedente.
        /// </summary>
        [ComponentInteraction("select_antecedente")]
        public async Task SelectAntecedenteHandler(string valor)
        {
            FichaTempStore.UpdateFicha(Context.User.Id, antecedente: valor);

            if (!await TentarFinalizarFichaAsync())
                await DeferAsync(ephemeral: true);
        }

        /// <summary>
        /// Evento disparado quando o usuário seleciona um alinhamento.
        /// </summary>
        [ComponentInteraction("select_alinhamento")]
        public async Task SelectAlinhamentoHandler(string valor)
        {
            FichaTempStore.UpdateFicha(Context.User.Id, alinhamento: valor);

            if (!await TentarFinalizarFichaAsync())
                await DeferAsync(ephemeral: true);
        }       


        // Botão para concluir a distribuição
        [ComponentInteraction("concluir_distribuicao")]
        public async Task ConcluirDistribuicao()
        {
            var fichas = _fichaService.ObterFichasPorJogador(Context.User.Id);
            var ficha = fichas.OrderByDescending(f => f.DataAlteracao).FirstOrDefault();

            if (ficha == null)
            {
                await RespondAsync("❌ Ficha não encontrada para salvar atributos.", ephemeral: true);
                return;
            }

            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);

            if (dist.PontosUsados > dist.PontosDisponiveis)
            {
                await RespondAsync("❌ Você usou mais pontos do que o permitido.", ephemeral: true);
                return;
            }

            ficha.Forca = dist.Atributos["Forca"];
            ficha.Destreza = dist.Atributos["Destreza"];
            ficha.Constituicao = dist.Atributos["Constituicao"];
            ficha.Inteligencia = dist.Atributos["Inteligencia"];
            ficha.Sabedoria = dist.Atributos["Sabedoria"];
            ficha.Carisma = dist.Atributos["Carisma"];

            _fichaService.AtualizarFicha(ficha);

            _atributosHandler.RemoverDistribuicao(Context.User.Id, ficha.Id);

            await RespondAsync("✅ Distribuição de atributos concluída com sucesso!", ephemeral: true);
        }

        
    }
}
