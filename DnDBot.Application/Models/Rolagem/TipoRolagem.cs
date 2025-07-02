namespace DnDBot.Application.Models.Rolagem
{
    /// <summary>
    /// Representa os tipos de rolagem de dados possíveis.
    /// </summary>
    public enum TipoRolagem
    {
        /// <summary>
        /// Rolagem normal, sem vantagem ou desvantagem.
        /// </summary>
        Normal,

        /// <summary>
        /// Rolagem com vantagem (melhor resultado entre dois lançamentos).
        /// </summary>
        Vantagem,

        /// <summary>
        /// Rolagem com desvantagem (pior resultado entre dois lançamentos).
        /// </summary>
        Desvantagem
    }
}
