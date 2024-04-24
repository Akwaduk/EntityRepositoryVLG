using BlazingScaffolds.Models;

namespace BlazingScaffolds.Gates;
public interface IGateKeep<T> where T : BaseItem
{
    bool CanCreate(T item);
    bool CanRead(T item);
    bool CanUpdate(T item);
    bool CanDelete(T item);
}
