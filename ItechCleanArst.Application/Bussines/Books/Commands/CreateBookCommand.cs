using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public record CreateBookCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public string? Publisher { get; init; }
        public bool IsPublished { get; set; }
        public Guid[] AuthorId { get; init; }
    }
}