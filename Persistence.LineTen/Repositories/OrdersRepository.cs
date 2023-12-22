using Application.LineTen.Orders.Interfaces;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Orders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.LineTen.Repositories
{
    public sealed class OrdersRepository : IOrdersRepository
    {
        private LineTenDB db;

        public OrdersRepository(LineTenDB dbContext)
        {
            db = dbContext;
        }

        public void Create(Order order)
        {
            db.Orders.Add(order);
        }

        public void Delete(Order order)
        {
            db.Orders.Remove(order);
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Product);
            return orders;
        }

        public Order GetById(OrderID orderID)
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Product);
            var order = orders.Where(o => o.ID == orderID).FirstOrDefault();
            if (order == null) return null;
            return order;
        }

        public void Update(Order order)
        {
            db.Orders.Update(order);
        }
    }
}
