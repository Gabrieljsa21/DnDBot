using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.Ficha
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
        public List<ClasseProficiencia> Proficiencias { get; set; } = new();

        /// <summary>
        /// Perícias associadas à classe.
        /// </summary>
        public virtual ICollection<ClassePericia> PericiasRelacionadas { get; set; } = new List<ClassePericia>();

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
        public List<ClasseMagia> MagiasDisponiveis { get; set; } = new();

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
        public List<ClasseItens> IdItensIniciais { get; set; } = new();

        /// <summary>
        /// Riqueza inicial da classe, em moedas.
        /// </summary>
        public List<ClasseMoeda> Moedas { get; set; } = new();

        /// <summary>
        /// Relacionamento com as tags armazenadas na tabela Classe_Tag.
        /// </summary>
        public List<ClasseTag> ClasseTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de ClasseTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => ClasseTags?.Select(ct => ct.Tag).ToList() ?? new();
            set => ClasseTags = value?.Select(tag => new ClasseTag { Tag = tag, ClasseId = Id }).ToList() ?? new();
        }

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
