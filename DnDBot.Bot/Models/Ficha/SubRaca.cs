﻿using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa uma sub-raça jogável com todos os seus atributos.
    /// </summary>
    public class SubRaca : EntidadeBase
    {

        /// <summary>
        /// Atributos que recebem bônus ao escolher essa sub-raça (ex: Carisma +2).
        /// </summary>
        public List<BonusAtributo> BonusAtributos { get; set; } = new();

        /// <summary>
        /// Tendências de alinhamento mais comuns entre os membros da sub-raça.
        /// </summary>
        public List<SubRacaAlinhamento> AlinhamentosComuns { get; set; } = new();

        /// <summary>
        /// Tamanho da criatura (geralmente Médio ou Pequeno).
        /// </summary>
        public TamanhoCriatura Tamanho { get; set; }

        /// <summary>
        /// Deslocamento base em metros (ex: 9 metros).
        /// </summary>
        public int Deslocamento { get; set; }

        /// <summary>
        /// Idiomas que a sub-raça conhece por padrão.
        /// </summary>
        public List<SubRacaIdioma> Idiomas { get; set; } = new();

        /// <summary>
        /// Tipos de resistência concedidas (ex: resistência a dano radiante).
        /// </summary>
        public List<SubRacaResistencia> Resistencias { get; set; } = new();

        /// <summary>
        /// Proficiências concedidas pela sub-raça (ex: armas, ferramentas, perícias).
        /// </summary>
        public List<SubRacaProficiencia> Proficiencias { get; set; } = new();

        /// <summary>
        /// Alcance da visão no escuro em metros. Use 0 para indicar que a sub-raça não possui visão no escuro.
        /// </summary>
        public int VisaoNoEscuro { get; set; }

        /// <summary>
        /// Características especiais únicas da sub-raça.
        /// </summary>
        public List<SubRacaCaracteristica> Caracteristicas { get; set; } = new();

        /// <summary>
        /// Lista de magias raciais que o personagem pode conjurar.
        /// </summary>
        public List<SubRacaMagia> MagiasRaciais { get; set; } = new();

        /// <summary>
        /// ID da raça à qual a sub-raça pertence.
        /// </summary>
        public string RacaId { get; set; }

        /// <summary>
        /// Referência para a raça principal da qual esta sub-raça deriva.
        /// </summary>
        public Raca Raca { get; set; }


        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Raca_Tag.
        /// </summary>
        public List<SubRacaTag> SubRacaTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de RacaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => SubRacaTags?.Select(rt => rt.Tag).ToList() ?? new();
            set => SubRacaTags = value?.Select(tag => new SubRacaTag { Tag = tag, SubRacaId = Id }).ToList() ?? new();
        }
    }
}
