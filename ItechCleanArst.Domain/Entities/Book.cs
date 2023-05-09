namespace ItechCleanArst.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public Book()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime PublisherDate { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
    }
}