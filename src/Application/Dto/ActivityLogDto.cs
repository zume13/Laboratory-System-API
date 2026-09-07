namespace Application.Dto
{
    public record ActivityLogDto(Guid id, Guid? userId, string action, string target, string severity, DateTime timestamp);
}