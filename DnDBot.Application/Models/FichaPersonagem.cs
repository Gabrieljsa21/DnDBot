using System;
using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa a ficha de personagem de um jogador.
    /// </summary>
    public class FichaPersonagem
    {
        /// <summary>
        /// Identificador único da ficha.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// ID do jogador no Discord (ulong).
        /// </summary>
        public ulong JogadorId { get; set; }

        /// <summary>
        /// Nome do personagem.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Raça escolhida pelo jogador.
        /// </summary>
        public string Raca { get; set; } = "Não definida";

        /// <summary>
        /// Classe do personagem (ex: Mago, Guerreiro).
        /// </summary>
        public string Classe { get; set; } = "Não definida";

        /// <summary>
        /// Antecedente (background) do personagem.
        /// </summary>
        public string Antecedente { get; set; } = "Não definido";

        /// <summary>
        /// Alinhamento moral e ético do personagem.
        /// </summary>
        public string Alinhamento { get; set; } = "Não definido";

        // Atributos básicos de personagem

        /// <summary>
        /// Atributo de Força.
        /// </summary>
        public int Forca { get; set; }

        /// <summary>
        /// Atributo de Destreza.
        /// </summary>
        public int Destreza { get; set; }

        /// <summary>
        /// Atributo de Constituição.
        /// </summary>
        public int Constituicao { get; set; }

        /// <summary>
        /// Atributo de Inteligência.
        /// </summary>
        public int Inteligencia { get; set; }

        /// <summary>
        /// Atributo de Sabedoria.
        /// </summary>
        public int Sabedoria { get; set; }

        /// <summary>
        /// Atributo de Carisma.
        /// </summary>
        public int Carisma { get; set; }

        /// <summary>
        /// Lista de proficiências adquiridas pelo personagem.
        /// </summary>
        public List<string> Proficiencias { get; set; } = new();
    }
}
