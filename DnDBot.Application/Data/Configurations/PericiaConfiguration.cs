using DnDBot.Application.Models;
using DnDBot.Application.Models.Enums;
using DnDBot.Application.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text.Json;

namespace DnDBot.Application.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Pericia para o Entity Framework Core.
    /// Define o mapeamento da classe Pericia para a tabela no banco de dados,
    /// incluindo propriedades, relacionamentos e conversões de dados.
    /// </summary>
    public class PericiaConfiguration : IEntityTypeConfiguration<Pericia>
    {
        /// <summary>
        /// Configura a entidade Pericia.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade Pericia.</param>
        public void Configure(EntityTypeBuilder<Pericia> entity)
        {
            // Define a propriedade Id como chave primária da tabela Pericia
            entity.HasKey(p => p.Id);

            // Configura a propriedade Nome como obrigatória e com tamanho máximo de 100 caracteres
            entity.Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            // Converte a propriedade enum AtributoBase para string no banco, tornando obrigatória
            entity.Property(p => p.AtributoBase)
                  .HasConversion<string>()
                  .IsRequired();

            // Converte a propriedade enum Tipo para string no banco, tornando obrigatória
            entity.Property(p => p.Tipo)
                  .HasConversion<string>()
                  .IsRequired();

            // Configura a propriedade Descricao com tamanho máximo de 2000 caracteres (pode ser nula)
            entity.Property(p => p.Descricao)
                  .HasMaxLength(2000);

            // Ignora as propriedades calculadas ou não persistidas no banco
            entity.Ignore(p => p.DificuldadeSugerida);
            entity.Ignore(p => p.ValorTotal);

            // Serializa a lista de Tags para JSON na coluna TEXT e desserializa ao ler
            entity.Property(p => p.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null))
                .HasColumnType("TEXT");

            // Configura relacionamento muitos-para-muitos entre Pericia e Classe,
            // com tabela intermediária Classe_Pericia e chaves compostas
            entity.HasMany(p => p.ClassesRelacionadas)
                  .WithMany(c => c.PericiasRelacionadas)
                  .UsingEntity<Dictionary<string, object>>(
                      "Classe_Pericia",
                      j => j.HasOne<Classe>()
                            .WithMany()
                            .HasForeignKey("ClasseId")
                            .HasConstraintName("FK_Classe_Pericia_Classe"),
                      j => j.HasOne<Pericia>()
                            .WithMany()
                            .HasForeignKey("PericiaId")
                            .HasConstraintName("FK_Classe_Pericia_Pericia"),
                      j =>
                      {
                          j.HasKey("ClasseId", "PericiaId");
                          j.ToTable("Classe_Pericia");
                      });

            // Configura relacionamento 1:N para a coleção de dificuldades associadas à perícia,
            // deletando em cascata ao remover a perícia
            entity.HasMany(p => p.Dificuldades)
                  .WithOne()
                  .HasForeignKey("PericiaId")
                  .OnDelete(DeleteBehavior.Cascade);

            // Propriedades obrigatórias do tipo bool e inteiros
            entity.Property(p => p.EhProficiente).IsRequired();
            entity.Property(p => p.TemEspecializacao).IsRequired();
            entity.Property(p => p.BonusBase).IsRequired();
            entity.Property(p => p.BonusAdicional).IsRequired();
        }
    }
}
