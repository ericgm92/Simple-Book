using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _bookContext;

        public BooksController(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = _bookContext.Books.ToList();
            return Ok(books);
        }

        [HttpPost("create")]
        public ActionResult<int> CreateBook([FromBody] Book book)
        {
            // Generate an ID for new book
            int newBookId = GenerateUniqueId();
            book.Id = newBookId;
            book.RegistrationTimestamp = DateTime.Now;
            _bookContext.Books.Add(book);
            _bookContext.SaveChanges();
            return Ok(newBookId);
        }

        [HttpPut("{bookId}/update")]
        public ActionResult<Book> UpdateBook(int bookId, [FromBody] Book updatedBook)
        {
            var book = _bookContext.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }
            book.Name = updatedBook.Name ?? book.Name;
            book.Author = updatedBook.Author ?? book.Author;
            book.Category = updatedBook.Category ?? book.Category;
            book.Description = updatedBook.Description ?? book.Description;
            _bookContext.SaveChanges();
            return Ok(book);
        }



        private int GenerateUniqueId()
        {
            // Implement logic to generate a unique ID for a book
            // This is just a placeholder method
            return new Random().Next(1000, 9999);
        }
    }
}
