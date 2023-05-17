using ItechCleanArst.Application.Bussines.Authors.DTOs;

namespace ItechCleanArst.Application.Bussines.Books.DTOs
{
    public record BookDto
    {
        public Guid Id { get; init; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? Publisher { get; init; }
        public DateTime? PublishedDate { get; init; }
        public ICollection<AuthorDto> Author { get; init; }
    }  
}