using Application.LineTen.Common.Interfaces;
using Application.LineTen.Orders.Exceptions;
using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.ValueObjects.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderID = new OrderID(request.ID);
            var order = _ordersRepository.GetById(orderID);
            if (order == null) throw new OrderNotFoundException(orderID);

            _ordersRepository.Delete(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
