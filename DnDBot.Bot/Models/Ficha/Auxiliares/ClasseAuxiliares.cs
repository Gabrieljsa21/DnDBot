using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.ItensInventario;

namespace DnDBot.Bot.Models.Ficha.Auxiliares
{
    /// <summary>
    /// Representa a associação entre uma classe e uma perícia que ela possui.
    /// </summary>
    public class ClassePericia
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

    public class ClasseMoeda
    {
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }
        public string MoedaId { get; set; }
        public Moeda Moeda { get; set; }

    }

    public class ClasseItens
    {
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }

    }

    public class ClasseProficiencia
    {
        public string ClasseId { get; set; }
        public Classe Classe { get; set; }
        public string ProficienciaId { get; set; }
        public Proficiencia Proficiencia { get; set; }

    }


}
