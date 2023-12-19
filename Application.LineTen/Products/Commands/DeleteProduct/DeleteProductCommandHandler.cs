using Application.LineTen.Common.Interfaces;
using Application.LineTen.Products.Interfaces;
using Domain.LineTen.Products;
using MediatR;

namespace Application.LineTen.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler: IRequestHandler<DeleteProductCommand, bool>
    {
        private IProductsRepository _productsRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IProductsRepository customersRepository, IUnitOfWork unitOfWork)
        {
            _productsRepository = customersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var order = _productsRepository.GetById(new ProductID(request.ID));
            if (order == null) return false;

            _productsRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
