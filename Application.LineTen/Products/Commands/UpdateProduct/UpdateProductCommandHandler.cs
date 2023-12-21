using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Exceptions;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand>
    {
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductsRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productID = new ProductID(request.ID);
            var product = _productsRepository.GetById(productID);
            if (product == null) throw new ProductNotFoundException(productID);

            product.Name = request.Name;
            product.Description = request.Description;
            product.SKU = request.SKU;
            _productsRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
