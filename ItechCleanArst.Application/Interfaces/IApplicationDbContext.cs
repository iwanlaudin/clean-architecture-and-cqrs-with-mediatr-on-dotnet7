using ItechCleanArst.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItechCleanArst.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}