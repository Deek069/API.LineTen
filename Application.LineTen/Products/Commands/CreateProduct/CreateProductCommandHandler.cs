using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductsRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                ID = ProductID.CreateUnique(),
                Name = request.Name,
                Description = request.Description,
                SKU = request.SKU
            };
            _productsRepository.Create(product);
            await _unitOfWork.SaveChangesAsync();
            return ProductDTO.FromProduct(product);
        }
    }
}
