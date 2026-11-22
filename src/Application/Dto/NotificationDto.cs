namespace Application.Dto
{
    public record NotificationDto(
        Guid id,
        string channel,
        string status,
        string message,
        Guid? labResultId,
        DateTime? sentAt,
        DateTime createdAt);
}