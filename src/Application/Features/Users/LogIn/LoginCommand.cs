using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.LogIn
{
    public record LoginCommand(
        string email,
        string password)
        : IRequest<ResultT<TokenDto>>;
}
