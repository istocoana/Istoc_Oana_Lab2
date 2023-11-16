using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Istoc_Oana_Lab2.Data;
using Istoc_Oana_Lab2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Istoc_Oana_Lab2.Pages.Authors
{
    [Authorize(Roles = "Admin")]

    public class CreateModel : PageModel
    {
        private readonly Istoc_Oana_Lab2.Data.Istoc_Oana_Lab2Context _context;

        public CreateModel(Istoc_Oana_Lab2.Data.Istoc_Oana_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Author Author { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Author == null || Author == null)
            {
                var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
                ViewData["Errors"] = errors;
                return Page();
            }

            _context.Author.Add(Author);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
