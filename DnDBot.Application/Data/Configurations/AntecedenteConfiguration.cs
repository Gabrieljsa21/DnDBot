using DnDBot.Application.Models.AntecedenteModels;
using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Antecedente para o Entity Framework Core.
    /// Define como a classe Antecedente será mapeada para a tabela no banco de dados.
    /// </summary>
    public class AntecedenteConfiguration : IEntityTypeConfiguration<Antecedente>
    {
        /// <summary>
        /// Configura a entidade Antecedente.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade Antecedente.</param>
        public void Configure(EntityTypeBuilder<Antecedente> entity)
        {
            // Define a propriedade Id como chave primária da tabela Antecedente
            entity.HasKey(a => a.Id);

            // Configura a propriedade Nome: obrigatório e com tamanho máximo de 100 caracteres
            entity.Property(a => a.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Configura a coleção de RiquezaInicial como uma entidade própria (Owned Entity)
            entity.OwnsMany(a => a.RiquezaInicial, moedas =>
            {
                // Define a chave estrangeira que liga a riqueza inicial ao antecedente
                moedas.WithOwner().HasForeignKey("AntecedenteId");

                // Define uma chave primária para cada item da riqueza inicial
                moedas.Property<int>("Id");
                moedas.HasKey("Id");

                // Configura o campo Tipo, convertendo o enum para string e tornando-o obrigatório
                moedas.Property(m => m.Tipo)
                      .HasConversion<string>()
                      .IsRequired();

                // Configura o campo Quantidade como obrigatório
                moedas.Property(m => m.Quantidade)
                      .IsRequired();

                // Define o nome da tabela que armazenará os registros de riqueza inicial ligados aos antecedentes
                moedas.ToTable("Antecedente_RiquezaInicial");
            });

            // Configura o relacionamento com AntecedenteTag (tags do antecedente)
            entity.HasMany(a => a.AntecedenteTags)
                  .WithOne(at => at.Antecedente)
                  .HasForeignKey(at => at.AntecedenteId);
        }
    }
}
