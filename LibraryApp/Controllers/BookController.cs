using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            return await _context.Books
                .Include(b => b.BookAuthors) 
                .ThenInclude(ba => ba.Author) 
                .Select(b => new BookDto
                {
                    id = b.id,
                    title = b.title,
                    isbn = b.isbn,
                    authors = b.BookAuthors.Select(ba => ba.Author.name).ToList()
                })
                .ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors) 
                .ThenInclude(ba => ba.Author) 
                .Select(b => new BookDto
                {
                    id = b.id,
                    title = b.title,
                    isbn = b.isbn,
                    authors = b.BookAuthors.Select(ba => ba.Author.name).ToList()
                })
                .FirstOrDefaultAsync(b => b.id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(long id, BookDto updatedBookDto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.id == id);

            if (book == null)
            {
                return NotFound();
            }

            // Update the book properties
            book.title = updatedBookDto.title;
            book.isbn = updatedBookDto.isbn;

           
            var authorNames = updatedBookDto.authors ?? new List<string>();

           
            var authors = new List<Author>();
            foreach (var authorName in authorNames)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.name == authorName);
                if (author == null)
                {
                    author = new Author { name = authorName };
                    _context.Authors.Add(author);
                }
                authors.Add(author);
            }

            
            _context.BookAuthors.RemoveRange(book.BookAuthors);

            
            var newBookAuthors = authors.Select(a => new BookAuthor { Book = book, Author = a }).ToList();
            _context.BookAuthors.AddRange(newBookAuthors);

            
            await _context.SaveChangesAsync();

           
            var removedAuthors = await _context.Authors
                                               .Where(a => a.BookAuthors.Count == 0)
                                               .ToListAsync();
            _context.Authors.RemoveRange(removedAuthors);

            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> PostBook(BookDto bookDto)
        {
            var book = new Book
            {
                title = bookDto.title,
                isbn = bookDto.isbn,
                BookAuthors = new List<BookAuthor>()
            };

            
            var authorNames = bookDto.authors ?? new List<string>();

            var authors = new List<Author>();
            foreach (var authorName in authorNames)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.name == authorName);
                if (author == null)
                {
                    author = new Author { name = authorName };
                    _context.Authors.Add(author);
                }
                authors.Add(author);
            }

            
            foreach (var author in authors)
            {
                var bookAuthor = new BookAuthor { Book = book, Author = author };
                book.BookAuthors.Add(bookAuthor);
            }

            _context.Books.Add(book);

            
            await _context.SaveChangesAsync();

            var createdBookDto = new BookDto
            {
                id = book.id,
                title = book.title,
                isbn = book.isbn,
                authors = book.BookAuthors.Select(ba => ba.Author.name).ToList()
            };

            return CreatedAtAction(nameof(GetBook), new { id = createdBookDto.id }, createdBookDto);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books
                                     .Include(b => b.BookAuthors)
                                     .FirstOrDefaultAsync(b => b.id == id);

            if (book == null)
            {
                return NotFound();
            }

            var associatedLoans = await _context.Loans
                                               .Where(l => l.bookid == id)
                                               .ToListAsync();
            foreach (var loan in associatedLoans)
            {
                _context.Loans.Remove(loan);
            }

            foreach (var bookAuthor in book.BookAuthors.ToList())
            {
                _context.BookAuthors.Remove(bookAuthor);
            }

            _context.Books.Remove(book);

            var removedAuthors = await _context.Authors
                                               .Where(a => a.BookAuthors.Count == 0)
                                               .ToListAsync();
            foreach (var author in removedAuthors)
            {
                _context.Authors.Remove(author);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
