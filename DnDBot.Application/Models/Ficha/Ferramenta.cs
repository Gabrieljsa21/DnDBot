using DnDBot.Application.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        /// Relacionamento com as tags armazenadas na tabela Ferramenta_Tag.
        /// </summary>
        public List<FerramentaTag> FerramentaTags { get; set; } = new();

        /// <summary>
        /// Tags derivadas da lista de FerramentaTags, útil para facilitar acesso.
        /// </summary>
        [NotMapped]
        public List<string> Tags
        {
            get => FerramentaTags?.Select(ft => ft.Tag).ToList() ?? new();
            set => FerramentaTags = value?.Select(tag => new FerramentaTag { Tag = tag, FerramentaId = this.Id }).ToList() ?? new();
        }


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
