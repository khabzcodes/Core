using Core.Application.Common.Interfaces.Helpers;

namespace Core.Infrastructure.Helpers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
