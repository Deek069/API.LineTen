using Application.LineTen.Orders.DTOs;
using Application.LineTen.Orders.Interfaces;
using MediatR;

namespace Application.LineTen.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderSummaryDTO>>
    {
        private IOrdersRepository _ordersRepository;

        public GetAllOrdersQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<IEnumerable<OrderSummaryDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var allOrders = _ordersRepository.GetAll();
            List<OrderSummaryDTO> result = allOrders.Select(OrderSummaryDTO.FromOrder).ToList();
            return result;
        }
    }
}
