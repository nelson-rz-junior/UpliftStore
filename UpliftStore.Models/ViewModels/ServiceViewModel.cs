using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace UpliftStore.Models.ViewModels
{
    public class ServiceViewModel
    {
        public IFormFileCollection UploadImage { get; set; }

        public Service Service { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Frequencies { get; set; }
    }
}
