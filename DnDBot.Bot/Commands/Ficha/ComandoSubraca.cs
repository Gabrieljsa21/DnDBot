using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Distribuicao;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável por lidar com a seleção de sub-raças durante a criação da ficha.
    /// </summary>
    public class ComandoSubraca : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;
        private readonly RacasService _racasService;
        private readonly DistribuicaoAtributosHandler _atributosHandler;

        /// <summary>
        /// Inicializa uma nova instância do módulo <see cref="ComandoSubraca"/>.
        /// </summary>
        /// <param name="fichaService">Serviço responsável pela manipulação das fichas de personagem.</param>
        public ComandoSubraca(FichaService fichaService, RacasService racasService, DistribuicaoAtributosHandler atributosHandler)
        {
            _fichaService = fichaService;
            _racasService = racasService;
            _atributosHandler = atributosHandler;
        }

        /// <summary>
        /// Manipula a interação do componente de seleção de sub-raça.
        /// Atualiza a última ficha criada pelo jogador com a sub-raça selecionada.
        /// </summary>
        /// <param name="valor">Valor da sub-raça selecionada pelo usuário.</param>
        [ComponentInteraction("select_subraca")]
        public async Task SelectSubracaHandler(string valor)
        {
            var ficha = await _fichaService.ObterUltimaFichaDoJogadorAsync(Context.User.Id);

            if (ficha == null)
            {
                await RespondAsync("❌ Não encontrei a ficha para atualizar a sub-raça.", ephemeral: true);
                return;
            }

            var subraca = (await _racasService.ObterTodasSubracasAsync()).FirstOrDefault(sr => sr.Id == valor);

            if (subraca == null)
            {
                await RespondAsync("❌ Sub-raça selecionada não encontrada.", ephemeral: true);
                return;
            }

            // Atualiza a ficha com os dados da sub-raça
            ficha.IdSubraca = subraca.Id;
            ficha.BonusAtributos = subraca.BonusAtributos
                .Select(b => new BonusAtributo
                {
                    Atributo = b.Atributo,
                    Valor = b.Valor,
                    Origem = "Sub-Raça"
                }).ToList();
            ficha.Tamanho = subraca.Tamanho;
            ficha.Deslocamento = subraca.Deslocamento;
            ficha.Idiomas = subraca.Idiomas;
            ficha.Proficiencias = subraca.Proficiencias;
            ficha.VisaoNoEscuro = subraca.VisaoNoEscuro;
            ficha.Resistencias = subraca.Resistencias.ToList();
            ficha.Caracteristicas = subraca.Caracteristicas.ToList();
            ficha.MagiasRaciais = subraca.MagiasRaciais.ToList();


            await _fichaService.AtualizarFichaAsync(ficha);

            // Cria o estado temporário para distribuição (exemplo inicial)
            var distTemp = new DistribuicaoAtributosTemp
            {
                JogadorId = Context.User.Id,
                FichaId = ficha.Id,
                PontosDisponiveis = 27,
                PontosUsados = 0,
                // Atributos base já com 8 pontos cada (pode alterar se quiser outro valor)
                Atributos = new Dictionary<string, int>
                {
                        { "Forca", 8 },
                        { "Destreza", 8 },
                        { "Constituicao", 8 },
                        { "Inteligencia", 8 },
                        { "Sabedoria", 8 },
                        { "Carisma", 8 }
                    },
                BonusRacial = new Dictionary<string, int>
                    {
                        { "Forca", 0 },
                        { "Destreza", 0 },
                        { "Constituicao", 0 },
                        { "Inteligencia", 0 },
                        { "Sabedoria", 0 },
                        { "Carisma", 0 }
                    }
            };

            // Aplica os bônus da sub-raça (se houver)
            foreach (var bonus in ficha.BonusAtributos)
            {
                if (distTemp.BonusRacial.ContainsKey(bonus.Atributo))
                    distTemp.BonusRacial[bonus.Atributo] = bonus.Valor;
            }

            // Armazena no repositório temporário para depois recuperar nos handlers de botões
            var dist = _atributosHandler.ObterDistribuicao(Context.User.Id, ficha.Id);
            _atributosHandler.InicializarDistribuicao(dist, ficha);

            foreach (var bonus in ficha.BonusAtributos)
            {
                if (dist.BonusRacial.ContainsKey(bonus.Atributo))
                    dist.BonusRacial[bonus.Atributo] = bonus.Valor;
            }

            // Envia mensagem para o usuário com os botões para distribuir pontos
            await RespondAsync(
                $"✅ Sub-raça **{valor}** aplicada à ficha **{ficha.Nome}** com sucesso!\n\nAgora distribua os pontos nos atributos:",
                embed: _atributosHandler.ConstruirEmbedDistribuicao(distTemp),
                components: _atributosHandler.ConstruirComponentesDistribuicao(distTemp),
                ephemeral: true);

        }


    }
}
