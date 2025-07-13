using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class Item : EntidadeBase
    {
        public double PesoUnitario { get; set; } = 0.0;
        public CategoriaItem Categoria { get; set; }
        public SubcategoriaItem SubCategoria { get; set; }
        public bool Empilhavel { get; set; }
        public int ValorCobre { get; set; }
        public bool Equipavel { get; set; }

        public List<ItemRaca> RacasPermitidas { get; set; } = new();
        public List<AnatomiaPermitida> AnatomiasPermitidas { get; set; } = new();

        public PropriedadesMagicas PropriedadesMagicas { get; set; }
        public RaridadeItem Raridade { get; set; } = RaridadeItem.Comum;
        public string Fabricante { get; set; } = string.Empty;

        [JsonIgnore]
        public ItemMaterial Material { get; set; }

        [JsonPropertyName("Material")]
        public string MaterialId
        {
            get => Material?.MaterialId;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    Material = new ItemMaterial { MaterialId = value };
            }
        }

    }
}