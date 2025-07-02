using System;

namespace DnDBot.Application.Models.Antecedente.Antecedente
{
    /// <summary>
    /// Representa um vínculo associado a um antecedente de personagem.
    /// Vínculos são conexões ou relacionamentos importantes para o personagem.
    /// </summary>
    public class Vinculo
    {
        /// <summary>
        /// Identificador único do vínculo.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do vínculo.
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada do vínculo.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Identificador do antecedente ao qual este vínculo está associado.
        /// </summary>
        public string IdAntecedente { get; set; }
    }
}
