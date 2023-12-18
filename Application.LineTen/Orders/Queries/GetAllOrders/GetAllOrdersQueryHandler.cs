using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Interfaces;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDTO>>
    {
        private IOrdersRepository _ordersRepository;

        public GetAllOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var allOrders = _ordersRepository.GetAll();
            List<OrderDTO> result = allOrders.Select(OrderDTO.FromOrder).ToList();
            return result;
        }
    }
}
