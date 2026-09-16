using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.Commands.LogIn
{
    public record LoginCommand(
        string email,
        string password)
        : IRequest<ResultT<TokenDto>>;
}
