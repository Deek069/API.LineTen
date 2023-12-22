using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.DTOs;
using MediatR;
using Domain.LineTen.ValueObjects.Orders;
using Application.LineTen.Orders.Exceptions;

namespace Application.LineTen.Orders.Queries.GetOrderSummary
{
    public sealed class GetOrderSummaryQueryHandler : IRequestHandler<GetOrderSummaryQuery, OrderSummaryDTO>
    {
        private IOrdersRepository _ordersRepository;

        public GetOrderSummaryQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<OrderSummaryDTO> Handle(GetOrderSummaryQuery request, CancellationToken cancellationToken)
        {
            var orderID = new OrderID(request.ID);
            var order = _ordersRepository.GetById(orderID);
            if (order == null) throw new OrderNotFoundException(orderID);
            var result = OrderSummaryDTO.FromOrder(order);
            return result;
        }
    }
}
