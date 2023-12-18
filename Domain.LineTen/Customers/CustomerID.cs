namespace Domain.LineTen.Customers
{
    public sealed record CustomerID(Guid value)
    {
        public static CustomerID CreateUnique()
        {
            return new CustomerID(Guid.NewGuid());
        }
    }
}
