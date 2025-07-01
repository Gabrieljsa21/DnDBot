using Discord.Interactions;
using DnDBot.Application.Services;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands.Ficha
{
    /// <summary>
    /// Módulo responsável por lidar com a seleção de sub-raças durante a criação da ficha.
    /// </summary>
    public class ComandoSubraca : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly FichaService _fichaService;

        /// <summary>
        /// Inicializa uma nova instância do módulo <see cref="ComandoSubraca"/>.
        /// </summary>
        /// <param name="fichaService">Serviço responsável pela manipulação das fichas de personagem.</param>
        public ComandoSubraca(FichaService fichaService)
        {
            _fichaService = fichaService;
        }

        /// <summary>
        /// Manipula a interação do componente de seleção de sub-raça.
        /// Atualiza a última ficha criada pelo jogador com a sub-raça selecionada.
        /// </summary>
        /// <param name="valor">Valor da sub-raça selecionada pelo usuário.</param>
        [ComponentInteraction("select_subraca")]
        public async Task SelectSubracaHandler(string valor)
        {
            var ficha = _fichaService.ObterUltimaFichaDoJogador(Context.User.Id);

            if (ficha == null)
            {
                await RespondAsync("❌ Não encontrei a ficha para atualizar a sub-raça.", ephemeral: true);
                return;
            }

            ficha.Subraca = valor;
            _fichaService.AtualizarFicha(ficha);

            await RespondAsync($"✅ Sub-raça **{valor}** adicionada à ficha **{ficha.Nome}**.", ephemeral: true);
        }
    }
}
