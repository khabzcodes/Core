namespace Core.Domain.Enums;

public class Enums
{
    public Permissions Permissions { get; set; }
}

public enum Permissions
{
    CanViewMemberGroup = 1,
    CanViewMember = 2,
    CanUpdateMember = 3
}
