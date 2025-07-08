using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using System;
using System.Collections.Generic;

namespace DnDBot.Bot.Models.Ficha
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

        public List<FichaPersonagem> Fichas { get; set; } = new List<FichaPersonagem>();
    }
}
