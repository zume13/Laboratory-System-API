namespace Application.Dto
{
    public record TestCategoryPopularityDto(Guid testCategoryId, string testCategoryName, int requestCount);
}