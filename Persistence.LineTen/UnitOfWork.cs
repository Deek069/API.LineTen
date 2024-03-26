using Application.LineTen.Common.Interfaces;
using Domain.LineTen.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.LineTen
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LineTenDB _dbContext;

        public UnitOfWork(LineTenDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            UpdateAuditableEntities();
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            var entries = _dbContext.ChangeTracker.Entries<IAuditableEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(a => a.CreatedDate).CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(a => a.UpdatedDate).CurrentValue = DateTime.Now;
                }
            }
        }
    }
}
