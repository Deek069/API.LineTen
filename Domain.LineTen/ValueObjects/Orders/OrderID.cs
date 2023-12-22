namespace Domain.LineTen.ValueObjects.Orders
{
    public sealed record OrderID(Guid value)
    {
        public static OrderID CreateUnique()
        {
            return new OrderID(Guid.NewGuid());
        }
    };
}
