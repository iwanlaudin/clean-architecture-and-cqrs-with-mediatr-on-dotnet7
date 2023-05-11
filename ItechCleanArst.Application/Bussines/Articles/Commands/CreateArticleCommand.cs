using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Articles.Commands
{
    public record CreateArticleCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? Title { get; init; }
        public string? Content { get; init; }
        public Guid CategoryId  { get; init; }
    }
}