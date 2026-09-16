namespace Application.Dto
{
    public record SystemConfigDto(Guid id, string key, string value, DateTime updatedAt);
}