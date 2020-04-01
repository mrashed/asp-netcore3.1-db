using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor
{
    public class IndexModel : PageModel
    {
        // 1. First step is to get the db context.  
        //    In pipeline so we can use dependency injection
        private readonly ApplicationDbContext _db;

        // 3. Create a list of books to hold all the books from the DB
        //    This property will be available to the razor page.
        public IEnumerable<Book> Books { get; set; }


        // 2. Create a constructor (ctro tab tab)
        //    We get the ApplicationDbContext through depencency injection.
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }


        // We have handlers in razor pages instead of action methods in ASP.NET.
        public async Task OnGet()
        {
            // 4. Get the books and fill the list (IEnumerable)
            //    Need: using Microsoft.EntityFrameworkCore;
            Books = await _db.Book.ToListAsync();   // thread off a task and wait for response.
        }


        // Handles the delete of a book.
        // This is post handler because we used a button to get here (like a form does)
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if (book == null)
            {
                // Returns to the same index page with NotFound included
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}