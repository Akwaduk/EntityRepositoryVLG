using Microsoft.AspNetCore.Identity;
namespace BlazingScaffolds.Gates;
public interface IsGated
{
    GateAccessType HasAccess(IdentityUser user, BaseItem item);
}
