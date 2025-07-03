namespace DnDBot.Application.Models.Ficha.ClasseAuxiliares
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
    /// Representa a associação entre uma classe e uma proficiência em arma.
    /// </summary>
    public class ClasseProficienciaArma
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da arma.
        /// </summary>
        public string ArmaId { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Referência à entidade Arma relacionada.
        /// </summary>
        public Arma Arma { get; set; }
    }


    /// <summary>
    /// Representa a associação entre uma classe e uma proficiência em armadura.
    /// </summary>
    public class ClasseProficienciaArmadura
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da armadura.
        /// </summary>
        public string ArmaduraId { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }

        /// <summary>
        /// Referência à entidade Armadura relacionada.
        /// </summary>
        public Armadura Armadura { get; set; }
    }

    /// <summary>
    /// Representa a associação entre uma classe e proficiências multiclasse.
    /// Utilizado para definir quais proficiências são adquiridas ao multiclassificar.
    /// </summary>
    public class ClasseProficienciaMulticlasse
    {
        /// <summary>
        /// Identificador da classe.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Identificador da proficiência multiclasse.
        /// </summary>
        public string IdProficiencia { get; set; }

        /// <summary>
        /// Referência à entidade Classe relacionada.
        /// </summary>
        public Classe Classe { get; set; }
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
}
