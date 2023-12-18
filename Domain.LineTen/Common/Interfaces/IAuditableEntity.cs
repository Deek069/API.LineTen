namespace Domain.LineTen.Common.Interfaces
{
    public class IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
