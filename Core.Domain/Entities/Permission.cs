using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;
public class Permission
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
}
