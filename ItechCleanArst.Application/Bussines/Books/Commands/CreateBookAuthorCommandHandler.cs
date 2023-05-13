using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Books.Commands
{
    public sealed class CreateBookAuthorCommandHandler : IRequestHandler<CreateBookAuthorCommand, Guid>
    {
        private readonly IApplicationDbContext _dbcontext;

        public CreateBookAuthorCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Guid> Handle(CreateBookAuthorCommand request, CancellationToken cancellationToken)
        {
            var bookAuthor = await _dbcontext.BookAuthors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BookId == request.BookId);

            if (bookAuthor != null)
            {
                return await Update(bookAuthor, request, cancellationToken);
            }

            foreach (var item in request.AuthorId)
            {
                _dbcontext.BookAuthors.Add(new BookAuthor
                {
                    Id = Guid.NewGuid(),
                    BookId = request.BookId,
                    AuthorId = item
                });
            }

            await _dbcontext.SaveChangesAsync(cancellationToken);
            return request.BookId;
        }

        public async Task<Guid> Update(BookAuthor bookAuthor, CreateBookAuthorCommand request, CancellationToken cancellationToken)
        {

            foreach (var item in request.AuthorId)
            {
                bookAuthor.AuthorId = item;
            }

            _dbcontext.BookAuthors.UpdateRange(bookAuthor);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return request.BookId;
        }
    }
}