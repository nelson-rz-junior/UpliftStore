using System;
using UpliftStore.DataAccess.Data.Repository.IRepository;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        IFrequencyRepository FrequencyRepository { get; }

        IServiceRepository ServiceRepository { get; }

        IOrderHeaderRepository OrderHeaderRepository { get; }

        IOrderDetailRepository OrderDetailRepository { get; }

        IUserRepository UserRepository { get; }

        void Save();
    }
}
