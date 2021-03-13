﻿using System.Collections.Generic;
using System.Web.Mvc;
using UpliftStore.DataAccess.Data.Repository.Interfaces;
using UpliftStore.Models;

namespace UpliftStore.DataAccess.Data.Repository.IRepository
{
    public interface IFrequencyRepository : IRepository<Frequency>
    {
        IEnumerable<SelectListItem> GetFrequencyListForDropDown();

        void Update(Frequency frequency);
    }
}
