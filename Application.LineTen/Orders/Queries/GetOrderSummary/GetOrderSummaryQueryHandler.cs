using Application.LineTen.Orders.Interfaces;
using Application.LineTen.Orders.DTOs;
using MediatR;
using Domain.LineTen.Orders;

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
            var order = _ordersRepository.GetById(new OrderID(request.OrderID));
            if (order == null) return null;
            var result = OrderSummaryDTO.FromOrder(order);
            return result;
        }
    }
}
