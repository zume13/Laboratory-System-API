using Domain.Aggregates.Identity.UserProfile.Enums;

namespace Application.Dto
{
    public record RegisterEmployeeDto(string firstName, string lastName, string email, string phoneNumber, string password, StaffRole staffRole);
}
