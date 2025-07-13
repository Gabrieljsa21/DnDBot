using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DnDBot.Bot.Data.Configurations
{
    public class ArmaduraConfiguration : IEntityTypeConfiguration<Armadura>
    {
        public void Configure(EntityTypeBuilder<Armadura> builder)
        {
            builder.ToTable("Armadura");
        }
    }

    public class ArmaduraTagConfiguration : IEntityTypeConfiguration<ArmaduraTag>
    {
        public void Configure(EntityTypeBuilder<ArmaduraTag> builder)
        {
            builder.HasKey(at => new { at.ArmaduraId, at.Tag });

            builder.HasOne(at => at.Armadura)
                   .WithMany(a => a.ArmaduraTags)
                   .HasForeignKey(at => at.ArmaduraId);
        }
    }

    public class ArmaduraResistenciaConfiguration : IEntityTypeConfiguration<ArmaduraResistencia>
    {
        public void Configure(EntityTypeBuilder<ArmaduraResistencia> builder)
        {
            builder.HasKey(ar => new { ar.ArmaduraId, ar.ResistenciaId });

            builder.HasOne(ar => ar.Armadura)
                   .WithMany(a => a.Resistencias)
                   .HasForeignKey(ar => ar.ArmaduraId);

            builder.HasOne(ar => ar.Resistencia)
                   .WithMany()
                   .HasForeignKey(ar => ar.ResistenciaId);
        }
    }

    public class ArmaduraImunidadeConfiguration : IEntityTypeConfiguration<ArmaduraImunidade>
    {
        public void Configure(EntityTypeBuilder<ArmaduraImunidade> builder)
        {
            builder.HasKey(ai => new { ai.ArmaduraId, ai.ImunidadeId });

            builder.HasOne(ai => ai.Armadura)
                   .WithMany(a => a.Imunidades)
                   .HasForeignKey(ai => ai.ArmaduraId);

            builder.HasOne(ai => ai.Imunidade)
                   .WithMany()
                   .HasForeignKey(ai => ai.ImunidadeId);
        }
    }

    public class ArmaduraPropriedadeEspecialConfiguration : IEntityTypeConfiguration<ArmaduraPropriedadeEspecial>
    {
        public void Configure(EntityTypeBuilder<ArmaduraPropriedadeEspecial> builder)
        {
            // Define a chave composta
            builder.HasKey(ape => new { ape.ArmaduraId, ape.PropriedadeEspecialId });

            // Configura relacionamento com Armadura
            builder.HasOne(ape => ape.Armadura)
                   .WithMany(a => a.PropriedadesEspeciais)
                   .HasForeignKey(ape => ape.ArmaduraId);

            // Configura relacionamento com PropriedadeEspecial
            builder.HasOne(ape => ape.PropriedadeEspecial)
                   .WithMany()
                   .HasForeignKey(ape => ape.PropriedadeEspecialId);
        }
    }
}
