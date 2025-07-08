using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DnDBot.Bot.Models.ItensInventario
{
    /// <summary>
    /// Representa uma ferramenta utilizada por personagens em diversas tarefas e perícias.
    /// </summary>
    public class Ferramenta : Item
    {
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
            set => FerramentaTags = value?.Select(tag => new FerramentaTag { Tag = tag, FerramentaId = Id }).ToList() ?? new();
        }

        /// <summary>
        /// Construtor padrão que inicializa propriedades herdadas do Item.
        /// </summary>
        public Ferramenta()
        {
            // Se quiser definir valores padrão para PesoUnitario, Categoria etc, pode fazer aqui
            Categoria ??= "Ferramenta";
            Empilhavel = false;
            Equipavel = false;
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
