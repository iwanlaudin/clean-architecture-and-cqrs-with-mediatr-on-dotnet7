using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechCleanArst.Application.Bussines.Authors.DTOs
{
    public record AuthorDto
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Address { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}