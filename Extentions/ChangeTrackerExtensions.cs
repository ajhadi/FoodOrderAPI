using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FoodOrderAPI.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            IEnumerable<EntityEntry> entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is IBaseEntity && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    IBaseEntity entity = (IBaseEntity)entry.Entity;
                    entity.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
