namespace Application.Features.LabOrder.Dto
{
    public record PendingLabRequestDto(
        Guid requestId,
        Guid labOrderId,
        Guid patientId,
        string testCategoryName,
        DateTime createdAt);
}   