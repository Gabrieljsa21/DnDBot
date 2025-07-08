using System;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa um requisito necessário para que um personagem possa adquirir uma determinada classe como multiclasse.
    /// Os requisitos normalmente envolvem um valor mínimo em atributos específicos.
    /// </summary>
    public class RequisitoMulticlasse
    {
        /// <summary>
        /// Identificador da classe à qual o requisito se aplica.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Nome do atributo necessário (ex: "Força", "Destreza").
        /// </summary>
        public string Atributo { get; set; }

        /// <summary>
        /// Valor mínimo exigido no atributo para multiclasse.
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// Referência para a classe associada a este requisito.
        /// </summary>
        public Classe Classe { get; set; }
    }
}
