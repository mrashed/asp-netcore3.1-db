using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    // Creating an API that can be used to get data from the Book db
    [Route("api/Book")]     // Need to route to the API controller
    [ApiController]
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;


        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }


        // API for getting the list of current books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // data is a list of book that is then turned into JSON format.
            return Json(new { data = await _db.Book.ToListAsync() });
        }


        // API for removing a book
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            // Grap the book object from the db context
            // get u where u.Id equal the id passed in.
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            _db.Book.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}