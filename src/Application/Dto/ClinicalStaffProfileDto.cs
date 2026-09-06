namespace Application.Dto
{
    public record ClinicalStaffProfileDto(Guid id, Guid userId, string role, bool isActive);
}