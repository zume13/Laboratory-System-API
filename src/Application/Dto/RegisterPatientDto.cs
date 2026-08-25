using Domain.Aggregates.Identity.PatientProfile.Enums;

namespace Application.Dto
{
    public record RegisterPatientDto(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        DateOnly dateOfBirth,
        Sex sex,
        string password,
        bool consent);
}
