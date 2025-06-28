using Discord.Interactions;
using DnDBot.Application.Models;
using DnDBot.Application.Services;
using System;
using System.Collections.Generic;
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
        [SlashCommand("roll", "Rola uma ou mais expressões de dados separadas por vírgula, ex: 2d20+3, 1d6")]
        public async Task RollAsync([Summary("expressao", "Uma ou mais expressões separadas por vírgula")] string expressoes)
        {
            await ProcessarMultiplaRolagemAsync(_rolagemService.Rolar, expressoes);
        }

        [SlashCommand("roll_vantagem", "Rola uma ou mais expressões com vantagem (duas rolagens, pega o maior), separadas por vírgula")]
        public async Task RollComVantagemAsync([Summary("expressao", "Uma ou mais expressões separadas por vírgula")] string expressoes)
        {
            await ProcessarMultiplaRolagemAsync(_rolagemService.RolarVantagem, expressoes);
        }

        [SlashCommand("roll_desvantagem", "Rola uma ou mais expressões com desvantagem (duas rolagens, pega o menor), separadas por vírgula")]
        public async Task RollComDesvantagemAsync([Summary("expressao", "Uma ou mais expressões separadas por vírgula")] string expressoes)
        {
            await ProcessarMultiplaRolagemAsync(_rolagemService.RolarDesvantagem, expressoes);
        }

        /// <summary>
        /// Método que processa múltiplas expressões separadas por vírgula,
        /// usando a função de rolagem passada, e envia uma única resposta com os resultados.
        /// </summary>
        /// <param name="funcRolagem">Função que executa a rolagem (simples, vantagem ou desvantagem).</param>
        /// <param name="expressoes">String com uma ou mais expressões separadas por vírgula.</param>
        private async Task ProcessarMultiplaRolagemAsync(Func<string, ResultadoRolagem?> funcRolagem, string expressoes)
        {
            if (string.IsNullOrWhiteSpace(expressoes))
            {
                await RespondAsync("Por favor, informe pelo menos uma expressão de rolagem válida.", ephemeral: true);
                return;
            }

            var listaExpressoes = expressoes.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var mensagens = new List<string>();

            foreach (var expressao in listaExpressoes)
            {
                var exprTrim = expressao.Trim();
                var resultado = funcRolagem(exprTrim);

                if (resultado == null)
                {
                    mensagens.Add($"❌ Expressão inválida: `{exprTrim}`");
                    continue;
                }

                string msg = _formatador.FormatarMensagem(resultado);
                mensagens.Add(msg);
            }

            string respostaFinal = string.Join("\n\n", mensagens);
            await RespondAsync(respostaFinal);
        }

    }
}
