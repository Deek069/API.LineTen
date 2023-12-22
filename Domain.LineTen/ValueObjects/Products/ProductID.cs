namespace Domain.LineTen.ValueObjects.Products
{
    public sealed record ProductID(Guid value)
    {
        public static ProductID CreateUnique()
        {
            return new ProductID(Guid.NewGuid());
        }
    };
}
