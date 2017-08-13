using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Domain.Model;
using Domain.Repositories;
using Domain.Exceptions;

namespace Persistence.XML
{
    public class ProductRepository : IProductRepository
    {
        // Very basic XML handling: read all data into memory, then write back changes 
        // I am assuming this is required only for changing the storage during run-time
        // I would implement a more efficient way of handling data if an XML storage is needed.
        private String fileName = "data.xml";
        private static Object fileLock = new object();

        public ProductRepository()
        {
            if (!File.Exists(fileName))
                PersistData(new List<Product>());
        }

        public IEnumerable<Product> List()
        {
            return LoadData();
        }

        public Product Get(Guid ProductId)
        {
            var products = LoadData();
            return products.Where(x => x.ProductId == ProductId).SingleOrDefault();
        }

        public Guid Insert(Product Product)
        {
            var products = LoadData() as List<Product>;
            Product.ProductId = Guid.NewGuid();
            products.Add(Product);
            PersistData(products);

            return Product.ProductId;
        }

        public Boolean Update(Product Product)
        {
            var products = LoadData() as List<Product>;
            var existingProduct = products.Where(x => x.ProductId == Product.ProductId).SingleOrDefault();

            if (existingProduct != null)
            {
                products.Remove(existingProduct);
                products.Add(Product);
                PersistData(products);
                return true;
            }
            else
                return false;
        }

        public Product Delete(Guid ProductId)
        {
            var products = LoadData() as List<Product>;
            var product = products.Where(x => x.ProductId == ProductId).SingleOrDefault();

            if (product != null)
            {
                products.Remove(product);
                PersistData(products);
            }
            else
                throw new ProductNotFoundException(product);

            return product;
        }

        private void PersistData(IEnumerable<Product> products)
        {
            StreamWriter writer = null;

            lock (fileLock)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<Product>));
                    writer = new StreamWriter(fileName);
                    serializer.Serialize(writer, products);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }
            }
        }

        private IEnumerable<Product> LoadData()
        {
            List<Product> products = null;
            StreamReader reader = null;

            lock (fileLock)
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(List<Product>));
                    reader = new StreamReader(fileName);
                    products = serializer.Deserialize(reader) as List<Product>;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }
            }

            return products;
        }
    }
}
