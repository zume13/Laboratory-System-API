using Application.Abstractions.Auth;
using Application.Dto;
using Domain.ValueObjects;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.Users.LogIn
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
            var emailResult = Email.Create(request.email);
            if (emailResult.IsFailure)
                return emailResult.Error;

            var result = await _authService.LocalLogInAsync(emailResult.value, request.password);

            if (result.IsFailure)
                return result.Error;

            return ResultT<TokenDto>.Success(result.value);
        }
    }
}
