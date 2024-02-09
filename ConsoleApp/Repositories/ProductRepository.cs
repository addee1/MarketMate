using ConsoleApp.Context;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Repositories;

public class ProductRepository(DataContext context) : Repository<ProductEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Products.Include(i => i.Details).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }


    public override async Task<IEnumerable<ProductEntity>> GetAsync()
    {
        try
        {
            var existingEntities = await _context.Products.Include(i => i.Details).ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }
}
