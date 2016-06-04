using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookService.Data;
using Microsoft.EntityFrameworkCore;
using BookService.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private ApplicationDbContext _dbContext;

        public BooksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Books
        [HttpGet]
        public IQueryable<BookListDTO> GetAll()
        {
            var books = from b in _dbContext.Books
                        select new BookListDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            AuthorName = b.Author.Name
                        };

            return books;
        }

        // GET: api/Books/author/5
        [HttpGet("author/{authorId}")]
        public IQueryable GetByAuthorId(int authorId)
        {
            var books = from b in _dbContext.Books.Where(b => b.AuthorId == authorId)
                        select new BookListDTO()
                       {
                           Id = b.Id,
                           Title = b.Title,
                           AuthorName = b.Author.Name
                       };

            return books;
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ObjectResult> GetById(int id)
        {
            var book = await _dbContext.Books.Select(b =>
                    new BookDTO()
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Year = b.Year,
                        Price = b.Price,
                        Genre = b.Genre,
                        AuthorId = b.AuthorId
                    }).SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound(id);
            }

            return Ok(book);
        }

        // GET: api/Books/5/details
        [HttpGet("{id}/details")]
        public async Task<ObjectResult> GetDetailsById(int id)
        {
            var book = await _dbContext.Books.Include(b => b.Author).Select(b =>
                    new BookDetailDTO()
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Year = b.Year,
                        Price = b.Price,
                        Genre = b.Genre,
                        AuthorName = b.Author.Name
                    }).SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound(id);
            }

            return Ok(book);
        }

        // POST: api/Books
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> Create([FromBody] Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            // Load author name
            // Not Yet Implemented in Core 1.0 RC2
            //_dbContext.Entry(book).Reference(x => x.Author).Load();

            return CreatedAtRoute("GetBook", new { Controller = "Books", id = book.Id }, book);
        }

        // PUT: api/Books/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ObjectResult> Update(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest(id + ":" + book);
            }

            _dbContext.Entry(book).State = EntityState.Modified;

            int result;

            try
            {
                result = await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound(id);
                }
                else
                {
                    throw;
                }
            }

            return new ObjectResult(result);
        }

        // DELETE: api/Books/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Book book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return _dbContext.Books.Count(b => b.Id == id) > 0;
        }
    }
}