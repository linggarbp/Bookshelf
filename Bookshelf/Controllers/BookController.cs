using Bookshelf.Models;
using Bookshelf.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooksAsync() => Ok(await bookRepository.GetBookAsync());

        [HttpGet("Promo")]
        public async Task<ActionResult<List<Book>>> GetPromoBooksAsync() => Ok(await bookRepository.GetPromoBookAsync());

        [HttpPost]
        public async Task<ActionResult<(bool, string)>> ManageBookAsync(Book book)
        {
            if (book is null) return BadRequest(false);

            var result = await bookRepository.ManageBookAsync(book);

            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<(bool, string)>> DeleteBookAsync(int id)
        {
            var result = await bookRepository.DeleteBookAsync(id);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
