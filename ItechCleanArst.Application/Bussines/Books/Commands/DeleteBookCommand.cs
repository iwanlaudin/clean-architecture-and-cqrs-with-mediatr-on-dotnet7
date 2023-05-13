using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest<string>;

    public sealed class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public DeleteBookCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbcontext.Books
                .Where(x => x.Id == request.Id && x.IsDeleted != true)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"book with ID {request.Id} not found");

            book.IsDeleted = true;

            _dbcontext.Books.Update(book);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"book with ID {request.Id} has been deleted";
        }
    }
}