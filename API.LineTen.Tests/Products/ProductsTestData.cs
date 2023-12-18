using Domain.LineTen.Products;

namespace API.LineTen.Tests.Products
{
    internal sealed class ProductsTestData
    {
        public Product Product1 { get; set; }
        public Product Product2 { get; set; }

        public ProductsTestData()
        {
            Product1 = new Product()
            {
                ID = ProductID.CreateUnique(),
                Name = "Kawasaki Z800",
                Description = "The Kawasaki Z800 is a Z series four-cylinder standard motorcycle made by Kawasaki from 2013 through 2016, replaced by the Z900 for 2017.",
                SKU = "KHI-201310"
            };

            Product2 = new Product()
            {
                ID = ProductID.CreateUnique(),
                Name = "Yamaha R1",
                Description = "The Yamaha YZF-R1, or simply R1, is a 998 cc sports motorcycle made by Yamaha. It was first released in 1998, undergoing significant updates in 2000, 2002, 2004, 2006, 2007, 2009, 2015, 2018 and 2020.",
                SKU = "YZF-200405"
            };
        }
    }
}
