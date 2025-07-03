namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa um idioma que pode ser conhecido ou falado por personagens no jogo.
    /// </summary>
    public class Idioma
    {
        /// <summary>
        /// Identificador único do idioma (ex: "comum", "anão", "élfico").
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do idioma.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do idioma, contendo informações relevantes, como onde é falado ou sua origem.
        /// </summary>
        public string Descricao { get; set; }
    }
}
