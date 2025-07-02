using System.Collections.Generic;

namespace DnDBot.Application.Models.Ficha
{
    /// <summary>
    /// Representa uma sub-raça jogável com todos os seus atributos.
    /// </summary>
    public class SubRaca
    {
        /// <summary>
        /// Identificador único da sub-raça (usado internamente).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome da sub-raça (ex: Protetor, Caído, Alto Elfo).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da sub-raça, explicando suas origens ou estilo.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Atributos que recebem bônus ao escolher essa sub-raça (ex: Carisma +2).
        /// </summary>
        public List<BonusAtributo> BonusAtributos { get; set; } = new();

        /// <summary>
        /// Tendências de alinhamento mais comuns entre os membros da sub-raça.
        /// </summary>
        public string TendenciasComuns { get; set; }

        /// <summary>
        /// Tamanho da criatura (geralmente Médio ou Pequeno).
        /// </summary>
        public string Tamanho { get; set; }

        /// <summary>
        /// Deslocamento base em metros (ex: 9 metros).
        /// </summary>
        public int Deslocamento { get; set; }

        /// <summary>
        /// Idiomas que a sub-raça conhece por padrão.
        /// </summary>
        public List<string> Idiomas { get; set; } = new();

        /// <summary>
        /// Proficiências concedidas pela sub-raça (ex: armas, ferramentas, perícias).
        /// </summary>
        public List<string> Proficiencias { get; set; } = new();

        /// <summary>
        /// Alcance da visão no escuro em metros. Use 0 para indicar que a sub-raça não possui visão no escuro.
        /// </summary>
        public int VisaoNoEscuro { get; set; }

        /// <summary>
        /// Tipos de resistência concedidas (ex: resistência a dano radiante).
        /// </summary>
        public List<string> Resistencias { get; set; } = new();

        /// <summary>
        /// Características especiais únicas da sub-raça.
        /// </summary>
        public List<string> Caracteristicas { get; set; } = new();

        /// <summary>
        /// Lista de magias raciais que o personagem pode conjurar.
        /// </summary>
        public List<string> MagiasRaciais { get; set; } = new();

        /// <summary>
        /// URL do ícone representando visualmente a sub-raça.
        /// </summary>
        public string IconeUrl { get; set; }

        /// <summary>
        /// URL da imagem ilustrativa principal da sub-raça.
        /// </summary>
        public string ImagemUrl { get; set; }
    }

}
