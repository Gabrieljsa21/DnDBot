using System;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa um pergaminho mágico que armazena uma magia para ser usada uma vez.
    /// </summary>
    public class Pergaminho : Consumivel
    {
        /// <summary>
        /// Nome da magia contida no pergaminho.
        /// </summary>
        public string Magia { get; set; } = string.Empty;

        /// <summary>
        /// Nível da magia armazenada.
        /// </summary>
        public int NivelMagia { get; set; } = 0;

        /// <summary>
        /// Indica se o pergaminho foi usado.
        /// </summary>
        public bool Usado { get; private set; } = false;

        /// <summary>
        /// Usa o pergaminho, consumindo-o e aplicando o efeito da magia.
        /// </summary>
        /// <returns>True se o uso foi bem-sucedido, false se já foi usado.</returns>
        public bool Usar()
        {
            if (Usado) return false;
            Usado = true;
            // Aqui você pode adicionar lógica para aplicar o efeito da magia no jogo
            return true;
        }

        /// <summary>
        /// Restaura o pergaminho para o estado não usado (ex: recarga mágica, ou uso limitado).
        /// </summary>
        public void Restaurar()
        {
            Usado = false;
        }
    }
}
