using DnDBot.Bot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class EquipamentoItem
    {
        public string Id { get; set; }
        public SlotEquipamento Slot { get; set; }

        // Id do ItemInventario que está equipado
        public string ItemInventarioId { get; set; }
        public ItemInventario ItemInventario { get; set; }

        public string InventarioId { get; set; }
        public Inventario Inventario { get; set; }
    }

}
