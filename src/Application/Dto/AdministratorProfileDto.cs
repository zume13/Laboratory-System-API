namespace Application.Dto
{
    public record AdministratorProfileDto(Guid id, Guid userId, string permissions);
}