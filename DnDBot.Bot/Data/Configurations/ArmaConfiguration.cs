using DnDBot.Bot.Models.Ficha.Auxiliares;
using DnDBot.Bot.Models.ItensInventario;
using DnDBot.Bot.Models.ItensInventario.Auxiliares;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ArmaConfiguration : IEntityTypeConfiguration<Arma>
{
    public void Configure(EntityTypeBuilder<Arma> builder)
    {

        builder.ToTable("Arma");

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

        // Campos específicos para armas à distância
        builder.Property<int?>("AlcanceMinimo");
        builder.Property<int>("AlcanceMaximo");
        builder.Property<string>("TipoMunicao").HasMaxLength(50);
        builder.Property<int>("MunicaoPorAtaque");
        builder.Property<bool>("RequerRecarga");
        builder.Property<int>("TempoRecargaTurnos");

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

public class ArmaTagConfiguration : IEntityTypeConfiguration<ArmaTag>
{
    public void Configure(EntityTypeBuilder<ArmaTag> builder)
    {
        builder.HasKey(at => new { at.ArmaId, at.Tag });

        builder.HasOne(at => at.Arma)
               .WithMany(a => a.ArmaTags)
               .HasForeignKey(at => at.ArmaId);
    }
}
public class ArmaRequisitoAtributoConfiguration : IEntityTypeConfiguration<ArmaRequisitoAtributo>
{
    /// <summary>
    /// Configura a entidade ArmaRequisitoAtributo.
    /// </summary>
    /// <param name="entity">Construtor para configuração da entidade ArmaRequisitoAtributo.</param>
    public void Configure(EntityTypeBuilder<ArmaRequisitoAtributo> entity)
    {
        // Define a propriedade Id como chave primária da tabela ArmaRequisitoAtributo
        entity.HasKey(e => e.Id);

        // Configura a propriedade Atributo como obrigatória
        entity.Property(e => e.Atributo).IsRequired();

        // Configura a propriedade Valor como obrigatória
        entity.Property(e => e.Valor).IsRequired();

        // Configura o relacionamento muitos-para-um entre ArmaRequisitoAtributo e Arma
        // Uma arma pode ter vários requisitos de atributo
        entity.HasOne(e => e.Arma)
              .WithMany(a => a.RequisitosAtributos)
              .HasForeignKey(e => e.ArmaId);
    }
}