using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

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
        /// Lista de atributos alternativos que podem ser usados para testes.
        /// </summary>
        public List<Atributo> AtributosAlternativos { get; set; }

        /// <summary>
        /// Tipo da perícia (habilidade, ferramenta, perícia de arma).
        /// </summary>
        public TipoPericia Tipo { get; set; } = TipoPericia.Habilidade;

        /// <summary>
        /// Descrição detalhada da perícia.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Indica se o personagem é proficiente nessa perícia.
        /// </summary>
        public bool EhProficiente { get; set; } = false;

        /// <summary>
        /// Indica se o personagem possui especialização (dobro do bônus de proficiência).
        /// </summary>
        public bool TemEspecializacao { get; set; } = false;

        /// <summary>
        /// Bônus base aplicado na perícia (ex: bônus de proficiência).
        /// </summary>
        public int BonusBase { get; set; } = 0;

        /// <summary>
        /// Bônus adicional, podendo vir de talentos, magias ou itens.
        /// </summary>
        public int BonusAdicional { get; set; } = 0;

        /// <summary>
        /// Coleção de classes que podem estar relacionadas a essa perícia.
        /// </summary>
        public virtual ICollection<Classe> ClassesRelacionadas { get; set; } = new List<Classe>();

        /// <summary>
        /// Tags para categorização e filtros (ex: "exploração", "combate").
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Níveis de dificuldade sugeridos para testes dessa perícia.
        /// </summary>
        public List<DificuldadePericia> Dificuldades { get; set; } = new();

        /// <summary>
        /// Dicionário derivado das dificuldades para acesso rápido por tipo.
        /// </summary>
        [NotMapped]
        public Dictionary<string, int> DificuldadeSugerida =>
            Dificuldades.ToDictionary(d => d.Tipo, d => d.Valor);

        /// <summary>
        /// URL ou caminho do ícone representativo da perícia.
        /// </summary>
        public string Icone { get; set; }

        /// <summary>
        /// Valor total calculado para essa perícia (atributo + bônus + proficiência).
        /// </summary>
        [JsonIgnore]
        public int ValorTotal { get; set; }

        /// <summary>
        /// Enumeração dos tipos possíveis de perícia.
        /// </summary>
        public enum TipoPericia
        {
            Habilidade,
            Ferramenta,
            PericiaDeArma
        }
    }
}
