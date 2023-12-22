using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Exceptions;
using Application.LineTen.Products.Interfaces;
using MediatR;

namespace Application.LineTen.Products.Queries.GetProductByID
{
    public sealed class GetProductByIDQueryHandler: IRequestHandler<GetProductByIDQuery, ProductDTO>
    {
        private IProductsRepository _productsRepository;

        public GetProductByIDQueryHandler(IProductsRepository customersRepository)
        {
            _productsRepository = customersRepository;
        }

        public async Task<ProductDTO> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
        {
            var product = _productsRepository.GetById(request.ID);
            if (product == null) throw new ProductNotFoundException(request.ID);
            return ProductDTO.FromProduct(product);
        }
    }
}
