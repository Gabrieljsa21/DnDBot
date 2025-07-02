using System;
using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa os tipos de moedas usadas em D&D.
    /// </summary>
    public enum TipoMoeda
    {
        PC, // Peça de Cobre
        PP, // Peça de Prata
        PE, // Peça de Electrum
        PO, // Peça de Ouro
        PL  // Peça de Platina
    }

    /// <summary>
    /// Representa uma quantidade de moedas de um determinado tipo.
    /// </summary>
    public class Moeda
    {
        public TipoMoeda Tipo { get; set; }
        public int Quantidade { get; set; }

        /// <summary>
        /// Taxas de câmbio relativas à peça de cobre.
        /// </summary>
        private static readonly Dictionary<TipoMoeda, decimal> ValorEmCobre = new()
        {
            { TipoMoeda.PC, 1m },
            { TipoMoeda.PP, 10m },
            { TipoMoeda.PE, 50m },
            { TipoMoeda.PO, 100m },
            { TipoMoeda.PL, 1000m }
        };

        public Moeda(TipoMoeda tipo, int quantidade)
        {
            Tipo = tipo;
            Quantidade = quantidade;
        }

        /// <summary>
        /// Converte essa moeda para o tipo desejado.
        /// </summary>
        public decimal ConverterPara(TipoMoeda destino)
        {
            decimal valorEmCobre = Quantidade * ValorEmCobre[Tipo];
            return valorEmCobre / ValorEmCobre[destino];
        }

        /// <summary>
        /// Adiciona uma outra moeda (convertendo, se necessário).
        /// </summary>
        public void Adicionar(Moeda outra)
        {
            if (outra == null) return;

            decimal outraConvertida = outra.ConverterPara(Tipo);
            Quantidade += (int)Math.Round(outraConvertida);
        }

        public override string ToString()
        {
            return $"{Quantidade} {Tipo}";
        }
    }
}
