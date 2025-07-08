using System;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa uma quantidade específica associada a um nível de classe,
    /// usada para indicar, por exemplo, quantidades de magias, truques ou outros recursos que um personagem possui em cada nível.
    /// </summary>
    public class QuantidadePorNivel
    {
        /// <summary>
        /// Chave primária única da entidade.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador da classe a que essa quantidade está associada.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Nível da classe ao qual esta quantidade se refere.
        /// </summary>
        public int Nivel { get; set; }

        /// <summary>
        /// Quantidade associada ao nível da classe.
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Navegação para a entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }
    }
}
