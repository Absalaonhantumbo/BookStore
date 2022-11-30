using Application.Specification;
using Domain;

namespace Aplication.Interfaces;

public interface IGenericRepository<T> where T: BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> ListAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> Complete();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<List<T>> ListWithSpecAsync(ISpecification<T> spec);
}