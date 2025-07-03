namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa uma proficiência que um personagem pode ter, como em armas, armaduras,
    /// perícias ou ferramentas.
    /// </summary>
    public class Proficiencia
    {
        /// <summary>
        /// Identificador único da proficiência.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da proficiência (ex: "Espadas Longas", "Armadura Pesada", "Furtividade").
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Tipo da proficiência (ex: "arma", "armadura", "perícia", "ferramenta").
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Descrição detalhada da proficiência, explicando seus efeitos e usos.
        /// </summary>
        public string Descricao { get; set; }
    }
}
