using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EFBlaze;

public class EfBlazeIdentityDbContext<TUser> : IdentityDbContext<TUser> 
    where TUser : IdentityUser
{
    public EfBlazeIdentityDbContext(DbContextOptions<EfBlazeIdentityDbContext<TUser>> options) 
        : base(options) 
    {
    }
}
