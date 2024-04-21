using Domain.Contracts;

namespace Application.Interfaces.Repositories;

public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    IQueryable<T> Entities { get; }

    Task<T> GetByIdAsync(TId id);

    Task<List<T>> GetAllAsync(int skipCount = 0, int getCount = 0, string sortingString = "");

    Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}
