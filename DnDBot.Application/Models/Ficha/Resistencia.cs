namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma resistência a um tipo de dano, reduzindo ou anulando seus efeitos em um personagem.
    /// </summary>
    public class Resistencia : EntidadeBase
    {
        /// <summary>
        /// Tipo de dano ao qual a resistência se aplica (ex: "fogo", "frio", "radiante").
        /// </summary>
        public string TipoDano { get; set; }

    }
}
