using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;

namespace Persistence.LineTen.Repositories
{
    public sealed class ProductsRepository : IProductsRepository
    {
        private LineTenDB db;

        public ProductsRepository(LineTenDB dbContext)
        {
            db = dbContext;
        }

        public void Create(Product product)
        {
            db.Products.Add(product);
        }

        public void Delete(Product product)
        {
            db.Products.Remove(product);
        }

        public IEnumerable<Product> GetAll()
        {
            var result = db.Products;
            return result;
        }

        public Product GetById(ProductID productID)
        {
            var result = db.Products.Find(productID);
            return result;  
        }

        public bool ProductExists(ProductID productID)
        {
            var result = db.Products.Any(m => m.ID == productID);
            return result;
        }

        public void Update(Product product)
        {
            db.Products.Update(product);
        }
    }
}
