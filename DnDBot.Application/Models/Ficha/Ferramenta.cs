using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma ferramenta utilizada por personagens em diversas tarefas e perícias.
    /// </summary>
    public class Ferramenta
    {
        /// <summary>
        /// Identificador único da ferramenta.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da ferramenta (ex: "Kit de ladrão", "Kit de ferreiro").
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição detalhada da ferramenta, incluindo usos e efeitos.
        /// </summary>
        public string Descricao { get; set; }

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
        /// Tags para organização e filtros (ex: "artesão", "furtividade").
        /// </summary>
        public List<string> Tags { get; set; } = new();

        /// <summary>
        /// Ícone ou URL para imagem da ferramenta.
        /// </summary>
        public string Icone { get; set; }

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

        /// <summary>
        /// Adiciona uma tag para organização da ferramenta.
        /// </summary>
        /// <param name="tag">A tag a ser adicionada.</param>
        public void AdicionarTag(string tag)
        {
            if (!Tags.Contains(tag))
                Tags.Add(tag);
        }

        /// <summary>
        /// Remove uma tag da ferramenta.
        /// </summary>
        /// <param name="tag">A tag a ser removida.</param>
        public void RemoverTag(string tag)
        {
            Tags.Remove(tag);
        }

        /// <summary>
        /// Lista todas as tags associadas à ferramenta em formato string.
        /// </summary>
        /// <returns>String com as tags separadas por vírgula ou "Sem tags" caso não haja nenhuma.</returns>
        public string ListarTags()
        {
            return Tags.Count == 0 ? "Sem tags" : string.Join(", ", Tags);
        }
    }
}
