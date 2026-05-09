using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class JoinRepository<TMain, TLink, TTarget>
    where TMain : Entity
    where TLink : Entity
    where TTarget : Entity
{
    private readonly DbContext _context;

    public JoinRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<List<TMain>> GetCombinedDataAsync(
        Expression<Func<TMain, IEnumerable<TLink>>> linkProperty,
        Expression<Func<TLink, TTarget>> targetProperty)
    {
        return await _context.Set<TMain>()
            .AsNoTracking()
            .Include(linkProperty)
                .ThenInclude(targetProperty)
            .ToListAsync();
    }
}