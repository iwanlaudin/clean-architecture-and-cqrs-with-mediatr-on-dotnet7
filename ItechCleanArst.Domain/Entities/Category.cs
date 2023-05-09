using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechCleanArst.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Category()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}