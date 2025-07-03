namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma característica (feature) de uma classe ou subclasse.
    /// </summary>
    public class Caracteristica
    {
        /// <summary>
        /// Identificador único da característica.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da característica.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada da característica.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Fonte de onde a característica foi retirada.
        /// </summary>
        public string Fonte { get; set; }
    }
}

