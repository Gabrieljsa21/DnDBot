using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Rolagem;
using System;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por formatar a mensagem de resultado da rolagem de dados.
    /// </summary>
    public class FormatadorMensagemService
    {
        /// <summary>
        /// Gera uma mensagem formatada para exibir o resultado da rolagem,
        /// baseada no tipo de rolagem (normal, vantagem ou desvantagem).
        /// </summary>
        /// <param name="resultado">Objeto <see cref="ResultadoRolagem"/> contendo os dados da rolagem.</param>
        /// <returns>Uma string formatada representando o resultado da rolagem.</returns>
        public string FormatarMensagem(ResultadoRolagem resultado)
        {
            if (resultado == null)
                return "Erro ao formatar a rolagem.";

            // Emoji representando o tipo de rolagem
            string emoji = resultado.Tipo switch
            {
                TipoRolagem.Vantagem => "🟢🎲",
                TipoRolagem.Desvantagem => "🔴🎲",
                _ => "🎲"
            };

            // Início da mensagem
            string prefixo = resultado.Tipo switch
            {
                TipoRolagem.Vantagem => "Resultado com vantagem:",
                TipoRolagem.Desvantagem => "Resultado com desvantagem:",
                _ => "Resultado:"
            };

            string mensagem = $"{emoji} {resultado.Expressao}\n{prefixo} {resultado.Detalhes}";

            // Adiciona linha do modificador apenas se diferente de zero
            if (resultado.Modificador != 0)
            {
                string modString = resultado.Modificador > 0
                    ? $"+{resultado.Modificador}"
                    : $"{resultado.Modificador}";

                mensagem += $"\nModificador: {modString}";
            }

            // Linha final com total
            mensagem += $"\nTotal: {resultado.Total}";

            return mensagem;
        }
    }
}
