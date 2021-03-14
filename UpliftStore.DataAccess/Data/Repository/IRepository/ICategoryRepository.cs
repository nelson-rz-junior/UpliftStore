using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }
}
