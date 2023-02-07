namespace Core.Domain.Enums;

public class Enums
{
    public Permissions Permissions { get; set; }
}

public enum Permissions
{
    ReadUsers = 1,
    ReadUser = 2,
    UpdateUser = 3,
    DeleteUser = 4,
    AddUser = 5,
}
