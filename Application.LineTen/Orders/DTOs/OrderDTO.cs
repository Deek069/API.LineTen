using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Orders;
using System.Numerics;

namespace Application.LineTen.Orders.DTOs
{
    public class OrderDTO
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public Guid CustomerID { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public static OrderDTO FromOrder(Order order)
        {
            var result = new OrderDTO()
            {
                ID = order.ID.value,
                ProductID = order.ProductID.value,
                CustomerID = order.CustomerID.value,
                Status = order.Status,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            OrderDTO other = (OrderDTO)obj;
            return ID == other.ID &&
                   ProductID == other.ProductID &&
                   CustomerID == other.CustomerID &&
                   Status == other.Status &&
                   CreatedDate == other.CreatedDate &&
                   UpdatedDate == other.UpdatedDate;
        }
    }
}
