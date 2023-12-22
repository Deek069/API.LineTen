using Application.LineTen.Common;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.DTOs;
using Application.LineTen.Products.Exceptions;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Entities;
using Domain.LineTen.Validation;
using Domain.LineTen.ValueObjects.Products;
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

            var validator = new ProductValidator();
            var result = validator.Validate(product);
            if (!result.IsValid)
            {
                throw new ProductValidationException(ValidationExceptionMessage.Message(result.Errors));
            }

            _productsRepository.Create(product);
            await _unitOfWork.SaveChangesAsync();
            return ProductDTO.FromProduct(product);
        }
    }
}
