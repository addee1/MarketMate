using ConsoleApp.Context;
using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConsoleApp.Repositories;

public class CustomerProfileRepository(DataContext context) : Repository<CustomerProfileEntity>(context)
{
    private readonly DataContext _context = context;

    public override async Task<CustomerProfileEntity> GetAsync(Expression<Func<CustomerProfileEntity, bool>> expression)
    {
        try
        {
            var existingEntity = await _context.CustomerProfiles.Include(i => i.Customer).FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                return existingEntity;
            }
        }
        catch { }
        return null!;
    }


    public override async Task<IEnumerable<CustomerProfileEntity>> GetAsync()
    {
        try
        {
            var existingEntities = await _context.CustomerProfiles.Include(i => i.Customer).ToListAsync();
            if (existingEntities != null)
            {
                return existingEntities;
            }
        }
        catch { }
        return null!;
    }
}
