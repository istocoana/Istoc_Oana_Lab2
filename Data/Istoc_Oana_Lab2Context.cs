using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Istoc_Oana_Lab2.Models;

namespace Istoc_Oana_Lab2.Data
{
    public class Istoc_Oana_Lab2Context : DbContext
    {
        public Istoc_Oana_Lab2Context (DbContextOptions<Istoc_Oana_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Istoc_Oana_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<Istoc_Oana_Lab2.Models.Author>? Author { get; set; }
    }
}
