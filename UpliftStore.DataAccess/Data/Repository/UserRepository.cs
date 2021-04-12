using System;
using System.Linq;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void LockUser(string userId)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                _context.SaveChanges();
            }
        }

        public void UnlockUser(string userId)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
