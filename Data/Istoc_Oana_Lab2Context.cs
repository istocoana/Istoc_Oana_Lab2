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
        private const DbSet<Book> Books = default;

        public Istoc_Oana_Lab2Context (DbContextOptions<Istoc_Oana_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; } = Books!;

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<Category>? Category { get; set; }
        public DbSet<BookCategory>? BookCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Borrowing)
                .WithOne(bw => bw.Book)
                .HasForeignKey<Borrowing>(bw => bw.BookID);
        }

        public DbSet<Istoc_Oana_Lab2.Models.Member>? Member { get; set; }

        public DbSet<Istoc_Oana_Lab2.Models.Borrowing>? Borrowing { get; set; }


    }


}
