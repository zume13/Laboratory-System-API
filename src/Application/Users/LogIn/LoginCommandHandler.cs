using Application.Abstractions.Auth;
using Application.Dto;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Users.LogIn
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultT<TokenDto>>
    {
        private readonly IAuthService _authService;
        public LoginCommandHandler(IAuthService service)
        {
            _authService = service;
        }
        public async Task<ResultT<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.LocalLogInAsync(Email.Create(request.email).value, request.password);

            if (result.IsFailure)
                return result.Error;

            return ResultT<TokenDto>.Success(result.value);
        }
    }
}
