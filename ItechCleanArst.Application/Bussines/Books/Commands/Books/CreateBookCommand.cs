using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace ItechCleanArst.Application.Bussines.Books.Commands.Books
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime PublisherDate { get; set; }
        public Guid[] AuthorId { get; set; }
    }
}