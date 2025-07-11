﻿using DnDBot.Bot.Models;
using System;

namespace DnDBot.Bot.Models.AntecedenteModels
{
    /// <summary>
    /// Representa um vínculo associado a um antecedente de personagem.
    /// Vínculos são conexões ou relacionamentos importantes para o personagem.
    /// </summary>
    public class Vinculo : EntidadeBase
    {

        /// <summary>
        /// Identificador do antecedente ao qual este vínculo está associado.
        /// </summary>
        public string AntecedenteId { get; set; }
    }
}
