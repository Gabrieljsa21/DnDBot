using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma classe de personagem no jogo, contendo informações sobre proficiências,
    /// magias, subclasses, características, requisitos e itens iniciais.
    /// </summary>
    public class Classe
    {
        /// <summary>
        /// Identificador único da classe (exemplo: "barbaro", "cacador-de-sangue").
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da classe (ex: Bárbaro, Mago).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição geral da classe.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Dado de vida da classe (ex: "1d12").
        /// </summary>
        public string DadoVida { get; set; }

        /// <summary>
        /// Lista de IDs das proficiências em armaduras concedidas pela classe.
        /// </summary>
        public List<string> IdProficienciasArmadura { get; set; } = new();

        /// <summary>
        /// Lista de IDs das proficiências em armas concedidas pela classe.
        /// </summary>
        public List<string> IdProficienciasArmas { get; set; } = new();

        /// <summary>
        /// Lista de IDs das proficiências para multiclasse.
        /// </summary>
        public List<string> IdProficienciasMulticlasse { get; set; } = new();

        /// <summary>
        /// Requisitos mínimos para multiclasse, onde a chave é o atributo e o valor é o mínimo exigido.
        /// </summary>
        public Dictionary<string, int> RequisitosParaMulticlasse { get; set; } = new();

        /// <summary>
        /// Lista de IDs das perícias associadas à classe.
        /// </summary>
        public List<string> IdPericias { get; set; } = new();

        /// <summary>
        /// Lista de IDs das salvaguardas (saving throws) da classe.
        /// </summary>
        public List<string> IdSalvaguardas { get; set; } = new();

        /// <summary>
        /// Fonte de onde a classe foi retirada (ex: Livro do Jogador).
        /// </summary>
        public string Fonte { get; set; }

        /// <summary>
        /// URL da imagem representativa da classe.
        /// </summary>
        public string ImagemUrl { get; set; }

        /// <summary>
        /// URL do ícone da classe.
        /// </summary>
        public string IconeUrl { get; set; }

        /// <summary>
        /// Papel tático da classe (ex: Tanque, Suporte, Dano).
        /// </summary>
        public string PapelTatico { get; set; }

        /// <summary>
        /// ID da habilidade usada para conjuração (ex: Inteligência, Sabedoria).
        /// </summary>
        public string IdHabilidadeConjuracao { get; set; }

        /// <summary>
        /// Indica se a classe usa magias preparadas.
        /// </summary>
        public bool UsaMagiaPreparada { get; set; }

        /// <summary>
        /// IDs das magias disponíveis para a classe.
        /// </summary>
        public List<string> IdMagiasDisponiveis { get; set; } = new();

        /// <summary>
        /// Número de truques conhecidos por nível da classe.
        /// </summary>
        public Dictionary<int, int> TruquesConhecidosPorNivel { get; set; } = new();

        /// <summary>
        /// Número de magias conhecidas por nível da classe.
        /// </summary>
        public Dictionary<int, int> MagiasConhecidasPorNivel { get; set; } = new();

        /// <summary>
        /// Espaços de magia por nível e nível do espaço (ex: nível 1 -> 4 espaços).
        /// </summary>
        public Dictionary<int, Dictionary<string, int>> EspacosMagiaPorNivel { get; set; } = new();

        /// <summary>
        /// Nível no qual a subclasse é escolhida (ex: 3).
        /// </summary>
        public int? SubclassePorNivel { get; set; }

        /// <summary>
        /// Lista de subclasses disponíveis para a classe.
        /// </summary>
        public List<Subclasse> Subclasses { get; set; } = new();

        /// <summary>
        /// Características da classe organizadas por nível.
        /// </summary>
        public Dictionary<int, List<Caracteristica>> CaracteristicasPorNivel { get; set; } = new();

        /// <summary>
        /// Progressão da classe por nível, onde o dicionário interno pode armazenar dados variados.
        /// </summary>
        public Dictionary<int, Dictionary<string, object>> ProgressaoPorNivel { get; set; } = new();

        /// <summary>
        /// IDs dos itens iniciais que a classe possui.
        /// </summary>
        public List<string> IdItensIniciais { get; set; } = new();

        /// <summary>
        /// Riqueza inicial em moedas para a classe.
        /// </summary>
        public List<Moeda> RiquezaInicial { get; set; } = new();

        /// <summary>
        /// Notas adicionais relacionadas à classe.
        /// </summary>
        public List<string> Notas { get; set; } = new();
    }

    /// <summary>
    /// Representa uma subclasse de uma classe, incluindo características específicas por nível.
    /// </summary>
    public class Subclasse
    {
        /// <summary>
        /// Identificador único da subclasse.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da subclasse.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da subclasse.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Fonte de onde a subclasse foi retirada.
        /// </summary>
        public string Fonte { get; set; }

        /// <summary>
        /// URL da imagem representativa da subclasse.
        /// </summary>
        public string ImagemUrl { get; set; }

        /// <summary>
        /// Características específicas da subclasse organizadas por nível.
        /// </summary>
        public Dictionary<int, List<Caracteristica>> CaracteristicasPorNivel { get; set; } = new();
    }

    /// <summary>
    /// Representa uma característica (feature) de uma classe ou subclasse.
    /// </summary>
    public class Caracteristica
    {
        /// <summary>
        /// Identificador único da característica.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da característica.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada da característica.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Fonte de onde a característica foi retirada.
        /// </summary>
        public string Fonte { get; set; }
    }
}
