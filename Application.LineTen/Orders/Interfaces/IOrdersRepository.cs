using Domain.LineTen.Orders;

namespace Application.LineTen.Orders.Interfaces
{
    public interface IOrdersRepository
    {
        void Create(Order order);
        Order GetById(OrderID id);
        IEnumerable<Order> GetAll();
        void Update(Order order);
        void Delete(Order order);
    }
}
