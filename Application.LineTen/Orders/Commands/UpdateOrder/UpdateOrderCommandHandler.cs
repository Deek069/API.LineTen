using Application.LineTen.Common.Interfaces;
using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _ordersRepository.GetById(new OrderID(request.OrderID));
            if (order == null) return false;

            order.Status = request.Status;
            _ordersRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
