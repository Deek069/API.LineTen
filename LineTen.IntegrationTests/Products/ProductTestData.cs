using Application.LineTen.Products.Commands.CreateProduct;
using Domain.LineTen.Products;

namespace LineTen.IntegrationTests.Products
{
    internal sealed class ProductTestData
    {
        public CreateProductCommand CreateProductCommand1 { get; set; }
        public CreateProductCommand CreateProductCommand2 { get; set; }

        public ProductTestData()
        {
            CreateProductCommand1 = new CreateProductCommand(
                "Kawasaki Z800",
                "The Kawasaki Z800 is a Z series four-cylinder standard motorcycle made by Kawasaki from 2013 through 2016, replaced by the Z900 for 2017.",
                "KHI-201310"
            );

            CreateProductCommand2 = new CreateProductCommand(
                "Yamaha R1",
                "The Yamaha YZF-R1, or simply R1, is a 998 cc sports motorcycle made by Yamaha. It was first released in 1998, undergoing significant updates in 2000, 2002, 2004, 2006, 2007, 2009, 2015, 2018 and 2020.",
                "YZF-200405"
            );
        }
    }
}
