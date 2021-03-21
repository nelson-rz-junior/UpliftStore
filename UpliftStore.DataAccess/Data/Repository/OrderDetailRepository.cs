using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
