using System;
using BlazingScaffolds.Models;
public class Version<T> : BaseItem where T : BaseItem
{
    public T Entity { get; set; }
}
