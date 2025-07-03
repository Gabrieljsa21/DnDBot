using System;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa o grau de dificuldade associado a uma perícia específica.
    /// Usado para definir testes de habilidade com níveis como "Fácil", "Médio", etc.
    /// </summary>
    public class DificuldadePericia
    {
        /// <summary>
        /// Identificador único da dificuldade/perícia (requisito do EF).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo da dificuldade (ex: "Fácil", "Médio", "Difícil").
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Valor numérico da CD (Classe de Dificuldade) associada ao tipo.
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// ID da perícia à qual esta dificuldade está associada.
        /// </summary>
        public string PericiaId { get; set; }

        /// <summary>
        /// Referência à entidade da perícia relacionada.
        /// </summary>
        public Pericia Pericia { get; set; }
    }
}
