using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.DataAccess.Data.Repository.IRepository;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository CategoryRepository { get; private set; }

        public IFrequencyRepository FrequencyRepository { get; private set; }

        public IServiceRepository ServiceRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            CategoryRepository = new CategoryRepository(_context);
            FrequencyRepository = new FrequencyRepository(_context);
            ServiceRepository = new ServiceRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
