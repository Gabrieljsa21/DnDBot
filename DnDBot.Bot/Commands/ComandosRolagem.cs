using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using System;
using System.Threading.Tasks;

namespace DnDBot.Bot.Commands
{
    /// <summary>
    /// Módulo de comandos slash do Discord relacionados a rolagens de dados.
    /// Disponibiliza comandos para rolagens simples, com vantagem e desvantagem,
    /// utilizando o serviço de rolagem de dados injetado via DI.
    /// </summary>
    public class ComandosRolagem : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly RolagemDadosService _rolagemService;
        private readonly FormatadorMensagemService _formatador;

        /// <summary>
        /// Inicializa uma nova instância do módulo de comandos de rolagem,
        /// recebendo o serviço de rolagem de dados por injeção de dependência.
        /// </summary>
        /// <param name="servico">Serviço responsável por realizar as rolagens.</param>
        public ComandosRolagem(RolagemDadosService servico, FormatadorMensagemService formatador)
        {
            _rolagemService = servico;
            _formatador = formatador;
        }

        /// <summary>
        /// Comando slash "/roll" para realizar uma rolagem de dados simples.
        /// Exemplo de expressão: "2d6+3".
        /// </summary>
        /// <param name="expressao">Expressão da rolagem no formato NdX+Y.</param>
        [SlashCommand("roll", "Rola dados no formato NdX+Y")]
        public async Task RollAsync([Summary("expressao", "Expressão de dados para rolar, ex: 2d6+3")] string expressao)
        {
            await ProcessarRolagemAsync(_rolagemService.Rolar, expressao);
        }

        /// <summary>
        /// Comando slash "/roll_vantagem" para realizar uma rolagem com vantagem.
        /// O resultado será o maior entre duas rolagens.
        /// </summary>
        /// <param name="expressao">Expressão da rolagem no formato NdX+Y.</param>
        [SlashCommand("roll_vantagem", "Rola com vantagem (dois dados, pega o maior)")]
        public async Task RollComVantagemAsync([Summary("expressao", "Expressão de dados, ex: 1d20+3")] string expressao)
        {
            await ProcessarRolagemAsync(_rolagemService.RolarVantagem, expressao);
        }

        /// <summary>
        /// Comando slash "/roll_desvantagem" para realizar uma rolagem com desvantagem.
        /// O resultado será o menor entre duas rolagens.
        /// </summary>
        /// <param name="expressao">Expressão da rolagem no formato NdX+Y.</param>
        [SlashCommand("roll_desvantagem", "Rola com desvantagem (dois dados, pega o menor)")]
        public async Task RollComDesvantagemAsync([Summary("expressao", "Expressão de dados, ex: 1d20+3")] string expressao)
        {
            await ProcessarRolagemAsync(_rolagemService.RolarDesvantagem, expressao);
        }

        /// <summary>
        /// Método auxiliar que processa a rolagem de dados utilizando a função de rolagem
        /// passada como parâmetro, valida o resultado e responde no Discord.
        /// </summary>
        /// <param name="funcRolagem">Função que executa a rolagem (simples, vantagem ou desvantagem).</param>
        /// <param name="expressao">Expressão de dados para rolar, ex: "1d20+3".</param>
        private async Task ProcessarRolagemAsync(Func<string, ResultadoRolagem?> funcRolagem, string expressao)
        {
            var resultado = funcRolagem(expressao);

            if (resultado == null)
            {
                await RespondAsync("Expressão inválida! Use um formato como 1d20+3", ephemeral: true);
                return;
            }

            string mensagem = _formatador.FormatarMensagem(resultado);
            await RespondAsync(mensagem);
        }
    }
}
