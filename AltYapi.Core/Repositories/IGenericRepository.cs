using System.Linq.Expressions;


namespace AltYapi.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //IQueryable de memory de birleştirilir tolist tolistasync gelince veritabanına gider
        IQueryable<T> GetAll();
        //ProductRepository.Where(x=>x.id>5).OrderBy.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        //Çoklu Kaydetme
        Task AddRangeAsync(IEnumerable<T> entities);
        //Update ve Removenin EfCore tarafında Async si yok
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
