using System.ComponentModel.DataAnnotations.Schema;

namespace ItechCleanArst.Domain.Entities
{
    public class BookAuthor
    {
        public Guid Id { get; set; }
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }
        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Author Author { get; set; }
    }
}