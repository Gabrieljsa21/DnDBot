using System.Collections.Generic;

namespace DnDBot.Application.Models.Rolagem
{
    /// <summary>
    /// Representa o resultado detalhado de uma rolagem de dados.
    /// </summary>
    public class ResultadoRolagem
    {
        /// <summary>
        /// Expressão utilizada na rolagem, ex: "2d20+1".
        /// </summary>
        public string Expressao { get; set; }

        /// <summary>
        /// Valores da primeira rolagem (ou única, se for uma rolagem normal).
        /// </summary>
        public List<int> ValoresPrimeiraRolagem { get; set; }

        /// <summary>
        /// Valores da segunda rolagem (somente em vantagem ou desvantagem; nulo em rolagens normais).
        /// </summary>
        public List<int> ValoresSegundaRolagem { get; set; }

        /// <summary>
        /// Modificador aplicado ao resultado total.
        /// </summary>
        public int Modificador { get; set; }

        /// <summary>
        /// Resultado total da rolagem, somando os dados e o modificador.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Tipo da rolagem (normal, vantagem ou desvantagem).
        /// </summary>
        public TipoRolagem Tipo { get; set; }

        /// <summary>
        /// Texto detalhado que pode conter formatação especial (ex: valores tachados na desvantagem).
        /// </summary>
        public string Detalhes { get; set; }
    }
}
