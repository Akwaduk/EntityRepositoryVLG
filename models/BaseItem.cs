using System.ComponentModel.DataAnnotations.Schema;
using BlazingScaffolds.Gates;
using EntityRepositoryVLG.Models.Logging;

namespace  BlazingScaffolds.Models;
public abstract class BaseItem
{
    public Guid Id { get; set; }
    [NotMapped]
    public bool IsGated => this is IsGated ? true : false;

    [NotMapped]
    public bool IsLogged => this is IsLogged<BaseItem> ? true : false;

    [NotMapped]
    public bool IsVersioned => this is IsVersioned<BaseItem> ? true : false;

    
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}
