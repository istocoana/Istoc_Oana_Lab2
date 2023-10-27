using Microsoft.AspNetCore.Mvc.RazorPages;
using Istoc_Oana_Lab2.Data;
using Istoc_Oana_Lab2.Models;

namespace Istoc_Oana_Lab2.Models

{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Istoc_Oana_Lab2Context context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = book?.BookCategories?.Select(c => c.CategoryID) ?? new List<int>();
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.CategoryID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.CategoryID)
                });
            }
        }

        public void UpdateBookCategories(Istoc_Oana_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>
            (bookToUpdate.BookCategories.Select(c => c.Category.CategoryID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.CategoryID.ToString()))
                {
                    if (!bookCategories.Contains(cat.CategoryID))
                    {
                        bookToUpdate.BookCategories.Add(
                        new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.CategoryID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.CategoryID))
                    {
                        BookCategory courseToRemove
                        = bookToUpdate
                        .BookCategories
                        .SingleOrDefault(i => i.CategoryID == cat.CategoryID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}