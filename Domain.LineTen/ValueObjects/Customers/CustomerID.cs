namespace Domain.LineTen.ValueObjects.Customers
{
    public sealed record CustomerID(Guid value)
    {
        public static CustomerID CreateUnique()
        {
            return new CustomerID(Guid.NewGuid());
        }
    }
}
