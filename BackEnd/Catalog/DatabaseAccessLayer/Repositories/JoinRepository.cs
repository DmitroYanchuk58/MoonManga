using DatabaseAccessLayer.Entities;
using DatabaseAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class JoinRepository<TMain, TLink, TTarget> : IJoin<TMain, TLink, TTarget>
    where TMain : Entity
    where TLink : Entity
    where TTarget : Entity
{
    private readonly DbContext _context;

    public JoinRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<(TMain?, List<TTarget>?)> GetManyToManyByIdAsync(
        Guid idEntity,
        Expression<Func<TMain, IEnumerable<TLink>>> linkProperty,
        Expression<Func<TLink, TTarget>> targetProperty)
    {
        var entity = await _context.Set<TMain>()
            .AsNoTracking()
            .Where(x => x.Id == idEntity)
            .Include(linkProperty)
                .ThenInclude<TMain, TLink, TTarget>(targetProperty)
            .FirstOrDefaultAsync();

        if (entity == null) return (null, null);
        var getLinks = linkProperty.Compile();
        var links = getLinks(entity);
        var getTarget = targetProperty.Compile();
        var targets = links.Select(link => getTarget(link)).ToList();

        return (entity, targets);
    }

    public async Task<(TMain?, List<TChild>?)> GetOneToManyByIdAsync<TChild>(
        Guid idEntity,
        Expression<Func<TMain, IEnumerable<TChild>>> childProperty)
        where TChild : Entity
    {
        var entity = await _context.Set<TMain>()
            .AsNoTracking()
            .Where(x => x.Id == idEntity)
            .Include(childProperty) 
            .FirstOrDefaultAsync();

        if (entity == null) return (null, null);

        var getChildren = childProperty.Compile();
        var children = getChildren(entity)?.ToList() ?? new List<TChild>();

        return (entity, children);
    }
}