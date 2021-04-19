using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void ChangeOrderStatus(int orderHeaderId, string status);
    }
}
