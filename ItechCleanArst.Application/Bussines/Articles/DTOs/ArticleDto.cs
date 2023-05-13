using ItechCleanArst.Application.Bussines.Categories.DTOs;

namespace ItechCleanArst.Application.Bussines.Articles.DTOs
{
    public record ArticleDto
    {
        public Guid Id { get; init; }
        public string? Title { get; init; }
        public string? Content { get; init; }
        public int TotalUserRate { get; init; }
        public DateTime PublishedDate { get; init; }
        public DateTime CreatedDt { get; init; }
        public DateTime UpdatedDt { get; init; }

        public CategoryDto? Category { get; init; }
    }
}