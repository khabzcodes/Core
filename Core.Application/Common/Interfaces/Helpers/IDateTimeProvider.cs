namespace Core.Application.Common.Interfaces.Helpers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
