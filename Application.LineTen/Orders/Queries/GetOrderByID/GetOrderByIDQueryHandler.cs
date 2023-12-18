using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.Orders;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetOrderByID
{
    public sealed class GetOrderByIDQueryHandler : IRequestHandler<GetOrderByIDQuery, OrderDTO>
    {
        private IOrdersRepository _ordersRepository;

        public GetOrderByIDQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<OrderDTO> Handle(GetOrderByIDQuery request, CancellationToken cancellationToken)
        {
            var order = _ordersRepository.GetById(request.OrderID);
            if (order == null) return null;
            return OrderDTO.FromOrder(order);
        }
    }
}
