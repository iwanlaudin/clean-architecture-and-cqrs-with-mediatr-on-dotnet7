using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Authors.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public CreateAuthorCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbcontext.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.IsDeleted != true);

            if (author != null)
            {
                return await Update(author, request, cancellationToken);
            }

            author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _dbcontext.Authors.AddAsync(author);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{author.Id} has been saved!";
        }

        public async Task<string> Update(Author author, CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            author.FirstName = request.FirstName;
            author.LastName = request.LastName;
            author.Email = request.Email;
            author.Address = request.Address;
            author.UpdatedAt = DateTime.UtcNow;

            _dbcontext.Authors.Update(author);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{author.Id} has been updated";
        }
    }
}