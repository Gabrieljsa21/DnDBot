using Discord;
using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DnDBot.Bot.Data.Configurations
{
    /// <summary>
    /// Configuração da entidade Classe para o Entity Framework Core.
    /// Define o mapeamento da classe Classe para a tabela no banco de dados.
    /// </summary>
    public class ClasseConfiguration : IEntityTypeConfiguration<Classe>
    {
        /// <summary>
        /// Configura a entidade Classe.
        /// </summary>
        /// <param name="entity">Construtor para configuração da entidade Classe.</param>
        public void Configure(EntityTypeBuilder<Classe> entity)
        {
            // Define a propriedade Id como chave primária da tabela Classe
            entity.HasKey(c => c.Id);

            // Configura a propriedade Nome como obrigatória e com tamanho máximo de 100 caracteres
            entity.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            entity.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(c => c.ClasseTags)
                .WithOne(ct => ct.Classe)
                .HasForeignKey(ct => ct.ClasseId);

            // Configuração da coleção RiquezaInicial como entidades próprias (Owned Entities)
            entity.OwnsMany(c => c.RiquezaInicial, moedas =>
            {
                // Define chave estrangeira para relacionar RiquezaInicial à Classe
                moedas.WithOwner().HasForeignKey("ClasseId");

                // Define uma chave primária para cada item da riqueza inicial
                moedas.Property<int>("Id");
                moedas.HasKey(m => m.Tipo);

                // Configura o campo Tipo, convertendo enum para string e tornando obrigatório
                moedas.Property(m => m.Tipo)
                      .HasConversion<string>()
                      .IsRequired();

                // Configura o campo Quantidade como obrigatório
                moedas.Property(m => m.Quantidade)
                      .IsRequired();

                // Define o nome da tabela que armazenará os registros de riqueza inicial ligados às classes
                moedas.ToTable("Classe_RiquezaInicial");
            });

            // Configura relacionamento muitos-para-muitos entre Classe e Pericia
            entity
                .HasMany(c => c.PericiasRelacionadas)
                .WithMany(p => p.ClassesRelacionadas)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassePericia", // Nome da tabela intermediária
                    j => j
                        .HasOne<Pericia>()               // Configura relacionamento com Pericia
                        .WithMany()
                        .HasForeignKey("PericiaId")
                        .HasConstraintName("FK_ClassePericia_Pericia")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Classe>()                // Configura relacionamento com Classe
                        .WithMany()
                        .HasForeignKey("ClasseId")
                        .HasConstraintName("FK_ClassePericia_Classe")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        // Define chave primária composta na tabela intermediária
                        j.HasKey("ClasseId", "PericiaId");

                        // Define nome da tabela intermediária
                        j.ToTable("ClassePericia");
                    });

        }
    }
}
