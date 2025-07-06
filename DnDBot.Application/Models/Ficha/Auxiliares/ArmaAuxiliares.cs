using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.ItensInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models.Ficha.Auxiliares
{
    /// <summary>
    /// Representa um requisito mínimo de atributo para o uso de uma arma.
    /// Indica qual atributo (Força, Destreza, etc.) e qual valor mínimo necessário para manejar a arma.
    /// </summary>
    public class ArmaRequisitoAtributo
    {
        /// <summary>
        /// Chave primária única para o requisito.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador da arma à qual este requisito pertence.
        /// </summary>
        public string ArmaId { get; set; }

        /// <summary>
        /// Referência para a entidade arma relacionada.
        /// </summary>
        public Arma Arma { get; set; }

        /// <summary>
        /// Atributo requerido para usar a arma (por exemplo, Força ou Destreza).
        /// </summary>
        public Atributo Atributo { get; set; }

        /// <summary>
        /// Valor mínimo necessário do atributo para usar a arma.
        /// </summary>
        public int Valor { get; set; }
    }

    public class ArmaTag
    {
        public string ArmaId { get; set; }
        public string Tag { get; set; }

        public Arma Arma { get; set; }
    }

}
