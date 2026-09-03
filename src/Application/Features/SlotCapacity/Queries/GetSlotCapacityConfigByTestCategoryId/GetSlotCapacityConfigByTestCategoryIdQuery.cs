using Application.Dto;
using MediatR;
using SharedKernel.Shared;

namespace Application.Features.SlotCapacity.Queries.GetSlotCapacityConfigByTestCategoryId
{
    public record GetSlotCapacityConfigByTestCategoryIdQuery(Guid testCategoryId) : IRequest<ResultT<SlotCapacityConfigDto>>;
}