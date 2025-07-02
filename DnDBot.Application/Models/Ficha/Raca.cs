using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma raça jogável do D&D.
    /// </summary>
    public class Raca
    {
        public string Id { get; set; }

        /// <summary>
        /// Nome da raça (ex: Elfo, Anão).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição breve da raça.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Fonte da raça (ex: Livro do Jogador).
        /// </summary>
        public string Fonte { get; set; }

        /// <summary>
        /// Lista de sub-raças completas pertencentes a essa raça.
        /// </summary>
        public List<SubRaca> SubRacas { get; set; } = new();

        /// <summary>
        /// URL do ícone da raça.
        /// </summary>
        public string IconeUrl { get; set; }

        /// <summary>
        /// URL da imagem principal da raça.
        /// </summary>
        public string ImagemUrl { get; set; }
    }
}
