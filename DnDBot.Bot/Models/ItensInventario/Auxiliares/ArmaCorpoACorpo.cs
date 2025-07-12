using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario.Auxiliares
{

    public class ArmaCorpoACorpo : Arma
    {
        public int AlcanceEmMetros { get; set; } = 5;
        public bool PodeSerArremessada { get; set; }
        public int? AlcanceArremesso { get; set; }
    }
}
