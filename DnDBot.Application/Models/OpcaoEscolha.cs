using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa um conjunto de opções do tipo T, 
    /// onde o usuário pode escolher uma quantidade limitada.
    /// </summary>
    public class OpcaoEscolha<T>
    {
        /// <summary>
        /// Lista das opções disponíveis para escolha.
        /// </summary>
        public List<T> Opcoes { get; set; } = new();

        /// <summary>
        /// Quantidade máxima de opções que podem ser escolhidas.
        /// </summary>
        public int QuantidadeEscolhas { get; set; } = 1;
    }
}
