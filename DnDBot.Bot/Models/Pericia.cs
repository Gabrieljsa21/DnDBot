using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.AntecedenteModels;

namespace DnDBot.Bot.Models
{
    /// <summary>
    /// Representa uma perícia ou habilidade que um personagem pode possuir,
    /// vinculada a um atributo base e que pode ter diferentes tipos.
    /// </summary>
    public class Pericia : EntidadeBase
    {

        /// <summary>
        /// Atributo base usado para testes da perícia.
        /// </summary>
        public Atributo AtributoBase { get; set; }

        /// <summary>
        /// Níveis de dificuldade sugeridos para testes dessa perícia.
        /// </summary>
        public List<DificuldadePericia> Dificuldades { get; set; } = new();

        /// <summary>
        /// Dicionário derivado das dificuldades para acesso rápido por tipo.
        /// </summary>
        [NotMapped]
        public Dictionary<string, int> DificuldadeSugerida =>
            Dificuldades.ToDictionary(d => d.Tipo, d => d.Valor);


    }
}
