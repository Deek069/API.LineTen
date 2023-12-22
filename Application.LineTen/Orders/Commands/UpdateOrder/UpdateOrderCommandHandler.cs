using Application.LineTen.Common;
using Application.LineTen.Common.Interfaces;
using Application.LineTen.Orders.Exceptions;
using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.Validation;
using Domain.LineTen.ValueObjects.Orders;
using MediatR;

namespace Application.LineTen.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private IOrdersRepository _ordersRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateOrderCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderID = new OrderID(request.ID);
            var order = _ordersRepository.GetById(orderID);
            if (order == null) throw new OrderNotFoundException(orderID);

            order.Status = request.Status;

            var validator = new OrderValidator();
            var result = validator.Validate(order);
            if (!result.IsValid)
            {
                throw new OrderValidationException(ValidationExceptionMessage.Message(result.Errors));
            }

            _ordersRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
