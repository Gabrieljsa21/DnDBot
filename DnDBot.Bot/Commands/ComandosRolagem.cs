using Discord.Interactions;
using System.Threading.Tasks;
using DnDBot.Application.Services;

namespace DnDBot.Bot.Commands
{
    /// <summary>
    /// Módulo de comandos slash relacionados à rolagem de dados.
    /// </summary>
    public class ComandosRolagem : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly RolagemDadosService _rolagemService;

        /// <summary>
        /// Construtor que recebe o serviço de rolagem de dados via injeção de dependência.
        /// </summary>
        /// <param name="servico">Serviço que realiza a rolagem de dados.</param>
        public ComandosRolagem(RolagemDadosService servico)
        {
            _rolagemService = servico;
        }

        /// <summary>
        /// Comando slash /roll que executa a rolagem de dados.
        /// </summary>
        /// <param name="expressao">Expressão de dados para rolar, ex: 2d6+3.</param>
        [SlashCommand("roll", "Rola dados no formato NdX+Y")]
        public async Task RollAsync([Summary("expressao", "Expressão de dados para rolar, ex: 2d6+3")] string expressao)
        {
            var resultado = _rolagemService.Rolar(expressao);

            if (resultado == null)
            {
                await RespondAsync("Expressão inválida! Use um formato como 2d6+1", ephemeral: true);
                return;
            }

            var (total, detalhes) = resultado.Value;
            await RespondAsync($"🎲 Você rolou `{expressao}`: **{detalhes} = {total}**");
        }
    }
}
