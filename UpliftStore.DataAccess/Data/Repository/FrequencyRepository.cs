using System.Collections.Generic;
using UpliftStore.DataAccess.Data.Repository.IRepository;
using UpliftStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UpliftStore.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private readonly ApplicationDbContext _context;

        public FrequencyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {
            return _context.Frequencies.Select(f => new SelectListItem
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
        }

        public void Update(Frequency frequency)
        {
            var currentFrequency = _context.Frequencies.FirstOrDefault(f => f.Id == frequency.Id);
            if (currentFrequency != null)
            {
                currentFrequency.Name = frequency.Name;
                currentFrequency.Count = frequency.Count;

                _context.SaveChanges();
            }
        }
    }
}
