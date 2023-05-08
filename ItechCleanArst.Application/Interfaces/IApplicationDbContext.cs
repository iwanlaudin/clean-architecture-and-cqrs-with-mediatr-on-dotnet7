using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItechCleanArst.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ItechCleanArst.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }

        DatabaseFacade Database { get; }
    }
}