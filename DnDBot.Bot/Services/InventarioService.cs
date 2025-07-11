using DnDBot.Bot.Models.Enums;
using DnDBot.Bot.Models.ItensInventario;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
using DnDBot.Bot.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDBot.Bot.Services
{
    public class InventarioService
    {
        private readonly IFichaRepository _fichaRepository;

        public InventarioService(IFichaRepository fichaRepository)
        {
            _fichaRepository = fichaRepository;
        }

        /// <summary>
        /// Obtém o inventário de uma ficha pelo seu ID.
        /// </summary>
        public async Task<Inventario> ObterInventarioAsync(Guid fichaId)
        {
            var ficha = await _fichaRepository.ObterFichaPorIdAsync(fichaId);
            return ficha?.Inventario;
        }

        /// <summary>
        /// Adiciona um item ao inventário da ficha.
        /// </summary>
        public async Task<bool> AdicionarItemAsync(Guid fichaId, Item item, int quantidade)
        {
            var ficha = await _fichaRepository.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) return false;

            var inventario = ficha.Inventario ?? new Inventario();
            var sucesso = inventario.AdicionarItem(item, quantidade);

            if (!sucesso) return false;

            // Salvar alterações no repositório
            await _fichaRepository.AtualizarInventarioAsync(fichaId, inventario);
            return true;
        }

        /// <summary>
        /// Remove um item do inventário da ficha.
        /// </summary>
        public async Task<bool> RemoverItemAsync(Guid fichaId, string itemId, int quantidade)
        {
            var ficha = await _fichaRepository.ObterFichaPorIdAsync(fichaId);
            if (ficha == null) return false;

            var inventario = ficha.Inventario;
            if (inventario == null) return false;

            var sucesso = inventario.RemoverItem(itemId, quantidade);
            if (!sucesso) return false;

            await _fichaRepository.AtualizarInventarioAsync(fichaId, inventario);
            return true;
        }

        /// <summary>
        /// Retorna uma lista de itens no inventário, opcionalmente filtrando por categoria.
        /// </summary>
        public async Task<IEnumerable<InventarioItem>> ListarItensAsync(Guid fichaId, string categoria = null)
        {
            var ficha = await _fichaRepository.ObterFichaPorIdAsync(fichaId);
            var inventario = ficha?.Inventario;
            if (inventario == null) return new List<InventarioItem>();

            if (string.IsNullOrEmpty(categoria))
                return inventario.Itens;

            return inventario.ListarPorCategoria(categoria);
        }

        public async Task<Dictionary<SlotEquipamento, InventarioItem>> ObterEquipamentosEquipadosAsync(Guid fichaId)
        {
            var ficha = await _fichaRepository.ObterFichaPorIdAsync(fichaId);
            var inventario = ficha?.Inventario;

            if (inventario == null || inventario.Equipados == null)
                return new();

            // Converte a lista Equipados para Dictionary
            return inventario.Equipados
                .Where(e => e.ItemInventario != null) // se quiser garantir que não tenha nulos
                .ToDictionary(e => e.Slot, e => e.ItemInventario);
        }


    }
}
