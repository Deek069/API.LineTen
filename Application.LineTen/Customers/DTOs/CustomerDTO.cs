using Domain.LineTen.Entities;

namespace Application.LineTen.Customers.DTOs
{
    public class CustomerDTO
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public static CustomerDTO FromCustomer(Customer customer)
        {
            var result = new CustomerDTO()
            {
                ID = customer.ID.value,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Email = customer.Email
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CustomerDTO other = (CustomerDTO)obj;
            return ID == other.ID && 
                   FirstName == other.FirstName && 
                   LastName == other.LastName &&
                   Phone == other.Phone && 
                   Email == other.Email;
        }
    }
}
