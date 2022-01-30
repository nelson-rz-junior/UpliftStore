using System.Linq;
using UpliftStore.DataAccess.Data.Repository.IRepository;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class WebImageRepository : Repository<WebImage>, IWebImageRepository
    {
        private readonly ApplicationDbContext _context;

        public WebImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(WebImage webImage)
        {
            var currentWebImage = _context.WebImages.FirstOrDefault(w => w.Id == webImage.Id);
            if (currentWebImage != null)
            {
                currentWebImage.Name = webImage.Name;
                currentWebImage.Picture = webImage.Picture;

                _context.SaveChanges();
            }
        }
    }
}
