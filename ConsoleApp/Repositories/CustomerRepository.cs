using ConsoleApp.Context;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Repositories;

public class CustomerRepository(DataContext context) : Repository<CustomerEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.Customers.Include(i => i.CustomerProfile).Include(i => i.Orders).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }

    public override async Task<IEnumerable<CustomerEntity>> GetAsync()
    {
        try
        {
            var existingEntities = await _context.Customers.Include(i => i.CustomerProfile).Include(i => i.Orders).ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }



}
