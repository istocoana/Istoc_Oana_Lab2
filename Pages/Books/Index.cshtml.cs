using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Istoc_Oana_Lab2.Data;
using Istoc_Oana_Lab2.Models;

namespace Istoc_Oana_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Istoc_Oana_Lab2Context _context;

        public IndexModel(Istoc_Oana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get; set; }
        public string TitleSort { get; private set; }
        public string AuthorSort { get; private set; }
        public string CurrentFilter { get; set; }


        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            AuthorSort = sortOrder == "author" ? "author_desc" : "author";
            CurrentFilter = searchString;


            IQueryable<Book> booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category);

            if (!String.IsNullOrEmpty(searchString))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Author.FirstName.Contains(searchString) ||
                    b.Author.LastName.Contains(searchString) ||
                    b.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    booksQuery = booksQuery.OrderByDescending(b => b.Title);
                    break;
                case "author":
                    Book = await booksQuery.ToListAsync();
                    Book = Book.OrderBy(b => b.Author.FullName).ToList();
                    return;
                case "author_desc":
                    Book = await booksQuery.ToListAsync();
                    Book = Book.OrderByDescending(b => b.Author.FullName).ToList();
                    return;
                default:
                    booksQuery = booksQuery.OrderBy(b => b.Title);
                    break;
            }

            Book = await booksQuery.ToListAsync();
        }

    }
}
