using library_management_system.Models;
using library_management_system.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace library_management_system.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? author, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (books, totalItems) = await _bookService.SearchBooksAsync(title, author, page, pageSize);

            return Ok(new
            {
                Data = books,
                TotalItems = totalItems,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] Book book)
        {
            var existingRecord = await _bookService.GetBookByIdAsync(id);
            _bookService.UpdateBook(book, existingRecord);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteBook(Guid id)
        {
            _bookService.DeleteBook(id);
            return NoContent();
        }
    }
}
