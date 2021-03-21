using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
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
    }
}
