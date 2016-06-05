using BookService.Controllers;
using BookService.Data;
using BookService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace BookService.Tests
{
    public class BooksControllerTest
    {
        [Fact]
        public void GetAll()
        {
            var services = new Setup().Services;

            using (var context = new ApplicationDbContext(
                services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var controller = new BooksController(context);

                var result = controller.GetAll() as IQueryable<BookListDTO>;

                Assert.True(result.Count()>=4);
            }
        }
    }
}
