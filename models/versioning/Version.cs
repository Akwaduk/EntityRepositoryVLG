using System;
using BlazingScaffolds.Models;
public class Version<T> : BaseItem where T : BaseItem
{
    public required T Entity { get; set; }
}
