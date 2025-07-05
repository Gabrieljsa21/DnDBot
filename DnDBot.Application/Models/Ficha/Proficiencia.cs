namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma proficiência que um personagem pode ter, como em armas, armaduras,
    /// perícias ou ferramentas.
    /// </summary>
    public class Proficiencia : EntidadeBase
    {

        /// <summary>
        /// Tipo da proficiência (ex: "arma", "armadura", "perícia", "ferramenta").
        /// </summary>
        public string Tipo { get; set; }

    }
}
