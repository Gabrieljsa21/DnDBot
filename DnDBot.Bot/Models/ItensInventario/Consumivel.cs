using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa um item consumível, como poções, pergaminhos, ou kits de uso único.
    /// </summary>
    public class Consumivel : Item
    {
        /// <summary>
        /// Número de usos antes do item se esgotar.
        /// </summary>
        public int UsosTotais { get; set; } = 1;

        /// <summary>
        /// Usos restantes do item.
        /// </summary>
        public int UsosRestantes { get; set; } = 1;

        /// <summary>
        /// Descreve o efeito causado ao consumir este item.
        /// </summary>
        public string Efeito { get; set; }

        /// <summary>
        /// Lista de condições ou efeitos aplicados ao personagem.
        /// </summary>
        public List<string> CondicoesAplicadas { get; set; } = new();

        /// <summary>
        /// Tempo necessário para usar (ex: "Ação", "Bônus", "1 minuto").
        /// </summary>
        public string TempoDeUso { get; set; }

        /// <summary>
        /// Verifica se o consumível ainda pode ser usado.
        /// </summary>
        /// <returns>True se ainda tem usos restantes.</returns>
        public bool PodeSerUsado()
        {
            return UsosRestantes > 0;
        }

        /// <summary>
        /// Reduz o número de usos restantes do item.
        /// </summary>
        /// <returns>True se ainda restam usos, False se esgotou.</returns>
        public bool Consumir()
        {
            if (UsosRestantes <= 0)
                return false;

            UsosRestantes--;
            return UsosRestantes > 0;
        }

        /// <summary>
        /// Restaura os usos do item ao máximo.
        /// </summary>
        public void Recarregar()
        {
            UsosRestantes = UsosTotais;
        }

        /// <summary>
        /// Texto formatado do efeito e usos.
        /// </summary>
        [NotMapped]
        public string DescricaoCompleta => $"{Nome} — {Efeito} ({UsosRestantes}/{UsosTotais} usos)";
    }
}
