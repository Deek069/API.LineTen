namespace Domain.LineTen.Orders
{
    public sealed record OrderID(Guid value)
    {
        public static OrderID CreateUnique()
        {
            return new OrderID(Guid.NewGuid());
        }
    };
}
