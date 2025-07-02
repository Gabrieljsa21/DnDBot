namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa um alinhamento moral e ético para um personagem.
    /// Exemplos incluem "Leal e Bom", "Caótico e Mal", etc.
    /// </summary>
    public class Alinhamento
    {
        /// <summary>
        /// Identificador único do alinhamento.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do alinhamento, como "Leal e Bom (LG)".
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada do significado e características do alinhamento.
        /// </summary>
        public string Descricao { get; set; }
    }
}
