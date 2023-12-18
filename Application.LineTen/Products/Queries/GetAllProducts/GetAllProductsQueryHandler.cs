using Application.LineTen.Customers.Queries.GetAllCustomers;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Interfaces;
using MediatR;

namespace Application.LineTen.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private IProductsRepository _productsRepository;

        public GetAllProductsQueryHandler(IProductsRepository customersRepository)
        {
            _productsRepository = customersRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var allProducts = _productsRepository.GetAll();
            List<ProductDTO> result = allProducts.Select(ProductDTO.FromProduct).ToList();
            return result;
        }
    }
}
