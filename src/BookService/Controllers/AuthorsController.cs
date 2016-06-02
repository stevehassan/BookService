using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookService.Data;
using Microsoft.EntityFrameworkCore;
using BookService.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public AuthorsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Authors
        [HttpGet]
        public IQueryable<Author> GetAll()
        {
            return _dbContext.Authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ObjectResult> GetById(int id)
        {
            Author author = await _dbContext.Authors.SingleOrDefaultAsync(e => e.Id == id);

            if (author == null)
            {
                return NotFound(id);
            }

            return Ok(author);
        }

        // POST: api/Authors
        [Authorize]
        [HttpPost]
        public async Task<ObjectResult> Create([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetAuthor", new { Controller = "Authors", id = author.Id }, author);
        }

        // PUT: api/Authors/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ObjectResult> Update(int id, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest(id);
            }

            _dbContext.Entry(author).State = EntityState.Modified;

           int result;

            try
            {
                result = await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // DELETE: api/Authors/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Author author = await _dbContext.Authors.SingleOrDefaultAsync(e => e.Id == id);
            if (author == null)
            {
                return NotFound(id);
            }

            _dbContext.Authors.Remove(author);
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

        private bool AuthorExists(int id)
        {
            return _dbContext.Authors.Count(e => e.Id == id) > 0;
        }
    }
}