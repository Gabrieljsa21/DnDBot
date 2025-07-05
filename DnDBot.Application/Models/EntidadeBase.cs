using System;
using System.Collections.Generic;

namespace DnDBot.Application.Models
{
    /// <summary>
    /// Classe base abstrata que representa uma entidade genérica com campos comuns
    /// como identificação, descrição, origem, controle de versão e metadados de auditoria.
    /// Pode ser herdada por outras entidades como Classe, Raça, Antecedente, etc.
    /// </summary>
    public abstract class EntidadeBase
    {
        /// <summary>
        /// Identificador único da entidade (ex: "guerreiro", "elfo").
        /// Usado para buscas e referências internas.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Nome legível da entidade, exibido para os usuários (ex: "Guerreiro", "Elfo").
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada da entidade, contendo informações de lore, regras ou características.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Fonte oficial ou suplemento de onde a entidade foi retirada (ex: "Livro do Jogador").
        /// </summary>
        public string Fonte { get; set; } = string.Empty;

        /// <summary>
        /// Página da fonte oficial onde a entidade está descrita.
        /// </summary>
        public string Pagina { get; set; } = string.Empty;

        /// <summary>
        /// Versão da entidade, útil para controle de revisões ou atualizações no conteúdo.
        /// </summary>
        public string Versao { get; set; } = string.Empty;

        /// <summary>
        /// URL de uma imagem ilustrativa relacionada à entidade.
        /// Pode ser usada em embeds, cards visuais ou sistemas de pré-visualização.
        /// </summary>
        public string ImagemUrl { get; set; } = string.Empty;

        /// <summary>
        /// URL de um ícone representativo da entidade.
        /// Pode ser usado em listas ou menus compactos.
        /// </summary>
        public string IconeUrl { get; set; } = string.Empty;

        /// <summary>
        /// Nome ou ID do usuário que criou o registro da entidade.
        /// </summary>
        public string CriadoPor { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora da criação do registro da entidade.
        /// </summary>
        public DateTime? CriadoEm { get; set; }

        /// <summary>
        /// Nome ou ID do usuário que realizou a última modificação no registro.
        /// </summary>
        public string ModificadoPor { get; set; } = string.Empty;

        /// <summary>
        /// Data e hora da última modificação registrada.
        /// </summary>
        public DateTime? ModificadoEm { get; set; }

    }
}
