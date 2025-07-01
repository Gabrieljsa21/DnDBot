//using Discord;
//using Discord.Interactions;
//using Discord.WebSocket;
//using DnDBot.Application.Models;
//using DnDBot.Application.Services;
//using DnDBot.Bot.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DnDBot.Bot.Commands.Ficha
//{
//    /// <summary>
//    /// Módulo responsável pelo comando /ficha_criar e etapas interativas da criação de personagem.
//    /// </summary>
//    public class ComandoCriarFicha : InteractionModuleBase<SocketInteractionContext>
//    {
//        private readonly FichaService _fichaService;
//        private readonly RacasService _racasService;
//        private readonly ClassesService _classesService;
//        private readonly AntecedentesService _antecedentesService;
//        private readonly AlinhamentosService _alinhamentosService;
//        private readonly DistribuicaoAtributosService _distribuicaoAtributosService;

//        /// <summary>
//        /// Construtor com injeção dos serviços necessários.
//        /// </summary>
//        public ComandoCriarFicha(
//            FichaService fichaService,
//            RacasService racasService,
//            ClassesService classesService,
//            AntecedentesService antecedentesService,
//            AlinhamentosService alinhamentosService,
//            DistribuicaoAtributosService distribuicaoAtributosService)
//        {
//            _fichaService = fichaService;
//            _racasService = racasService;
//            _classesService = classesService;
//            _antecedentesService = antecedentesService;
//            _alinhamentosService = alinhamentosService;
//            _distribuicaoAtributosService = distribuicaoAtributosService;
//        }

//        /// <summary>
//        /// Comando inicial que exibe o modal para inserir o nome do personagem.
//        /// </summary>
//        [SlashCommand("ficha_criar", "Inicia criação da ficha do personagem")]
//        public async Task CriarFichaAsync()
//        {
//            var modal = new ModalBuilder()
//                .WithTitle("Criar Personagem - Parte 1")
//                .WithCustomId("modal_ficha_nome")
//                .AddTextInput("Nome do personagem", "nome_personagem", TextInputStyle.Short, placeholder: "Ex: Zephyr", required: true);

//            await RespondWithModalAsync(modal.Build());
//        }

//        /// <summary>
//        /// Handler que processa o modal de nome e envia os selects.
//        /// </summary>
//        [ModalInteraction("modal_ficha_nome")]
//        public async Task ModalFichaNomeHandler(ModalFichaNomeInput input)
//        {
//            if (string.IsNullOrWhiteSpace(input.Nome))
//            {
//                await RespondAsync("❌ Nome inválido.", ephemeral: true);
//                return;
//            }

//            FichaTempStore.SavePartialFicha(Context.User.Id, input.Nome);

//            var componentesBuilder = new ComponentBuilder()
//                .WithSelectMenu(SelectMenuHelper.CriarSelectRaca(_racasService.ObterRacas()))
//                .WithSelectMenu(SelectMenuHelper.CriarSelectClasse(_classesService.ObterClasses()))
//                .WithSelectMenu(SelectMenuHelper.CriarSelectAntecedente(_antecedentesService.ObterAntecedentes()))
//                .WithSelectMenu(SelectMenuHelper.CriarSelectAlinhamento(_alinhamentosService.ObterAlinhamentos()));

//            await RespondAsync("Agora escolha os demais detalhes do personagem:", components: componentesBuilder.Build(), ephemeral: true);
//        }

//        /// <summary>
//        /// Verifica se a ficha está completa e válida.
//        /// </summary>
//        private bool FichaCompletaValida(FichaPersonagem ficha)
//        {
//            if (ficha == null) return false;

//            if (string.IsNullOrWhiteSpace(ficha.Nome)) return false;

//            string[] invalidos = { "Não definida", "Não definido" };
//            if (invalidos.Contains(ficha.Raca)) return false;
//            if (invalidos.Contains(ficha.Classe)) return false;
//            if (invalidos.Contains(ficha.Antecedente)) return false;
//            if (invalidos.Contains(ficha.Alinhamento)) return false;

//            return true;
//        }

//        /// <summary>
//        /// Finaliza o processo de criação da ficha, salvando-a se todos os campos obrigatórios estiverem preenchidos.
//        /// Também remove a ficha temporária da memória e solicita a escolha da sub-raça, caso a raça selecionada possua.
//        /// </summary>
//        /// <returns>
//        /// <c>true</c> se a ficha foi considerada válida e finalizada com sucesso; <c>false</c> caso contrário.
//        /// </returns>
//        private async Task<bool> TentarFinalizarFichaAsync()
//        {
//            var ficha = FichaTempStore.GetFicha(Context.User.Id);

//            if (FichaCompletaValida(ficha))
//            {
//                _fichaService.AdicionarFicha(ficha);
//                FichaTempStore.RemoveFicha(Context.User.Id);

//                string resumo =
//                    $"**Nome:** {ficha.Nome}\n" +
//                    $"**Raça:** {ficha.Raca}\n" +
//                    $"**Classe:** {ficha.Classe}\n" +
//                    $"**Antecedente:** {ficha.Antecedente}\n" +
//                    $"**Alinhamento:** {ficha.Alinhamento}";

//                await RespondAsync($"✅ Ficha do personagem criada com sucesso!\n\n{resumo}", ephemeral: true);

//                // Se a raça possuir sub-raças, solicita a seleção ao usuário
//                var racaObj = _racasService.ObterRacaPorNome(ficha.Raca);
//                if (racaObj?.SubRacas != null && racaObj.SubRacas.Any())
//                {
//                    var selectSubraca = SelectMenuHelper.CriarSelectSubraca(racaObj.SubRacas);
//                    await FollowupAsync("Escolha a sub-raça do seu personagem:", components: new ComponentBuilder().WithSelectMenu(selectSubraca).Build(), ephemeral: true);
//                }

//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// Evento disparado quando o usuário seleciona uma raça.
//        /// </summary>
//        [ComponentInteraction("select_raca")]
//        public async Task SelectRacaHandler(string valor)
//        {
//            FichaTempStore.UpdateFicha(Context.User.Id, raca: valor);

//            if (!await TentarFinalizarFichaAsync())
//                await DeferAsync(ephemeral: true);
//        }

//        /// <summary>
//        /// Evento disparado quando o usuário seleciona uma classe.
//        /// </summary>
//        [ComponentInteraction("select_classe")]
//        public async Task SelectClasseHandler(string valor)
//        {
//            FichaTempStore.UpdateFicha(Context.User.Id, classe: valor);

//            if (!await TentarFinalizarFichaAsync())
//                await DeferAsync(ephemeral: true);
//        }

//        /// <summary>
//        /// Evento disparado quando o usuário seleciona um antecedente.
//        /// </summary>
//        [ComponentInteraction("select_antecedente")]
//        public async Task SelectAntecedenteHandler(string valor)
//        {
//            FichaTempStore.UpdateFicha(Context.User.Id, antecedente: valor);

//            if (!await TentarFinalizarFichaAsync())
//                await DeferAsync(ephemeral: true);
//        }

//        /// <summary>
//        /// Evento disparado quando o usuário seleciona um alinhamento.
//        /// </summary>
//        [ComponentInteraction("select_alinhamento")]
//        public async Task SelectAlinhamentoHandler(string valor)
//        {
//            FichaTempStore.UpdateFicha(Context.User.Id, alinhamento: valor);

//            if (!await TentarFinalizarFichaAsync())
//                await DeferAsync(ephemeral: true);
//        }


//        // === INÍCIO COMANDO /criar-personagem PARA DISTRIBUIR ATRIBUTOS ===

//        [SlashCommand("criar-personagem", "Começa a distribuição de atributos da ficha")]
//        public async Task CriarPersonagemCommand()
//        {
//            var fichasJogador = _fichaService.ObterFichasPorJogador(Context.User.Id);

//            if (fichasJogador == null || !fichasJogador.Any())
//            {
//                await RespondAsync("❌ Você não possui fichas para distribuir atributos. Crie uma ficha primeiro.", ephemeral: true);
//                return;
//            }

//            var ficha = fichasJogador.OrderByDescending(f => f.DataAlteracao).FirstOrDefault();

//            if (ficha == null)
//            {
//                await RespondAsync("❌ Não foi possível encontrar sua ficha.", ephemeral: true);
//                return;
//            }

//            var dist = _distribuicaoAtributosService.CriarOuObterDistribuicao(Context.User.Id, ficha.Id);
//            dist.PontosDisponiveis = 27;
//            dist.PontosUsados = 0;
//            dist.Atributos = new Dictionary<string, int>
//    {
//        { "Força", 8 },
//        { "Destreza", 8 },
//        { "Constituição", 8 },
//        { "Inteligência", 8 },
//        { "Sabedoria", 8 },
//        { "Carisma", 8 }
//    };

//            var embed = ConstruirEmbedDistribuicao(dist);
//            var component = ConstruirComponentesDistribuicao(dist);

//            await RespondAsync(embed: embed, components: component, ephemeral: true);
//        }


//        private Embed ConstruirEmbedDistribuicao(DistribuicaoAtributosTemp dist)
//        {
//            var builder = new EmbedBuilder()
//                .WithTitle("Distribuição de Atributos - Point Buy")
//                .WithColor(Color.DarkBlue);

//            int totalUsado = dist.Atributos.Values.Sum();
//            int pontosUsados = dist.PontosUsados;
//            int pontosRestantes = dist.PontosDisponiveis - pontosUsados;

//            builder.AddField("Pontos restantes", pontosRestantes.ToString(), inline: true);

//            foreach (var attr in dist.Atributos)
//            {
//                builder.AddField(attr.Key, attr.Value.ToString(), inline: true);
//            }

//            // Se quiser mostrar bônus racial, pode incluir aqui (exemplo comentado)
//            // builder.AddField("Bônus Racial", "Força +2, Carisma +1");

//            return builder.Build();
//        }

//        private MessageComponent ConstruirComponentesDistribuicao(DistribuicaoAtributosTemp dist)
//        {
//            var builder = new ComponentBuilder();

//            foreach (var attr in dist.Atributos.Keys)
//            {
//                builder.WithButton($"+ {attr}", customId: $"mais_{attr.ToLower()}", style: ButtonStyle.Success, row: 0);
//                builder.WithButton($"- {attr}", customId: $"menos_{attr.ToLower()}", style: ButtonStyle.Danger, row: 0);
//            }

//            // Botão para concluir
//            builder.WithButton("✅ Concluir distribuição", customId: "concluir_distribuicao", style: ButtonStyle.Primary, row: 1);

//            return builder.Build();
//        }


//        // Aqui você implementaria os handlers para os botões de aumentar e diminuir atributos,
//        // bem como para concluir a distribuição, que receberão os ComponentInteraction com customId "mais_força", etc.
//        // Por exemplo:

//        [ComponentInteraction("mais_força")]
//        public async Task AumentarForca()
//        {
//            await AjustarAtributo("Força", +1);
//        }

//        [ComponentInteraction("menos_força")]
//        public async Task DiminuirForca()
//        {
//            await AjustarAtributo("Força", -1);
//        }

//        // Repetir para os outros atributos (Destreza, Constituição, Inteligência, Sabedoria, Carisma)
//        // Para economizar espaço, implementei um método genérico para ajustar:

//        private async Task AjustarAtributo(string atributo, int delta)
//        {
//            var ficha = _fichaService.ObterFichasPorJogador(Context.User.Id)
//                         .OrderByDescending(f => f.DataAlteracao)
//                         .FirstOrDefault();

//            if (ficha == null)
//            {
//                await RespondAsync("❌ Nenhuma ficha encontrada para distribuir atributos.", ephemeral: true);
//                return;
//            }

//            var dist = _distribuicaoAtributosService.CriarOuObterDistribuicao(Context.User.Id, ficha.Id);


//            int valorAtual = dist.Atributos[atributo];
//            int novoValor = valorAtual + delta;

//            // Limites de 8 a 15 para o point buy padrão
//            if (novoValor < 8 || novoValor > 15)
//            {
//                await DeferAsync(ephemeral: true);
//                return;
//            }

//            // Calcular custo total e verificar pontos disponíveis
//            int custoAtual = _distribuicaoAtributosService.CalcularCusto(dist.Atributos);
//            int custoNovo = _distribuicaoAtributosService.CalcularCustoComAlteracao(dist.Atributos, atributo, novoValor);

//            if (custoNovo > dist.PontosDisponiveis)
//            {
//                await DeferAsync(ephemeral: true);
//                return;
//            }

//            dist.Atributos[atributo] = novoValor;
//            dist.PontosUsados = custoNovo;

//            var embed = ConstruirEmbedDistribuicao(dist);
//            var componentes = ConstruirComponentesDistribuicao(dist);

//            var component = (SocketMessageComponent)Context.Interaction;

//            await component.UpdateAsync(msg =>
//            {
//                msg.Embed = embed;
//                msg.Components = componentes;
//            });

//        }

//        [ComponentInteraction("concluir_distribuicao")]
//        public async Task ConcluirDistribuicao()
//        {
//            var fichaTemp = FichaTempStore.GetFicha(Context.User.Id);
//            if (fichaTemp == null)
//            {
//                await RespondAsync("❌ Ficha temporária não encontrada. Crie uma ficha primeiro com /ficha_criar.", ephemeral: true);
//                return;
//            }

//            var dist = _distribuicaoAtributosService.CriarOuObterDistribuicao(Context.User.Id, fichaTemp.Id);

//            if (dist.PontosUsados > dist.PontosDisponiveis)
//            {
//                await RespondAsync("❌ Você usou mais pontos do que o permitido.", ephemeral: true);
//                return;
//            }

//            fichaTemp.Forca = dist.Atributos["Força"];
//            fichaTemp.Destreza = dist.Atributos["Destreza"];
//            fichaTemp.Constituicao = dist.Atributos["Constituição"];
//            fichaTemp.Inteligencia = dist.Atributos["Inteligência"];
//            fichaTemp.Sabedoria = dist.Atributos["Sabedoria"];
//            fichaTemp.Carisma = dist.Atributos["Carisma"];

//            // Atualiza a ficha temporária
//            FichaTempStore.UpdateFicha(Context.User.Id, fichaTemp);

//            await RespondAsync("✅ Distribuição de atributos concluída com sucesso! Agora você pode continuar a criação da ficha.", ephemeral: true);

//            // Aqui você pode seguir com próximos passos ou permitir comandos para seguir criação

//            _distribuicaoAtributosService.RemoverDistribuicao(Context.User.Id, fichaTemp.Id);
//        }

//        // ====================================================================
//    }
//}
