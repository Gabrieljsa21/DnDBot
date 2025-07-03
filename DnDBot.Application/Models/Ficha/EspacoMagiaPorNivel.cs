using System;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa a quantidade de espaços de magia disponíveis por nível de classe e nível da magia.
    /// </summary>
    public class EspacoMagiaPorNivel
    {
        /// <summary>
        /// Identificador único do registro (requisito do EF).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Identificador da classe à qual os espaços de magia pertencem.
        /// </summary>
        public string ClasseId { get; set; }

        /// <summary>
        /// Nível da classe (ex: 1, 2, 3... até 20).
        /// </summary>
        public int Nivel { get; set; }

        /// <summary>
        /// Tipo de magia (ex: "nível 1", "nível 2", "truque", etc).
        /// Normalmente indica o nível do espaço de magia.
        /// </summary>
        public string TipoMagia { get; set; }

        /// <summary>
        /// Quantidade de espaços de magia disponíveis para esse nível e tipo.
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Referência à classe associada a este espaço de magia.
        /// </summary>
        public Classe Classe { get; set; }
    }
}
