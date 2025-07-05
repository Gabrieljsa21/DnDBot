using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade SubRaca para o Entity Framework Core.
    /// Define o mapeamento da classe SubRaca para a tabela no banco de dados,
    /// incluindo chaves, relacionamentos e tabelas intermediárias para relacionamentos muitos-para-muitos.
    /// </summary>
    public class RacaConfiguration : IEntityTypeConfiguration<SubRaca>
    {
        /// <summary>
        /// Configura a entidade SubRaca.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade SubRaca.</param>
        public void Configure(EntityTypeBuilder<SubRaca> entity)
        {
            // Define a propriedade Id como chave primária da tabela SubRaca
            entity.HasKey(sr => sr.Id);

        }
    }
}
