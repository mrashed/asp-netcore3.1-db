using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor
{
    public class CreateModel : PageModel
    {
        // This page will open up a blank form and when submitted will call the OnPost() method to 
        // add the data to the DB
        //
        // 1. First step is to get the db context.  
        //    In pipeline so we can use dependency injection
        private readonly ApplicationDbContext _db;

        // 2. Create a model to hold the return from the user
        //    Use a property binding book so the OnPost does need a book object passed to it.
        //    The binding fills the Book object from the put request data.
        //    Use BindProperty instead of public async Task<IActionResult> OnPost(Book bookObj)
        [BindProperty]
        public Book Book { get; set; }

        // 3. Create a constructor (ctro tab tab)
        //    We get the ApplicationDbContext through depencency injection.
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }


        public void OnGet()
        {

        }


        // Save the submittion to the DB
        // IActionResult because we are going to another page.
        public async Task<IActionResult> OnPost()
        {
            // Will be pasing a book object
            // Check the book property was passed in correctly
            if (ModelState.IsValid)
            {
                // context takes the book object and adds it to a queue of things to be done
                await _db.Book.AddAsync(Book);

                // Now context / EF translate queue to SQL speak (insert) in this case.
                await _db.SaveChangesAsync();

                // Go back to the index page - redirect to a new page
                return RedirectToPage("Index");
            }
            else {
                // If the object is not correct from the input return to the page
                // IsValid input must match all the data model rules
                return Page();
            }

        }
    }
}