using DnDBot.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DnDBot.Application.Data.Configurations
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureEntidadeBase<T>(this EntityTypeBuilder<T> builder) where T : EntidadeBase
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Descricao).HasMaxLength(2000);
            builder.Property(e => e.Fonte).HasMaxLength(200);
            builder.Property(e => e.Versao).HasMaxLength(50);
            builder.Property(e => e.Pagina).HasMaxLength(20);
            builder.Property(e => e.ImagemUrl).HasMaxLength(500);
            builder.Property(e => e.IconeUrl).HasMaxLength(500);
            builder.Property(e => e.CriadoPor).HasMaxLength(100);
            builder.Property(e => e.ModificadoPor).HasMaxLength(100);
            builder.Property(e => e.CriadoEm);
            builder.Property(e => e.ModificadoEm);

        }
    }

}
