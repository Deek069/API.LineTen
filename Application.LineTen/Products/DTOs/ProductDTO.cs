using Domain.LineTen.Entities;

namespace Application.LineTen.Products.DTOs
{
    public class ProductDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }

        public static ProductDTO FromProduct(Product product)
        {
            var result = new ProductDTO()
            {
                ID = product.ID.value,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ProductDTO other = (ProductDTO)obj;
            return ID == other.ID &&
                   Name == other.Name &&
                   Description == other.Description &&
                   SKU == other.SKU;
        }
    }
}
