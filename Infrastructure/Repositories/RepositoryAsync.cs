using Application.Interfaces.Repositories;
using Domain.Contracts;
using Infrastructure.Contexts;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId> where T : AuditableEntity<TId>
{
    protected readonly MainContext _dbContext;

    public RepositoryAsync(MainContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync(int skipCount = 0, int getCount = 0, string sortingString = "")
    {
        var sqlString = $"SELECT * FROM {typeof(T).Name}s";

        if (!string.IsNullOrEmpty(sortingString))
        {
            sqlString += $" ORDER BY {sortingString}";
        }

        if (skipCount > 0)
        {
            sqlString += $" OFFSET {skipCount}";
        }

        if (getCount > 0)
        {
            sqlString += $" LIMIT {getCount}";
        }

        var notSortingEntities = _dbContext
            .Set<T>().FromSqlRaw(sqlString);

        return await notSortingEntities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(TId id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        return await _dbContext
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateAsync(T entity)
    {
        T exist = _dbContext.Set<T>().Find(entity.Id);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        return Task.CompletedTask;
    }
}
