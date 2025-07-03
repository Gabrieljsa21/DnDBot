using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Enums
{
    /// <summary>
    /// Enumeração que representa os atributos básicos de um personagem em Dungeons & Dragons.
    /// Cada valor corresponde a uma característica fundamental que influencia as habilidades e testes do personagem.
    /// </summary>
    public enum Atributo
    {
        /// <summary>Representa a Força física do personagem, usada para força bruta e ataques corpo a corpo.</summary>
        Forca,

        /// <summary>Representa a agilidade, reflexos e destreza manual.</summary>
        Destreza,

        /// <summary>Representa a saúde, resistência física e vigor.</summary>
        Constituicao,

        /// <summary>Representa o raciocínio, memória e conhecimento do personagem.</summary>
        Inteligencia,

        /// <summary>Representa a percepção, intuição e força de vontade.</summary>
        Sabedoria,

        /// <summary>Representa o carisma, influência e capacidade social.</summary>
        Carisma
    }
}
