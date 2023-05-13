using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechCleanArst.Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}