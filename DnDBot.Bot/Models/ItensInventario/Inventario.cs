using DnDBot.Bot.Models.Ficha;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa o inventário completo de um personagem em D&D 5e.
    /// </summary>
    public class Inventario : EntidadeBase
    {
        private readonly List<ItemInventario> itens = new();
        public IReadOnlyList<ItemInventario> Itens => itens.AsReadOnly();
        public List<EquipamentoItem> Equipados { get; set; } = new();

        public double PesoMaximo { get; set; } = 50.0;

        // Propriedade de chave estrangeira para FichaPersonagem
        public Guid FichaPersonagemId { get; set; }

        // Propriedade de navegação (precisa existir para configurar o relacionamento 1:1)
        public FichaPersonagem FichaPersonagem { get; set; }

        public double PesoAtual => itens.Sum(i => i.PesoTotal);

        public bool PodeAdicionarItem(Item item, int quantidade)
        {
            double pesoNovo = item.PesoUnitario * quantidade;
            return PesoAtual + pesoNovo <= PesoMaximo;
        }

        public bool AdicionarItem(Item item, int quantidade)
        {
            if (!PodeAdicionarItem(item, quantidade))
                return false;

            var existente = itens.FirstOrDefault(i => i.ItemBase.Id == item.Id);
            if (existente != null && item.Empilhavel)
                existente.Adicionar(quantidade);
            else
                itens.Add(new ItemInventario(item, quantidade));

            return true;
        }

        public bool RemoverItem(string itemId, int quantidade)
        {
            var item = itens.FirstOrDefault(i => i.ItemBase.Id == itemId);
            if (item == null || item.Quantidade < quantidade)
                return false;

            item.Remover(quantidade);
            if (item.Quantidade <= 0)
                itens.Remove(item);

            return true;
        }

        public IEnumerable<ItemInventario> ListarPorCategoria(string categoria)
        {
            return itens.Where(i => i.ItemBase.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
        }

        public void LimparInventario()
        {
            itens.Clear();
        }


        public ItemInventario ObterItem(string itemId)
        {
            return itens.FirstOrDefault(i => i.ItemBase.Id == itemId);
        }

        public bool PossuiItem(string itemId, int quantidade = 1)
        {
            var item = ObterItem(itemId);
            return item != null && item.Quantidade >= quantidade;
        }
    }
}
