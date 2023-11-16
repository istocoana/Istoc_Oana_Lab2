using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Istoc_Oana_Lab2.Data;
using Istoc_Oana_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Istoc_Oana_Lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]

    public class EditModel : BookCategoriesPageModel
    {
        private readonly Istoc_Oana_Lab2Context _context;

        public EditModel(Istoc_Oana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }
        public List<Author> Authors { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Category> SelectedCategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }

            Authors = await _context.Author.ToListAsync();
            Publishers = await _context.Publisher.ToListAsync();

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var category in _context.Category)
            {
                var assignedCategory = new AssignedCategoryData
                {
                    CategoryID = category.CategoryID,
                    Name = category.CategoryName
                };

                assignedCategory.Assigned = Book.BookCategories.Any(bc => bc.CategoryID == category.CategoryID);

                AssignedCategoryDataList.Add(assignedCategory);
            }
            PopulateAssignedCategoryData(_context, Book);



            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
                .Include(i => i.Publisher)
                .Include(i => i.BookCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                i => i.Title, i => i.AuthorID, i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            Authors = await _context.Author.ToListAsync();
            Publishers = await _context.Publisher.ToListAsync();
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
}
