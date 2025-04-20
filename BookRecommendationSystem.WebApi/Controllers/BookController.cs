using BookRecommendationSystem.Application.Books;
using BookRecommendationSystem.Application.Moduls.Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookRecommendationSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> AddBook(BookDto bookDto)
        {
            await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new {id = bookDto.Id}, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.Id) return BadRequest();

            await _bookService.UpdateBookAsync(bookDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
