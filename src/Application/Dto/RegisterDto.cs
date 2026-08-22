using Domain.Aggregates.Identity.PatientProfile.Enums;

namespace Application.Dto
{
    public record RegisterDto(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        DateTime DateOfBirth,
        Sex Sex,
        string Password,
        bool Consent);
}
