using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface IServiceRepository : IRepository<Service>
    {
        void Update(Service service);
    }
}
