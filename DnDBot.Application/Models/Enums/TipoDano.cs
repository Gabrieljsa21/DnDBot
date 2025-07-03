using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Enums
{
    /// <summary>
    /// Enumeração que representa os tipos de dano possíveis em Dungeons & Dragons.
    /// Cada valor corresponde a um tipo de dano que pode ser causado por ataques, magias ou outras fontes.
    /// </summary>
    public enum TipoDano
    {
        /// <summary>Dano causado por armas cortantes, como espadas e machados.</summary>
        Cortante,

        /// <summary>Dano causado por armas perfurantes, como lanças e flechas.</summary>
        Perfurante,

        /// <summary>Dano causado por impacto contundente, como martelos e porrada.</summary>
        Contundente,

        /// <summary>Dano causado por fogo ou calor intenso.</summary>
        Fogo,

        /// <summary>Dano causado por frio intenso ou gelo.</summary>
        Gelo,

        /// <summary>Dano causado por eletricidade ou choque elétrico.</summary>
        Eletrico,

        /// <summary>Dano causado por ácido ou substâncias corrosivas.</summary>
        Acido,

        /// <summary>Dano causado por veneno ou toxinas.</summary>
        Veneno,

        /// <summary>Dano causado por força mágica ou física não convencional.</summary>
        Forca,

        /// <summary>Dano causado por energia radiante, geralmente associada a forças divinas.</summary>
        Radiante,

        /// <summary>Dano causado por energia necromântica ou força negativa.</summary>
        Necrotico,

        /// <summary>Dano psicológico ou mental, como medo ou confusão.</summary>
        Psicologico
    }
}
