using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItechCleanArst.Domain.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedDt { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsDeleted { get; set; }
    }
}