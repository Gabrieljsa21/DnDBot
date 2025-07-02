using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DnDBot.Application.Models.Rolagem;

namespace DnDBot.Application.Services
{
    /// <summary>
    /// Serviço responsável por realizar rolagens de dados no formato NdX+Y,
    /// onde N é a quantidade de dados, X é o número de lados e Y é um modificador opcional.
    /// </summary>
    public class RolagemDadosService
    {
        private static readonly Regex padraoExpressao = new(@"^(\d*)d(\d+)(\s*[+-]\s*\d+)?$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Realiza uma rolagem normal.
        /// </summary>
        public ResultadoRolagem? Rolar(string expressao)
        {
            var match = padraoExpressao.Match(expressao.Trim());

            if (!match.Success)
                return null;

            int quantidade = string.IsNullOrEmpty(match.Groups[1].Value) ? 1 : int.Parse(match.Groups[1].Value);
            int lados = int.Parse(match.Groups[2].Value);
            int modificador = match.Groups[3].Success
                ? int.Parse(match.Groups[3].Value.Replace(" ", ""))
                : 0;

            var rng = new Random();
            var valores = Enumerable.Range(0, quantidade).Select(_ => rng.Next(1, lados + 1)).ToList();
            int total = valores.Sum() + modificador;

            return new ResultadoRolagem
            {
                Expressao = expressao,
                ValoresPrimeiraRolagem = valores,
                ValoresSegundaRolagem = null,
                Modificador = modificador,
                Total = total,
                Tipo = TipoRolagem.Normal,
                Detalhes = $"({string.Join(", ", valores)})"
            };
        }

        /// <summary>
        /// Realiza uma rolagem com vantagem.
        /// </summary>
        public ResultadoRolagem? RolarVantagem(string expressao)
        {
            return RolarComComparacao(expressao, (a, b) => a >= b, TipoRolagem.Vantagem);
        }

        /// <summary>
        /// Realiza uma rolagem com desvantagem.
        /// </summary>
        public ResultadoRolagem? RolarDesvantagem(string expressao)
        {
            return RolarComComparacao(expressao, (a, b) => a <= b, TipoRolagem.Desvantagem);
        }

        /// <summary>
        /// Rola duas vezes e seleciona o melhor ou pior resultado.
        /// </summary>
        private ResultadoRolagem? RolarComComparacao(string expressao, Func<int, int, bool> comparador, TipoRolagem tipo)
        {
            var r1 = Rolar(expressao);
            var r2 = Rolar(expressao);

            if (r1 == null || r2 == null)
                return null;

            int total1 = r1.Total;
            int total2 = r2.Total;

            var melhor = comparador(total1, total2) ? r1 : r2;
            var pior = comparador(total1, total2) ? r2 : r1;

            string valoresMelhor = $"({string.Join(", ", melhor.ValoresPrimeiraRolagem)})";
            string valoresPior = $"~~({string.Join(", ", pior.ValoresPrimeiraRolagem)})~~";

            string detalhes = $"{valoresMelhor} {valoresPior}";

            return new ResultadoRolagem
            {
                Expressao = expressao,
                ValoresPrimeiraRolagem = melhor.ValoresPrimeiraRolagem,
                ValoresSegundaRolagem = pior.ValoresPrimeiraRolagem,
                Modificador = melhor.Modificador,
                Total = melhor.Total,
                Tipo = tipo,
                Detalhes = detalhes
            };
        }
    }
}
