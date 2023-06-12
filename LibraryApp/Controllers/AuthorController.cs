using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors;
        }

        // GET: api/Authors/5/Books
        [HttpGet("{id}/Books")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAuthorBooks(int id)
        {
            var author = await _context.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(a => a.id == id);

            if (author == null)
            {
                return NotFound();
            }

            var books = author.BookAuthors.Select(ba => new BookDto
            {
                id = ba.Book.id,
                title = ba.Book.title,
                isbn = ba.Book.isbn,
                authors = ba.Book.BookAuthors.Select(b => b.Author.name).ToList()
            }).ToList();

            return books;
        }
    }
}
