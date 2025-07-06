using DnDBot.Application.Models.Enums;
using System.Collections.Generic;

namespace DnDBot.Application.Models.ItensInventario
{
    public abstract class Item : EntidadeBase
    {
        public double PesoUnitario { get; init; }
        public string Categoria { get; init; }
        public bool Empilhavel { get; init; }
        public int ValorCobre { get; init; }
        public bool Equipavel { get; init; }

        public List<string> RacasPermitidas { get; set; } = new();
        public List<AnatomiaPermitida> AnatomiasPermitidas { get; set; } = new();

    }
}