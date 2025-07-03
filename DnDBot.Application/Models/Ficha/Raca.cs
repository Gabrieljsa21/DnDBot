using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma raça jogável no universo de Dungeons & Dragons.
    /// Contém informações como nome, descrição, sub-raças, fonte de origem e recursos visuais.
    /// </summary>
    public class Raca
    {
        /// <summary>
        /// Identificador único da raça (ex: "elfo", "anao").
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da raça (exemplo: Elfo, Anão, Humano).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição geral e temática da raça.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Fonte oficial onde a raça aparece (exemplo: "Livro do Jogador").
        /// </summary>
        public string Fonte { get; set; }

        /// <summary>
        /// Lista das sub-raças pertencentes a essa raça (ex: Elfo da Floresta, Anão da Colina).
        /// </summary>
        public List<SubRaca> SubRaca { get; set; } = new();

        /// <summary>
        /// URL de um ícone representativo da raça (para exibição em interfaces compactas).
        /// </summary>
        public string IconeUrl { get; set; }

        /// <summary>
        /// URL de uma imagem ilustrativa da raça (mais detalhada ou artística).
        /// </summary>
        public string ImagemUrl { get; set; }
    }
}
