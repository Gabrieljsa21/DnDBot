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
        /// Descrição completa da poção.
        /// </summary>
        [NotMapped]
        public string Resumo => $"{Nome} — Cura: {ValorCura} ({TipoCura}) — Usos: {UsosRestantes}/{UsosTotais}";
    }

    /// <summary>
    /// Enumeração para indicar o tipo de cura aplicada por uma poção.
    /// </summary>
    public enum TipoCura
    {
        /// <summary>Valor numérico fixo (ex: 10 PV).</summary>
        Fixo,

        /// <summary>Rolagem de dado (ex: 2d4+2).</summary>
        Dado,

        /// <summary>Porcentagem do total de PV.</summary>
        Porcentagem
    }
}
