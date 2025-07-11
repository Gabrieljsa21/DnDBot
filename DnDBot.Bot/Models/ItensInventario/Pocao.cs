using DnDBot.Bot.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa uma poção mágica ou alquímica no sistema D&D 5e.
    /// </summary>
    public class Pocao : Consumivel
    {
        /// <summary>
        /// Tipo de efeito de cura causado pela poção (valor fixo, dado, porcentagem).
        /// </summary>
        public TipoCura TipoCura { get; set; }

        /// <summary>
        /// Valor da cura. Pode ser um número (ex: 10), um dado (ex: "2d4+2") ou porcentagem (ex: 50).
        /// </summary>
        public string ValorCura { get; set; }

        /// <summary>
        /// Nome da magia associada à poção (se ela imita uma magia).
        /// </summary>
        public string MagiaAssociada { get; set; }

        /// <summary>
        /// Indica se a poção causa efeitos colaterais.
        /// </summary>
        public bool TemEfeitosColaterais { get; set; }

        /// <summary>
        /// Lista de efeitos colaterais (ex: tontura, lentidão).
        /// </summary>
        public List<string> EfeitosColaterais { get; set; } = new();

        /// <summary>
        /// Descreve a aparência da poção (ex: cor, brilho, aroma).
        /// </summary>
        public string Aparencia { get; set; }

        /// <summary>
        /// Descrição completa da poção, incluindo cura e efeitos colaterais.
        /// </summary>
        [NotMapped]
        public string Resumo
        {
            get
            {
                var efeitos = TemEfeitosColaterais && EfeitosColaterais?.Count > 0
                    ? $" — Colateral: {string.Join(", ", EfeitosColaterais)}"
                    : string.Empty;

                return $"{Nome} — Cura: {ValorCura} ({TipoCura}) — Usos: {UsosRestantes}/{UsosTotais}{efeitos}";
            }
        }

        /// <summary>
        /// Valida se o valor de cura está coerente com o tipo.
        /// </summary>
        public bool ValorCuraValido()
        {
            return TipoCura switch
            {
                TipoCura.Fixo => int.TryParse(ValorCura, out _),
                TipoCura.Dado => ValorCura.Contains("d"),
                TipoCura.Porcentagem => int.TryParse(ValorCura, out int pct) && pct > 0 && pct <= 100,
                _ => false
            };
        }
    }
}
