using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFBlaze;

public class BlazingScaffoldDbContext<TUser> : IdentityDbContext<TUser>
    where TUser : IdentityUser
{
    private readonly UserManager<TUser> _userManager;
    private readonly HttpContextAccessor _httpContextAccessor;
    public BlazingScaffoldDbContext(DbContextOptions options, UserManager<TUser> userManager,
     HttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    private List<LoggedItem> OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<LoggedItem>();

        // Get the current user ID
        var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User)
                     ?? "Anonymous";

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                var tableName = entry?.Metadata?.GetTableName();
                if (tableName == null) tableName = "unknown";
                var auditEntry = new LoggedItem
                {
                    ItemIdentifier = Guid.NewGuid(), // Add the ItemIdentifier property
                    TableName = tableName,                  
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