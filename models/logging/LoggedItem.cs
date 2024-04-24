using System;
using BlazingScaffolds.Models;

public class LoggedItem : BaseItem
{
    public required string TableName {get;set;}
    public required string ItemIdentifier { get; set; }
    public required string OperationType { get; set; }
    public required string ItemData { get; set; }
    public required string LogMessage { get; set; }
}
