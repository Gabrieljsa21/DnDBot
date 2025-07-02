using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma perícia ou habilidade que um personagem pode possuir,
    /// vinculada a um atributo base e que pode ter diferentes tipos.
    /// </summary>
    public class Pericia
    {
        /// <summary>
        /// Identificador único da perícia.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da perícia (ex: "Acrobacia", "Furtividade").
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Atributo base usado para testes da perícia.
        /// </summary>
        public Atributo AtributoBase { get; set; }

        /// <summary>
        /// Tipo da perícia, que pode ser uma habilidade, ferramenta ou perícia de arma.
        /// </summary>
        public TipoPericia Tipo { get; set; } = TipoPericia.Habilidade;

        /// <summary>
        /// Descrição detalhada da perícia.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Indica se o personagem é proficiente nessa perícia.
        /// </summary>
        public bool EhProficiente { get; set; }

        /// <summary>
        /// Bônus base aplicado na perícia (ex: bônus de proficiência).
        /// </summary>
        public int BonusBase { get; set; }

        /// <summary>
        /// URL ou caminho do ícone representativo da perícia.
        /// </summary>
        public string Icone { get; set; }

        /// <summary>
        /// Construtor que inicializa uma nova instância da perícia.
        /// </summary>
        /// <param name="id">Identificador único da perícia.</param>
        /// <param name="nome">Nome da perícia.</param>
        /// <param name="atributoBase">Atributo base para testes.</param>
        /// <param name="descricao">Descrição detalhada (opcional).</param>
        /// <param name="tipo">Tipo da perícia (padrão: Habilidade).</param>
        /// <param name="ehProficiente">Indica se o personagem é proficiente (padrão: false).</param>
        /// <param name="bonusBase">Bônus base aplicado (padrão: 0).</param>
        /// <param name="icone">Ícone da perícia (opcional).</param>
        public Pericia(string id, string nome, Atributo atributoBase, string descricao = "", TipoPericia tipo = TipoPericia.Habilidade, bool ehProficiente = false, int bonusBase = 0, string icone = "")
        {
            Id = id;
            Nome = nome;
            AtributoBase = atributoBase;
            Descricao = descricao;
            Tipo = tipo;
            EhProficiente = ehProficiente;
            BonusBase = bonusBase;
            Icone = icone;
        }

        /// <summary>
        /// Enumeração dos tipos possíveis de perícia.
        /// </summary>
        public enum TipoPericia
        {
            /// <summary>
            /// Perícia relacionada a uma habilidade.
            /// </summary>
            Habilidade,

            /// <summary>
            /// Perícia relacionada a uma ferramenta.
            /// </summary>
            Ferramenta,

            /// <summary>
            /// Perícia relacionada a uma arma.
            /// </summary>
            PericiaDeArma
        }

        /// <summary>
        /// Enumeração dos atributos base do personagem.
        /// </summary>
        public enum Atributo
        {
            Forca,
            Destreza,
            Constituicao,
            Inteligencia,
            Sabedoria,
            Carisma
        }
    }
}
