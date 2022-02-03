namespace Courserio.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        IQueryable<T> ListAllAsQueryable();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);

        //Task<T> PatchAsync(int id, JsonPatchDocument<T> patchDocument);

        //Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        //IQueryable<T> ListAsQueryable(ISpecification<T> spec);
        //Task<int> CountAsync(ISpecification<T> spec);
        //Task<T> FirstAsync(ISpecification<T> spec);
        //Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
    }
}
