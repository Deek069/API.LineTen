using Application.LineTen.Customers.DTOs;
using Domain.LineTen.Orders;
using System.Numerics;

namespace Application.LineTen.Orders.DTOs
{
    public class OrderSummaryDTO
    {
        public Guid ID { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public string StatusDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public static OrderSummaryDTO FromOrder(Order order)
        {
            var result = new OrderSummaryDTO()
            {
                ID = order.ID.value,
                ProductName = order.Product.Name,
                ProductDescription = order.Product.Description,
                CustomerFirstName = order.Customer.FirstName,
                CustomerLastName = order.Customer.LastName,
                StatusDescription = order.Status.ToString(),
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
            };
            return result;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            OrderSummaryDTO other = (OrderSummaryDTO)obj;
            return ID == other.ID &&
                   ProductName == other.ProductName &&
                   ProductDescription == other.ProductDescription &&
                   CustomerFirstName == other.CustomerFirstName &&
                   CustomerLastName == other.CustomerLastName &&
                   StatusDescription == other.StatusDescription &&
                   CreatedDate == other.CreatedDate &&
                   UpdatedDate == other.UpdatedDate;
        }
    }
}
