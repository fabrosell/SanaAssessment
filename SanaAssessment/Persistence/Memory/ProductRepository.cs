using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Model;
using Domain.Repositories;
using Domain.Exceptions;


namespace Persistence.Memory
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products = new List<Product>();

        public IEnumerable<Product> List()
        {
            return _products;
        }

        public Product Get(Guid ProductId)
        {
            return _products.Where(x => x.ProductId == ProductId).SingleOrDefault();
        }

        public Guid Insert(Product Product)
        {
            Product.ProductId = Guid.NewGuid();
            _products.Add(Product);
            return Product.ProductId;
        }

        public Boolean Update(Product Product)
        {            
            try
            {
                // Over-simplify the method. Delete product then insert new one. 
                Delete(Product.ProductId);
                _products.Add(Product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product Delete(Guid ProductId)
        {
            var product = _products.Where(x => x.ProductId == ProductId).SingleOrDefault();

            if (product != null)
                _products.Remove(product);
            else
                throw new ProductNotFoundException(product);

            return product;
        }        
    }
}
