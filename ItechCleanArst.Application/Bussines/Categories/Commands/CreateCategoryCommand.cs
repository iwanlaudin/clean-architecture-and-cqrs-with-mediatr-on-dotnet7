using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Categories.Commands
{
    public record CreateCategoryCommand : IRequest<string>
    {
        public Guid Id { get; set; }
        public string? Name { get; init; }
    }
}