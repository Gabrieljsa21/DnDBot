using DnDBot.Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa o conjunto de moedas carregadas por um personagem.
    /// </summary>
    public class BolsaDeMoedas
    {
        public List<Moeda> Moedas { get; set; } = new();

        public BolsaDeMoedas()
        {
            // Inicializa com zero em todas as moedas
            foreach (TipoMoeda tipo in Enum.GetValues(typeof(TipoMoeda)))
            {
                Moedas.Add(new Moeda(tipo, 0));
            }
        }

        /// <summary>
        /// Retorna o valor total da bolsa de moedas em uma moeda específica.
        /// </summary>
        public decimal ValorTotalEm(TipoMoeda destino)
        {
            return Moedas.Sum(m => m.ConverterPara(destino));
        }

        /// <summary>
        /// Retorna a quantidade de uma moeda específica.
        /// </summary>
        public int GetQuantidade(TipoMoeda tipo)
        {
            return Moedas.FirstOrDefault(m => m.Tipo == tipo)?.Quantidade ?? 0;
        }

        /// <summary>
        /// Adiciona moedas a bolsa de moedas.
        /// </summary>
        public void Adicionar(Moeda moeda)
        {
            var existente = Moedas.FirstOrDefault(m => m.Tipo == moeda.Tipo);
            if (existente != null)
                existente.Quantidade += moeda.Quantidade;
            else
                Moedas.Add(new Moeda(moeda.Tipo, moeda.Quantidade));
        }

        /// <summary>
        /// Remove moedas do tesouro, se houver saldo suficiente.
        /// </summary>
        public bool Remover(Moeda moeda)
        {
            var existente = Moedas.FirstOrDefault(m => m.Tipo == moeda.Tipo);
            if (existente != null && existente.Quantidade >= moeda.Quantidade)
            {
                existente.Quantidade -= moeda.Quantidade;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Exibe as moedas formatadas.
        /// </summary>
        public override string ToString()
        {
            return string.Join(", ", Moedas
                .Where(m => m.Quantidade > 0)
                .Select(m => $"{m.Quantidade} {m.Tipo}"));
        }
    }
}
