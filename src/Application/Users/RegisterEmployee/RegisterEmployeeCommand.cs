using Domain.Aggregates.Identity.UserProfile.Enums;
using MediatR;
using SharedKernel.Shared;

namespace Application.Users.RegisterEmployee
{
    public record RegisterEmployeeCommand(
     string firstName,
     string lastName,
     string email,
     string phoneNumber,
     string password, 
     StaffRole staffRole) 
     : IRequest<Result>;
}
