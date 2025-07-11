using DnDBot.Bot.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class ItemRaca
    {
        public string ItemId { get; set; }
        public string RacaId { get; set; }
        public Item Item { get; set; }
        public Raca Raca { get; set; }
    }
}
