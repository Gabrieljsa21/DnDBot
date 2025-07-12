using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class EscudoPropriedadeEspecial
    {
        public string EscudoId { get; set; }
        public string Propriedade { get; set; }

        public Escudo Escudo { get; set; }
    }
}
