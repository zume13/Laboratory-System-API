using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Commands.UpdateSystemConfigValue
{
    public record UpdateSystemConfigValueCommand(string key, string value) : IRequest<Result>;
}