using Application.LineTen.Common.Interfaces;
using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersRepository.GetById(new OrderID(request.ID));
            if (order == null) return false;

            _ordersRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
