using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma ferramenta utilizada por personagens em diversas tarefas e perícias.
    /// </summary>
    public class Ferramenta : EntidadeBase
    {
        /// <summary>
        /// Peso da ferramenta em quilogramas.
        /// </summary>
        public double Peso { get; set; }

        /// <summary>
        /// Custo da ferramenta em moedas de ouro (PO).
        /// </summary>
        public decimal Custo { get; set; }

        /// <summary>
        /// Indica se é uma ferramenta mágica.
        /// </summary>
        public bool EMagica { get; set; }

        /// <summary>
        /// Lista de perícias que a ferramenta pode auxiliar ou está associada.
        /// </summary>
        public List<Pericia> PericiasAssociadas { get; set; } = new();

        /// <summary>
        /// Indica se a ferramenta requer proficiência para uso efetivo.
        /// </summary>
        public bool RequerProficiencia { get; set; }

        /// <summary>
        /// Adiciona uma perícia associada à ferramenta, caso ainda não esteja na lista.
        /// </summary>
        /// <param name="pericia">A perícia a ser adicionada.</param>
        public void AdicionarPericia(Pericia pericia)
        {
            if (!PericiasAssociadas.Contains(pericia))
                PericiasAssociadas.Add(pericia);
        }

        /// <summary>
        /// Remove uma perícia associada da ferramenta.
        /// </summary>
        /// <param name="pericia">A perícia a ser removida.</param>
        public void RemoverPericia(Pericia pericia)
        {
            PericiasAssociadas.Remove(pericia);
        }

        /// <summary>
        /// Verifica se a ferramenta está associada a uma perícia específica.
        /// </summary>
        /// <param name="pericia">A perícia a verificar.</param>
        /// <returns>True se associada, false caso contrário.</returns>
        public bool EstaAssociadaA(Pericia pericia)
        {
            return PericiasAssociadas.Contains(pericia);
        }

    }
}
