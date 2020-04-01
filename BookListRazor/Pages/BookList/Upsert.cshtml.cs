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

    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        [BindProperty]
        public Book UpsertBook { get; set; }


        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }


        // Get will get the ID and load the data into the form
        // Now the this method might be used for edit or create and if
        // used for create there will be no id.  We need to use a nullable
        // integer and then check if null.
        public async Task<IActionResult> OnGet(int? id)
        {
            UpsertBook = new Book();

            // New book - Create
            if (id == null)
            {
                return Page();  // Return back to the page with an empty book object
            }

            // Update
            // Go to database and find the book with an id of id.
            UpsertBook = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);

            if (UpsertBook == null)
            {
                return NotFound();
            }
            // Return to the page with the book filled in.
            return Page();
        }

        // Put will update the values in the DB.
        // When we redirect to a new page after handleing the form post
        // return we need to give Task a tyep of IActionResult
        public async Task<IActionResult> OnPost()
        {
            // Make sure the Model came back valid
            if (ModelState.IsValid)
            {
                // New book - add to DB
                if (UpsertBook.Id == 0) {
                    _db.Book.Add(UpsertBook);
                }
                else
                {
                    // Updates every property.   Can break out and update only some properties
                    // by retrieving the current copy and only changing some fields.
                    _db.Book.Update(UpsertBook);
                }

                // Saves the changes to the DB
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            // If model state is bad then redirect back to the same edit page
            return RedirectToPage();

        }
    }
}