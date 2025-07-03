using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DnDBot.Application.Data
{
    /// <summary>
    /// Fábrica para criação do contexto DnDBotDbContext em tempo de design.
    /// Usada principalmente para operações de migração e comandos CLI do Entity Framework Core.
    /// </summary>
    public class DnDBotDbContextFactory : IDesignTimeDbContextFactory<DnDBotDbContext>
    {
        /// <summary>
        /// Cria uma nova instância do DnDBotDbContext com configurações específicas,
        /// necessária para ferramentas EF Core em tempo de design, como 'dotnet ef migrations'.
        /// </summary>
        /// <param name="args">Argumentos opcionais (não usados).</param>
        /// <returns>Uma instância configurada do DnDBotDbContext.</returns>
        public DnDBotDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DnDBotDbContext>();

            // Configura a string de conexão para banco SQLite.
            // Atenção: ajuste o caminho do arquivo para refletir o local correto do banco.
            optionsBuilder.UseSqlite("Data Source=D:\\source\\repos\\DnDBot\\dndbot.db");

            // Retorna o contexto criado com as opções configuradas.
            return new DnDBotDbContext(optionsBuilder.Options);
        }
    }
}
