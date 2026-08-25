using Domain.Aggregates.Identity.PatientProfile.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Users.RegisterPatient
{
    public record RegisterPatientCommand(string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateOnly DateOfBirth,
            Sex sex,
            string password,
            bool consent) : IRequest<Result>;
}
