using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechCleanArst.Application.Bussines.Categories.DTOs;
using ItechCleanArst.Domain.Entities;

namespace ItechCleanArst.Application.Bussines.Articles.DTOs
{
    public record ArticleDto
    {
        public Guid Id { get; init; }
        public string? Title { get; init; }
        public string? Content { get; init; }
        public int TotalUserRate { get; init; }
        public DateTime CreatedDt { get; init; }
        public DateTime UpdatedDt { get; init; }

        public CategoryDto? Category { get; init; }
    }
}