using Discord.Interactions;
using System.ComponentModel.DataAnnotations;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Representa a entrada do modal de criação de personagem (etapa 1).
    /// </summary>
    public class ModalFichaNomeInput : IModal
    {
        /// <summary>
        /// Título exibido no cabeçalho do modal.
        /// </summary>
        public string Title => "Criar Personagem - Parte 1";

        /// <summary>
        /// Nome do personagem, fornecido pelo usuário via input do modal.
        /// </summary>
        [Required]
        [InputLabel("Nome do personagem")]
        [ModalTextInput("nome_personagem", placeholder: "Ex: Zephyr", maxLength: 100)]
        public string Nome { get; set; }
    }
}
