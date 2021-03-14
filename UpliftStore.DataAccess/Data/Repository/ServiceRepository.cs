using System.Linq;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Service service)
        {
            var currentService = _context.Services.FirstOrDefault(c => c.Id == service.Id);
            if (currentService != null)
            {
                currentService.Name = service.Name;
                currentService.Price = service.Price;
                currentService.ImageFile = service.ImageFile;
                currentService.Description = service.Description;
                currentService.CategoryId = service.CategoryId;
                currentService.FrequencyId = service.FrequencyId;

                _context.SaveChanges();
            }
        }
    }
}
