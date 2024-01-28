using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pezeshkafzar.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int PriceAfterDiscount { get; set; }
        public int Stock { get; set; }

    }
}
