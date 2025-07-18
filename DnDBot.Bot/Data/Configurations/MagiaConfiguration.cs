﻿using DnDBot.Bot.Models;
using DnDBot.Bot.Models.Ficha;
using DnDBot.Bot.Models.Ficha.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDBot.Bot.Data.Configurations
{
    public class MagiaConfiguration : IEntityTypeConfiguration<Magia>
    {
        public void Configure(EntityTypeBuilder<Magia> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(m => m.MagiaTags)
                .WithOne(mt => mt.Magia)
                .HasForeignKey(mt => mt.MagiaId);

            builder.HasMany(m => m.EfeitosEscalonados)
                   .WithOne(e => e.Magia)
                   .HasForeignKey(e => e.MagiaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class MagiaClassePermitidaConfiguration : IEntityTypeConfiguration<MagiaClassePermitida>
    {
        public void Configure(EntityTypeBuilder<MagiaClassePermitida> builder)
        {
            builder.HasKey(x => new { x.MagiaId, x.Classe });

            builder.HasOne(x => x.Magia)
                   .WithMany(m => m.ClassesPermitidas)
                   .HasForeignKey(x => x.MagiaId);
        }
    }

    public class MagiaTagConfiguration : IEntityTypeConfiguration<MagiaTag>
    {
        public void Configure(EntityTypeBuilder<MagiaTag> builder)
        {
            builder.HasKey(x => new { x.MagiaId, x.Tag });

            builder.HasOne(x => x.Magia)
                   .WithMany(m => m.MagiaTags)
                   .HasForeignKey(x => x.MagiaId);
        }
    }

    public class MagiaCondicaoAplicadaConfiguration : IEntityTypeConfiguration<MagiaCondicaoAplicada>
    {
        public void Configure(EntityTypeBuilder<MagiaCondicaoAplicada> builder)
        {
            builder.HasKey(x => new { x.MagiaId, x.Condicao });

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
        }
    }

    public class MagiaCondicaoRemovidaConfiguration : IEntityTypeConfiguration<MagiaCondicaoRemovida>
    {
        public void Configure(EntityTypeBuilder<MagiaCondicaoRemovida> builder)
        {
            builder.HasKey(x => new { x.MagiaId, x.Condicao });

            builder.HasOne(x => x.Magia)
                   .WithMany()
                   .HasForeignKey(x => x.MagiaId);
        }
    }
}
