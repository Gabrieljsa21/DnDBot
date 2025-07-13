using DnDBot.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class Imunidade : EntidadeBase
    {
        /// <summary>
        /// Tipo de dano ao qual o personagem é completamente imune.
        /// </summary>
        public TipoDano TipoDano { get; set; }
    }
}
