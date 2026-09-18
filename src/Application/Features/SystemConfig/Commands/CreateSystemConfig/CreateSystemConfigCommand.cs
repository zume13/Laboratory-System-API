using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SystemConfig.Commands.CreateSystemConfig
{
    public record CreateSystemConfigCommand(string key, string value) : IRequest<ResultT<Guid>>;
}