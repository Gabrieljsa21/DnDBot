using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
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
        public List<InventarioItem> Itens { get; set; } = new();
        public List<EquipamentoItem> Equipados { get; set; } = new();

        public double PesoMaximo { get; set; } = 50.0;

        // Propriedade de chave estrangeira para FichaPersonagem
        public Guid FichaPersonagemId { get; set; }

        // Propriedade de navegação (precisa existir para configurar o relacionamento 1:1)
        public FichaPersonagem FichaPersonagem { get; set; }

        public double PesoAtual => Itens.Sum(i => i.PesoTotal);

        public bool PodeAdicionarItem(Item item, int quantidade)
        {
            double? pesoNovo = item.PesoUnitario * quantidade;
            return PesoAtual + pesoNovo <= PesoMaximo;
        }

        public bool AdicionarItem(Item item, int quantidade)
        {
            if (!PodeAdicionarItem(item, quantidade))
                return false;

            var existente = Itens.FirstOrDefault(i => i.ItemBase.Id == item.Id);
            if (existente != null && item.Empilhavel)
                existente.Adicionar(quantidade);
            else
                Itens.Add(new InventarioItem(item, quantidade));

            return true;
        }

        public bool RemoverItem(string itemId, int quantidade)
        {
            var item = Itens.FirstOrDefault(i => i.ItemBase.Id == itemId);
            if (item == null || item.Quantidade < quantidade)
                return false;

            item.Remover(quantidade);
            if (item.Quantidade <= 0)
                Itens.Remove(item);

            return true;
        }

        public IEnumerable<InventarioItem> ListarPorCategoria(string categoria)
        {
            if (!Enum.TryParse<CategoriaItem>(categoria, ignoreCase: true, out var categoriaEnum))
                return Enumerable.Empty<InventarioItem>();

            return Itens.Where(i => i.ItemBase.Categoria == categoriaEnum);
        }


        public void LimparInventario()
        {
            Itens.Clear();
        }


        public InventarioItem ObterItem(string itemId)
        {
            return Itens.FirstOrDefault(i => i.ItemBase.Id == itemId);
        }

        public bool PossuiItem(string itemId, int quantidade = 1)
        {
            var item = ObterItem(itemId);
            return item != null && item.Quantidade >= quantidade;
        }
    }
}
