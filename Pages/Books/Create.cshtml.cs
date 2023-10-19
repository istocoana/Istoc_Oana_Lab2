using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Istoc_Oana_Lab2.Data;
using Istoc_Oana_Lab2.Models;

namespace Istoc_Oana_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Istoc_Oana_Lab2.Data.Istoc_Oana_Lab2Context _context;

        public CreateModel(Istoc_Oana_Lab2.Data.Istoc_Oana_Lab2Context context)
        {
            _context = context;
        }

        public List<Author> Authors { get; set; }

        public void OnGet()
        {
            Authors = _context.Author.ToList();

        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Book == null)
            {
                return Page();
            }

            if (Book.AuthorID != null)
            {
                var author = _context.Author.Find(Book.AuthorID);

                if (author != null)
                {
                    Book.Author = author;
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
