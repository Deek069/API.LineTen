using Microsoft.EntityFrameworkCore;

namespace Persistence.LineTen.Tests
{
    internal sealed class TestDBContext 
    {
        public static LineTenDB GetTestDBContext()
        {
            var options = new DbContextOptionsBuilder<LineTenDB>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            return new LineTenDB(options);
        }
    }
}
