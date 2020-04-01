using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class EditModel : PageModel
    {
        // Step 1: almost every controller needs to have at least 
        //         one database context 
        private ApplicationDbContext _db;


        // Step 2: Almost always created at least one object
        //         model to hold the return values from the
        //         database or user form.
        [BindProperty]
        public Book EditBook { get; set; }


        // Step 3: Almost always need to assign the database
        //         contexts to the local model object(s)
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }


        // Step 4: Often need a default page or a page with config 
        //         arguments sent in get request.
        //         Get will get the ID and load the data into the form
        //         URL: url/Edit?id={some ID Number}
        public async Task OnGet(int id)
        {
            // go to database and find the book with an id of id.
            EditBook = await _db.Book.FindAsync(id);
        }

        // Step 5: often need a post request to add user provided
        //         form data to the DB or program action.
        // Put will update the values in the DB.
        // When we redirect to a new page after handleing the form post
        // return we need to give Task a tyep of IActionResult
        public async Task <IActionResult> OnPost ()
        {
            // Make sure the Model came back valid
            if (ModelState.IsValid)
            {
                // Get the book from the DB.  The ID came back in the bind Editbook.Id property.
                // Must have the ID in the form even if hidden or it will not be set and this
                // line will give an error.
                // Error: NullReferenceException: Object reference not set to an instance of an object.
                var BookFromDb = await _db.Book.FindAsync(EditBook.Id);

                // Set our new values from the form into the record that we got from the DB.
                // From data --> Bound object --> DB record object
                BookFromDb.Name = EditBook.Name;
                BookFromDb.ISBN = EditBook.ISBN;
                BookFromDb.Author = EditBook.Author;

                await _db.SaveChangesAsync();   // Saves the changes to the DB

                return RedirectToPage("Index");
            }

            // If model state is bad then redirect back to the same edit page
            return RedirectToPage();

        }
    }
}