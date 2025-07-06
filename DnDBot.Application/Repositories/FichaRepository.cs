using DnDBot.Application.Data;
using DnDBot.Application.Models.Ficha;
using DnDBot.Application.Models.Inventario;
using DnDBot.Application.Models.ItensInventario;
using DnDBot.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class FichaRepository : IFichaRepository
{
    private readonly DnDBotDbContext _dbContext;

    public FichaRepository(DnDBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FichaPersonagem?> ObterFichaPorIdAsync(Guid id)
    {
        return await _dbContext.FichaPersonagem
            .Include(f => f.Inventario) // Se Inventario for uma entidade relacionada
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task InserirFichaAsync(FichaPersonagem ficha)
    {
        _dbContext.FichaPersonagem.Add(ficha);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Inventario?> ObterInventarioPorFichaIdAsync(Guid fichaId)
    {
        var ficha = await _dbContext.FichaPersonagem
            .Include(f => f.Inventario)
            .FirstOrDefaultAsync(f => f.Id == fichaId);

        return ficha?.Inventario;
    }

    public async Task AtualizarInventarioAsync(Guid fichaId, Inventario inventario)
    {
        // Aqui, por exemplo, atualize o inventário e salve:
        _dbContext.Inventarios.Update(inventario);
        await _dbContext.SaveChangesAsync();
    }
}
