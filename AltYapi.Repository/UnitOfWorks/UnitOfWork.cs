using AltYapi.Core.UnitOfWorks;

namespace AltYapi.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDisposable Session => throw new NotImplementedException();

        public void AddOperation(Action operation)
        {
            throw new NotImplementedException();
        }

        public void CleanOperations()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task CommitChanges()
        {
            throw new NotImplementedException();
        }
    }
}
