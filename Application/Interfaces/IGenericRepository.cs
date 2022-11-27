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
}