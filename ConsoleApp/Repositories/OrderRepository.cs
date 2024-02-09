using ConsoleApp.Context;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Repositories;

public class OrderRepository(DataContext context) : Repository<OrderEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Orders.Include(i => i.Details).Include(i => i.Customer).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }


    public override async Task<IEnumerable<OrderEntity>> GetAsync()
    {
        try
        {
            var existingEntities = await _context.Orders.Include(i => i.Details).Include(i => i.Customer).ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }
}


