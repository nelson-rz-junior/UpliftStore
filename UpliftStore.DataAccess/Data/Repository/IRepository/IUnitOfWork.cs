using System;
using UpliftStore.DataAccess.Data.Repository.IRepository;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        IFrequencyRepository FrequencyRepository { get; }

        IServiceRepository ServiceRepository { get; }

        void Save();
    }
}
