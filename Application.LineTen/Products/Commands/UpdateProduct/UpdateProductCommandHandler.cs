using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, bool>
    {
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductsRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _productsRepository.GetById(new ProductID(request.ProductID));
            if (product == null) return false;

            product.Name = request.Name;
            product.Description = request.Description;
            product.SKU = request.SKU;
            _productsRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
