using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UpliftStore.Models;
using UpliftStore.Utility;

namespace UpliftStore.DataAccess.Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

                if (!await _roleManager.RoleExistsAsync(SD.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                    await _roleManager.CreateAsync(new IdentityRole(SD.Manager));
                }

                if (_context.ApplicationUsers.Count() == 0)
                {
                    await _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                        Name = "Master User"
                    },
                    "P@$$w0rd");

                    ApplicationUser user = _context.ApplicationUsers.Where(u => u.Email == "admin@gmail.com")
                        .FirstOrDefault();

                    await _userManager.AddToRoleAsync(user, SD.Admin);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
