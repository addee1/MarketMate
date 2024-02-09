using ConsoleApp.Context;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Repositories;

public class DetailRepository(DataContext context) : Repository<DetailEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<DetailEntity> GetAsync(Expression<Func<DetailEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Details.Include(i => i.Order).Include(i => i.Product).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }


    public override async Task<IEnumerable<DetailEntity>> GetAsync()
    {
        try
        {
            var existingEntities = await _context.Details.Include(i => i.Order).Include(i => i.Product).ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }
}
