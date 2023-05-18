using ItechCleanArst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ItechCleanArst.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<BookAuthor> BookAuthors { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DatabaseFacade Database { get; }
    }
}