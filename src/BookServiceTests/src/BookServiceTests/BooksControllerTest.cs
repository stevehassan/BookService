using BookService.Controllers;
using BookService.Data;
using BookService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace BookService.Tests
{
    public class BooksControllerTest
    {
        IServiceProvider services = new Setup().Services;

        [Fact]
        public void GetAll()
        {
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
