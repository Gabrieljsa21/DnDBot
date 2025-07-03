namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma resistência a um tipo de dano, reduzindo ou anulando seus efeitos em um personagem.
    /// </summary>
    public class Resistencia
    {
        /// <summary>
        /// Identificador único da resistência.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da resistência (ex: Resistência ao Fogo, Resistência Radiante).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Tipo de dano ao qual a resistência se aplica (ex: "fogo", "frio", "radiante").
        /// </summary>
        public string TipoDano { get; set; }

        /// <summary>
        /// Descrição detalhada da resistência, incluindo efeitos e condições especiais.
        /// </summary>
        public string Descricao { get; set; }
    }
}
