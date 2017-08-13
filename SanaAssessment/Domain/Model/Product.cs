using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }
        // Assuming "product number" is actually a number, that's not the case most times        
        public Int32 ProductNumber { get; set; }
        public String Title { get; set; }
        public Decimal Price { get; set; }
    }
}
