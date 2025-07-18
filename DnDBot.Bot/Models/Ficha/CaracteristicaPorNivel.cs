﻿using DnDBot.Bot.Models.AntecedenteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha
{
    /// <summary>
    /// Representa uma associação entre uma classe, um nível e uma característica específica
    /// que é adquirida por essa classe ao atingir determinado nível.
    /// </summary>
    public class CaracteristicaPorNivel
    {
        /// <summary>
        /// Nível no qual a característica é adquirida.
        /// </summary>
        public int Nivel { get; set; }

        /// <summary>
        /// Identificador da característica adquirida.
        /// </summary>
        public string CaracteristicaId { get; set; }

        /// <summary>
        /// Referência à entidade da característica associada.
        /// </summary>
        public Caracteristica Caracteristica { get; set; }

        /// <summary>
        /// Identificador da classe à qual a característica pertence.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Referência à entidade da classe associada.
        /// </summary>
        public Classe Classe { get; set; }

        public string AntecedenteId { get; set; }

        public Antecedente Antecedente { get; set; }

        public string RacaId { get; set; }

        public Raca Raca { get; set; }
    }
}
