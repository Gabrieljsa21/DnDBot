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
        public void Configure(EntityTypeBuilder<Pericia> entity)
        {

            entity.ConfigureEntidadeBase();

            // Chave primária
            entity.HasKey(p => p.Id);

            // Propriedades básicas
            entity.Property(p => p.Nome)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.Descricao)
                  .HasMaxLength(2000);

            entity.Property(p => p.AtributoBase)
                  .HasConversion<string>()
                  .IsRequired();

            entity.Property(p => p.Tipo)
                  .HasConversion<string>()
                  .IsRequired();

            // Serialização de listas simples

            entity.Property(p => p.AtributosAlternativos)
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                      v => JsonSerializer.Deserialize<List<Atributo>>(v, (JsonSerializerOptions)null))
                  .HasColumnType("TEXT");

            // Ignora propriedades que não devem ser persistidas
            entity.Ignore(p => p.DificuldadeSugerida);
            entity.Ignore(p => p.ValorTotal);

            // Muitos-para-muitos: Pericia <-> Classe
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

            // Um-para-muitos: Pericia -> Dificuldades
            entity.HasMany(p => p.Dificuldades)
                  .WithOne()
                  .HasForeignKey("PericiaId")
                  .OnDelete(DeleteBehavior.Cascade);

            // Bools e ints obrigatórios
            entity.Property(p => p.EhProficiente).IsRequired();
            entity.Property(p => p.TemEspecializacao).IsRequired();
            entity.Property(p => p.BonusBase).IsRequired();
            entity.Property(p => p.BonusAdicional).IsRequired();
        }
    }
}
