using DnDBot.Bot.Models;
using System;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa um item armazenado no inventário do personagem, com quantidade.
    /// </summary>
    public class ItemInventario : EntidadeBase
    {
        /// <summary>
        /// Construtor sem parâmetros obrigatório para o Entity Framework.
        /// </summary>
        public ItemInventario() { }

        /// <summary>
        /// Construtor principal.
        /// </summary>
        public ItemInventario(Item item, int quantidade)
        {
            ItemBase = item ?? throw new ArgumentNullException(nameof(item));
            Quantidade = quantidade > 0 ? quantidade : throw new ArgumentOutOfRangeException(nameof(quantidade));
        }

        /// <summary>
        /// Referência ao item base (arma, armadura, poção, etc).
        /// </summary>
        public Item ItemBase { get; set; }  // <- set público para EF

        /// <summary>
        /// Quantidade de itens armazenados.
        /// </summary>
        public int Quantidade { get; set; } // <- set público para EF

        /// <summary>
        /// Peso total calculado com base na quantidade.
        /// </summary>
        public double PesoTotal => Math.Round(ItemBase?.PesoUnitario * Quantidade ?? 0, 2);

        /// <summary>
        /// Aumenta a quantidade do item.
        /// </summary>
        public void Adicionar(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantidade));

            Quantidade += quantidade;
        }

        /// <summary>
        /// Remove certa quantidade do item.
        /// </summary>
        public bool Remover(int quantidade)
        {
            if (quantidade <= 0 || quantidade > Quantidade)
                return false;

            Quantidade -= quantidade;
            return true;
        }

        /// <summary>
        /// Indica se o item está esgotado (quantidade 0).
        /// </summary>
        public bool EstaVazio() => Quantidade <= 0;
    }
}
