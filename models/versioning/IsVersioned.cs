using BlazingScaffolds.Models;
using Microsoft.AspNetCore.Identity;
namespace BlazingScaffolds.Gates;
public interface IsVersioned<T> where T : BaseItem
{
    Version<T> HasAccess(IdentityUser user, BaseItem item);
}