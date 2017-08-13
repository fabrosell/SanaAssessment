using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Model;

namespace Domain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Product product) : base(ProductNotFoundMessage(product))
        {

        }

        private static String ProductNotFoundMessage(Product product)
        {
            return String.Format("Product {0} (ref# {1}, id {2}) does not exist!", product.Title, product.ProductNumber, product.ProductId);
        }
    }
}
