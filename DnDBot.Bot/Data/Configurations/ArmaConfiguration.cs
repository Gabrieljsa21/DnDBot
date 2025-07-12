using DnDBot.Bot.Models.ItensInventario;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArmaConfiguration : IEntityTypeConfiguration<Arma>
{
    public void Configure(EntityTypeBuilder<Arma> builder)
    {
        builder.Property(a => a.Nome)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(a => a.DadoDano)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(a => a.DadoDanoVersatil)
               .HasMaxLength(10);

        builder.Property(a => a.TipoDano)
               .IsRequired();

        builder.Property(a => a.TipoDanoSecundario);

        builder.Property(a => a.EhDuasMaos)
               .IsRequired();

        builder.Property(a => a.EhLeve)
               .IsRequired();

        builder.Property(a => a.EhVersatil)
               .IsRequired();

        builder.Property(a => a.EhAgil)
               .IsRequired();

        builder.Property(a => a.EhPesada)
               .IsRequired();

        builder.Property(a => a.DurabilidadeAtual)
               .IsRequired();

        builder.Property(a => a.DurabilidadeMaxima)
               .IsRequired();

        builder.HasDiscriminator<string>("Discriminator")
                .HasValue<ArmaCorpoACorpo>("CorpoACorpo")
                .HasValue<ArmaADistancia>("Distancia");

        // Campos específicos para armas à distância
        builder.Property<int?>("AlcanceMinimo");
        builder.Property<int?>("AlcanceMaximo");
        builder.Property<string>("TipoMunicao").HasMaxLength(50);
        builder.Property<int?>("MunicaoPorAtaque");
        builder.Property<bool?>("RequerRecarga");
        builder.Property<int?>("TempoRecargaTurnos");

        // Relacionamentos (exemplo para tags, requisitos, etc)
        builder.Ignore(a => a.Tags); // se Tags for uma propriedade calculada

        builder.HasMany(a => a.RequisitosAtributos)
               .WithOne()
               .HasForeignKey("ArmaId");

        builder.HasMany(a => a.ArmaTags)
               .WithOne()
               .HasForeignKey("ArmaId");

        // Configure índices, chaves, ou outras constraints aqui, se necessário
    }
}

public class ArmaCorpoACorpoConfiguration : IEntityTypeConfiguration<ArmaCorpoACorpo>
{
    public void Configure(EntityTypeBuilder<ArmaCorpoACorpo> builder)
    {
        builder.Property(a => a.AlcanceEmMetros).IsRequired();
        // outras propriedades específicas
    }
}

public class ArmaADistanciaConfiguration : IEntityTypeConfiguration<ArmaADistancia>
{
    public void Configure(EntityTypeBuilder<ArmaADistancia> builder)
    {
        builder.Property(a => a.AlcanceMinimo);
        builder.Property(a => a.AlcanceMaximo);
        builder.Property(a => a.TipoMunicao).HasMaxLength(50);
        builder.Property(a => a.MunicaoPorAtaque);
        builder.Property(a => a.RequerRecarga);
        builder.Property(a => a.TempoRecargaTurnos);
    }
}
