namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa um item de equipamento no sistema D&D 5e.
    /// </summary>
    public class Equipamento
    {
        /// <summary>
        /// Identificador único do equipamento (usado internamente).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do equipamento, como "Corda de Cânhamo", "Tocha", "Poção de Cura", etc.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Quantidade desse equipamento que o personagem possui.
        /// </summary>
        public int Quantidade { get; set; }
    }
}
