using System.Collections.Generic;

namespace UpliftStore.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<Service> Services { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
