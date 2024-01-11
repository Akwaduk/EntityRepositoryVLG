using System;
using BlazingScaffolds.Models;

public class LoggedItem : BaseItem
{
    public string ItemIdentifier { get; set; }
    public string ItemData { get; set; }
    public string LogMessage { get; set; }
}
