using System.Linq;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void ChangeOrderStatus(int orderHeaderId, string status)
        {
            var order = _context.OrderHeaders.FirstOrDefault(oh => oh.Id == orderHeaderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
            }
        }
    }
}
