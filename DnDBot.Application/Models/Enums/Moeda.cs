using System;
using System.Collections.Generic;

namespace DnDBot.Application.Models.Enums
{
    /// <summary>
    /// Representa os tipos de moedas usadas em Dungeons & Dragons.
    /// </summary>
    public enum TipoMoeda
    {
        /// <summary>Peça de Cobre, a unidade base.</summary>
        PC,

        /// <summary>Peça de Prata.</summary>
        PP,

        /// <summary>Peça de Electrum.</summary>
        PE,

        /// <summary>Peça de Ouro.</summary>
        PO,

        /// <summary>Peça de Platina.</summary>
        PL
    }

    /// <summary>
    /// Representa uma quantidade de moedas de um determinado tipo.
    /// </summary>
    public class Moeda
    {
        /// <summary>
        /// Identificador único da moeda (requisito do Entity Framework).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo da moeda (ex: PC, PP, PE, PO, PL).
        /// </summary>
        public TipoMoeda Tipo { get; set; }

        /// <summary>
        /// Quantidade dessa moeda.
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Taxas de câmbio relativas à peça de cobre (unidade base para conversão).
        /// </summary>
        private static readonly Dictionary<TipoMoeda, decimal> ValorEmCobre = new()
        {
            { TipoMoeda.PC, 1m },
            { TipoMoeda.PP, 10m },
            { TipoMoeda.PE, 50m },
            { TipoMoeda.PO, 100m },
            { TipoMoeda.PL, 1000m }
        };

        /// <summary>
        /// Inicializa uma nova instância da moeda com o tipo e quantidade especificados.
        /// </summary>
        /// <param name="tipo">Tipo da moeda.</param>
        /// <param name="quantidade">Quantidade da moeda.</param>
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