using ItechCleanArst.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Bussines.Authors.Commands
{
    public record DeleteAuthorCommand(Guid Id) : IRequest<string>;

    public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, string>
    {
        private readonly IApplicationDbContext _dbcontext;

        public DeleteAuthorCommandHandler(IApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<string> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbcontext.Authors
                .Where(x => x.Id == request.Id && x.IsDeleted != true)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception($"author with ID {request.Id} not found");

            author.IsDeleted = true;
            _dbcontext.Authors.Update(author);
            await _dbcontext.SaveChangesAsync(cancellationToken);

            return $"{author.Id} has been deleted!";
        }
    }
}