using Domain.LineTen.Products;

namespace Application.LineTen.Products.Interfaces
{
    public interface IProductsRepository
    {
        void Create(Product product);
        Product GetById(ProductID productID);
        IEnumerable<Product> GetAll();
        void Update(Product product);
        void Delete(Product product);
        bool ProductExists(ProductID productID);
    }
}
