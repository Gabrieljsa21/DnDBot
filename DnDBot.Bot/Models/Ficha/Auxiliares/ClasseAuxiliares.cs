using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.ItensInventario;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    public class ClasseOpcaoProficiencia
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da perícia.
        /// </summary>
        public string ProficienciaId { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Referência à entidade Pericia relacionada.
        /// </summary>
        public Proficiencia Proficiencia { get; set; }
    }
    public class ClasseOpcaoPericia
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da perícia.
        /// </summary>
        public string PericiaId { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Referência à entidade Pericia relacionada.
        /// </summary>
        public Pericia Pericia { get; set; }
    }

    /// <summary>
    /// Representa as salvaguardas (saving throws) associadas a uma classe.
    /// </summary>
    public class ClasseSalvaguarda
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da salvaguarda.
        /// </summary>
        public string IdSalvaguarda { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }
    }
    public class ClasseProgressao
    {
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }
        public int Nivel { get; set; }

        public string SubclasseId { get; set; }

        public int BonusProficiencia { get; set; }

        public int TruquesConhecidos { get; set; }
        public int MagiasConhecidas { get; set; }
        public int InvocacoesConhecidas { get; set; } //Bruxo
        public int EspacosMagia { get; set; } //Bruxo
        public int NivelMagia { get; set; } //Bruxo
        public int PontosFeiticaria { get; set; }
        public int PontosFuria { get; set; }
        public int PontosChi { get; set; }
        public int InfusoesConhecidas { get; set; }
        public int ItensInfundidos { get; set; }
        public int SurtoAcao { get; set; }
        public int Indomavel { get; set; }
        public int AtaqueExtra { get; set; }
        public int AtaqueFurtivo { get; set; } // Ladino - Quantidade de dados d6

        public int Espaco1 { get; set; }
        public int Espaco2 { get; set; }
        public int Espaco3 { get; set; }
        public int Espaco4 { get; set; }
        public int Espaco5 { get; set; }
        public int Espaco6 { get; set; }
        public int Espaco7 { get; set; }
        public int Espaco8 { get; set; }
        public int Espaco9 { get; set; }

        public List<ClasseHabilidade> HabilidadesGanhas { get; set; }
    }

    /// <summary>
    /// Representa uma habilidade exclusiva de uma classe, como "Fúria", "Pontos de Ki", etc.
    /// Pode ter uma progressão por nível e uma descrição narrativa.
    /// </summary>
    public class ClasseHabilidade
    {
        /// <summary>
        /// Identificador da habilidade.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome da habilidade.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da habilidade, explicando sua mecânica ou função narrativa.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// ID da classe à qual essa habilidade pertence.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Referência para a classe.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Indica se essa habilidade tem uma progressão numérica por nível.
        /// </summary>
        public bool Escalavel { get; set; }

        /// <summary>
        /// Lista de valores da habilidade por nível (caso seja escalável).
        /// </summary>
        public List<ValorPorNivel> ValoresPorNivel { get; set; } = new();
    }

    /// <summary>
    /// Representa o valor de uma habilidade de classe em um determinado nível.
    /// </summary>
    public class ValorPorNivel
    {
        public int Id { get; set; }

        /// <summary>
        /// Nível ao qual o valor se refere.
        /// </summary>
        public int Nivel { get; set; }

        /// <summary>
        /// Valor numérico da habilidade nesse nível.
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// ID da habilidade associada.
        /// </summary>
        public int ClasseHabilidadeId { get; set; }

        /// <summary>
        /// Referência para a habilidade.
        /// </summary>
        public ClasseHabilidade ClasseHabilidade { get; set; }
    }
    /// <summary>
    /// Representa a associação entre uma classe e magias que ela possui ou pode lançar.
    /// </summary>
    public class ClasseMagia
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da magia.
        /// </summary>
        public string MagiaId { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Referência à entidade Magia relacionada.
        /// </summary>
        public Magia Magia { get; set; }
    }

    public class ClasseTag
    {
        public string ClasseId { get; set; }
        public string Tag { get; set; }

        public Classe Classe { get; set; }
    }


    public class ClasseItemFixo
    {
        // Chave composta: ClasseId + ItemId
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }
        public int? Quantidade { get; set; }
    }

    public class ClasseOpcaoItemGrupo
    {
        public int Id { get; set; }  // chave primária

        public string ClasseId { get; set; }
        public Classe Classe { get; set; }

        // Nome do grupo (ex: "Escolha entre armaduras")
        public string Nome { get; set; }

        // As opções que o jogador pode escolher dentro desse grupo
        public List<ClasseOpcaoItemOpcao> Opcoes { get; set; } = new();
    }

    public class ClasseOpcaoItemOpcao
    {
        public int Id { get; set; }  // chave primária

        public int GrupoId { get; set; }
        public ClasseOpcaoItemGrupo Grupo { get; set; }

        // Nome da opção (ex: "Opção A", "Opção B")
        public string Nome { get; set; }

        // Itens pertencentes a essa opção
        public List<ClasseItemOpcaoItem> Itens { get; set; } = new();
    }

    public class ClasseItemOpcaoItem
    {
        public int Id { get; set; }  // chave primária

        public int OpcaoId { get; set; }
        public ClasseOpcaoItemOpcao Opcao { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
    public class ClasseOpcaoItemBruto
    {
        public string Nome { get; set; }
        public List<string> Itens { get; set; } = new();
    }

    public class ClasseProficiencia
    {
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }
        public string ProficienciaId { get; set; }
        public Proficiencia Proficiencia { get; set; }

    }


}
