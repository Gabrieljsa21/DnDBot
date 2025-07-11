using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DnDBot.Bot.Models.Enums;

namespace DnDBot.Bot.Models.ItensInventario
{
    public class Consumivel : Item
    {
        public int UsosTotais { get; set; } = 1;

        private int _usosRestantes = 1;
        public int UsosRestantes
        {
            get => _usosRestantes;
            set => _usosRestantes = Math.Clamp(value, 0, UsosTotais);
        }

        public string Efeito { get; set; }
        public List<string> CondicoesAplicadas { get; set; } = new();

        public TipoUsoMagia TempoDeUso { get; set; }

        public SubcategoriaItem Subcategoria { get; set; } = SubcategoriaItem.Nenhuma;

        public bool PodeSerUsado() => UsosRestantes > 0;

        public bool Consumir()
        {
            if (UsosRestantes <= 0) return false;

            UsosRestantes--;
            return UsosRestantes > 0;
        }

        public void Recarregar(int quantidade = -1)
        {
            UsosRestantes = (quantidade < 0) ? UsosTotais : Math.Min(quantidade, UsosTotais);
        }

        [NotMapped]
        public string DescricaoCompleta => $"{Nome} — {Efeito} ({UsosRestantes}/{UsosTotais} usos)";
    }
}
