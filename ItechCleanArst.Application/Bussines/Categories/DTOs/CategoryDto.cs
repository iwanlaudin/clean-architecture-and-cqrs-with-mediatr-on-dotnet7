using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechCleanArst.Application.Bussines.Categories.DTOs
{
    public record CategoryDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public DateTime? CreatedDt { get; init; }
        public DateTime? UpdatedDt { get; init; }
    }
}