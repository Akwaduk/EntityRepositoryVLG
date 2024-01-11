using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFBlaze;

public class BlazingScaffoldDbContext<TUser> : IdentityDbContext<TUser>
    where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;

    public BlazingScaffoldDbContext(DbContextOptions options, UserManager<TUser> userManager)
        : base(options)
    {
        _userManager = userManager;
    }
    private List<LoggedItem> OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<LoggedItem>();

        // Get the current user ID
        var userId = _userManager.GetUserId(_httpContextAccessor?.HttpContext?.User)
                     ?? "Anonymous";

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                var auditEntry = new AuditEntry
                {
                    TableName = entry.Metadata.GetTableName(),
                    ChangeTime = DateTime.UtcNow,
                    UserId = userId,
                    OperationType = entry.State == EntityState.Added ? "Add" : "Update"
                };

                foreach (var property in entry.Properties)
                {
                    if (entry.State == EntityState.Added || property.IsModified)
                    {
                        auditEntry.PropertyName = property.Metadata.Name;
                        auditEntry.OriginalValue = entry.State == EntityState.Added ? null : property.OriginalValue?.ToString();
                        auditEntry.CurrentValue = property.CurrentValue?.ToString();
                        auditEntries.Add(auditEntry);
                    }
                }
            }
        }

        return auditEntries;
    }
}