using DnDBot.Application.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma classe de personagem no jogo, contendo informações sobre proficiências,
    /// magias, subclasses, características, requisitos e itens iniciais.
    /// </summary>
    public class Classe : EntidadeBase
    {

        /// <summary>
        /// Dado de vida da classe (ex: "1d12").
        /// </summary>
        public string DadoVida { get; set; }

        /// <summary>
        /// Lista de proficiências em armaduras concedidas pela classe.
        /// </summary>
        [NotMapped]
        public List<Proficiencia> ProficienciasArmadura { get; set; } = new();

        /// <summary>
        /// Lista de proficiências em armas concedidas pela classe.
        /// </summary>
        [NotMapped]
        public List<Proficiencia> ProficienciasArmas { get; set; } = new();

        /// <summary>
        /// Lista de proficiências específicas de multiclasse.
        /// </summary>
        [NotMapped]
        public List<Proficiencia> ProficienciasMulticlasse { get; set; } = new();

        /// <summary>
        /// Requisitos mínimos para multiclassing (atributo e valor mínimo).
        /// </summary>
        [NotMapped]
        public List<RequisitoMulticlasse> RequisitosParaMulticlasseEntities { get; set; }

        /// <summary>
        /// Perícias associadas à classe.
        /// </summary>
        public virtual ICollection<Pericia> PericiasRelacionadas { get; set; } = new List<Pericia>();

        /// <summary>
        /// Lista de identificadores das salvaguardas da classe.
        /// </summary>
        public List<string> IdSalvaguardas { get; set; } = new();


        /// <summary>
        /// Papel tático da classe (ex: Tanque, Suporte, Dano).
        /// </summary>
        public string PapelTatico { get; set; }

        /// <summary>
        /// ID da habilidade usada como atributo chave para conjuração (ex: Inteligência).
        /// </summary>
        public string IdHabilidadeConjuracao { get; set; }

        /// <summary>
        /// Indica se a classe usa magias preparadas.
        /// </summary>
        public bool UsaMagiaPreparada { get; set; }

        /// <summary>
        /// Lista de magias disponíveis para a classe.
        /// </summary>
        [NotMapped]
        public List<Magia> MagiasDisponiveis { get; set; } = new();

        /// <summary>
        /// Número de truques conhecidos por nível.
        /// </summary>
        public List<QuantidadePorNivel> TruquesConhecidosPorNivelList { get; set; } = new();

        /// <summary>
        /// Número de magias conhecidas por nível.
        /// </summary>
        public List<QuantidadePorNivel> MagiasConhecidasPorNivelList { get; set; } = new();

        /// <summary>
        /// Quantidade de espaços de magia disponíveis por nível.
        /// </summary>
        public List<EspacoMagiaPorNivel> EspacosMagia { get; set; }

        /// <summary>
        /// Nível em que o personagem escolhe sua subclasse.
        /// </summary>
        public int? SubclassePorNivel { get; set; }

        /// <summary>
        /// Subclasses associadas à classe.
        /// </summary>
        public List<Subclasse> Subclasses { get; set; } = new();

        /// <summary>
        /// Lista de características concedidas por nível.
        /// </summary>
        public List<CaracteristicaPorNivel> CaracteristicasPorNivelList { get; set; } = new();

        /// <summary>
        /// Mapeamento de características por nível.
        /// </summary>
        [NotMapped]
        public Dictionary<int, List<Caracteristica>> CaracteristicasPorNivel =>
            CaracteristicasPorNivelList
                .GroupBy(x => x.Nivel)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(cn => cn.Caracteristica).ToList()
                );

        /// <summary>
        /// Informações adicionais sobre a progressão da classe por nível.
        /// </summary>
        [NotMapped]
        public Dictionary<int, Dictionary<string, object>> ProgressaoPorNivel { get; set; } = new();

        /// <summary>
        /// Lista dos IDs dos itens iniciais da classe.
        /// </summary>
        public List<string> IdItensIniciais { get; set; } = new();

        /// <summary>
        /// Riqueza inicial da classe, em moedas.
        /// </summary>
        public List<Moeda> RiquezaInicial { get; set; } = new();

    }

    /// <summary>
    /// Representa uma subclasse de uma classe, incluindo características específicas por nível.
    /// </summary>
    public class Subclasse : EntidadeBase
    {

        /// <summary>
        /// Lista de características obtidas por nível.
        /// </summary>
        public List<CaracteristicaPorNivel> CaracteristicasPorNivelList { get; set; } = new();

        /// <summary>
        /// Mapeamento das características da subclasse por nível.
        /// </summary>
        [NotMapped]
        public Dictionary<int, List<Caracteristica>> CaracteristicasPorNivel =>
            CaracteristicasPorNivelList
                .GroupBy(x => x.Nivel)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(cn => cn.Caracteristica).ToList()
                );
    }
}
