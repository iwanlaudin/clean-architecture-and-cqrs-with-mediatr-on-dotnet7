using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Books.Commands.BookAuthors
{
    public class CreateBookAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid[] AuthorId { get; set; }
    }
}