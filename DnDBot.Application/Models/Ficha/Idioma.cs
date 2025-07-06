using DnDBot.Application.Models.Enums;
using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa um idioma em Dungeons & Dragons 5e.
    /// </summary>
    public class Idioma : EntidadeBase
    {

        /// <summary>
        /// Categoria do idioma:
        /// - Standard
        /// - Exotic
        /// - Dialeto
        /// - Secreto
        /// - TelepaticoOuMagico
        /// - RegionalOuCultural
        /// - BestialOuPictografico
        /// - ArtificialOuConstruto
        /// </summary>
        public CategoriaIdioma Categoria { get; set; }

    }
}
