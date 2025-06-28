using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por realizar rolagens de dados no formato NdX+Y, 
    /// onde N é a quantidade de dados, X é o número de lados, e Y é o modificador opcional.
    /// </summary>
    public class RolagemDadosService
    {
        // Expressão regular que valida expressões como "2d6+3", "d20", "4d8-1"
        private static readonly Regex padraoExpressao = new(@"^(\d*)d(\d+)([+-]\d+)?$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Realiza a rolagem de dados conforme a expressão informada.
        /// </summary>
        /// <param name="expressao">Expressão da rolagem no formato NdX+Y (exemplo: "2d6+3").</param>
        /// <returns>
        /// Tupla contendo o total da rolagem e o detalhamento dos dados rolados,
        /// ou null caso a expressão seja inválida.
        /// </returns>
        public (int total, string detalhes)? Rolar(string expressao)
        {
            var correspondencia = padraoExpressao.Match(expressao.Trim());

            if (!correspondencia.Success) // Expressão inválida
                return null;

            // Quantidade de dados a rolar; se não especificado, assume 1
            int quantidade = string.IsNullOrEmpty(correspondencia.Groups[1].Value)
                ? 1
                : int.Parse(correspondencia.Groups[1].Value);

            // Número de lados de cada dado
            int lados = int.Parse(correspondencia.Groups[2].Value);

            // Modificador que será somado ao resultado total (pode ser positivo ou negativo)
            int modificador = correspondencia.Groups[3].Success
                ? int.Parse(correspondencia.Groups[3].Value)
                : 0;

            var resultados = new List<int>();
            var aleatorio = new Random();

            // Rola os dados e armazena cada resultado
            for (int i = 0; i < quantidade; i++)
                resultados.Add(aleatorio.Next(1, lados + 1));

            int total = resultados.Sum() + modificador;

            // Cria uma string detalhando as rolagens e o modificador aplicado
            string detalhes = $"[{string.Join(" + ", resultados)}] {(modificador >= 0 ? "+" : "-")} {Math.Abs(modificador)}";

            return (total, detalhes);
        }
    }
}
