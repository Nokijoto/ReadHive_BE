using System.ComponentModel.DataAnnotations.Schema;
using ProjectBase.Interfaces;

namespace ProjectBase.Models;
public abstract class BaseModel : IBase
{
    [Column("id")]
    public Guid Id { get; set; }
  
    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
    
}