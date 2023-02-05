using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities;
public class Permission
{
    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static Permission Create(int id, string name)
    {
        return new Permission(id, name);
    }
}
