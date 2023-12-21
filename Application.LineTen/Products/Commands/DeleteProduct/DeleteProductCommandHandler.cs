using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Exceptions;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler: IRequestHandler<DeleteProductCommand>
    {
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductsRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productID = new ProductID(request.ID);
            var product = _productsRepository.GetById(productID);
            if (product == null) throw new ProductNotFoundException(productID);

            _productsRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
