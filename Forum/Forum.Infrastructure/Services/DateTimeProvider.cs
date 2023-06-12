using Forum.Application.Common.Interfaces.Services;

namespace Forum.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
