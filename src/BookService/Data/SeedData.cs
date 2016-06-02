using BookService.Data;
using BookService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookService.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Authors.Any())
                {
                    var authors = new List<Author>
                    {
                        new Author() { Name = "Jane Austen", BirthPlace = "Steventon, Hampshire, England" },
                        new Author() { Name = "Charles Dickens", BirthPlace = "Gad's Hill, Rochester, Kent, England" },
                        new Author() { Name = "Miguel de Cervantes", BirthPlace = " Alcalá de Henares, Madrid, Spain" },
                        new Author() { Name = "Isaac Asimov", BirthPlace = " Portsmouth, Hampshire, England" }
                    };

                    context.Authors.AddRange(authors);
                }

                if (!context.Books.Any())
                {

                    var books = new List<Book>
                    {
                        new Book()
                        {
                            Title = "Pride and Prejudice",
                            Year = 1813,
                            AuthorId = 1,
                            Price = 9.99M,
                            Genre = "Commedy of manners"
                        },
                        new Book()
                        {
                            Title = "Northanger Abbey",
                            Year = 1817,
                            AuthorId = 1,
                            Price = 12.95M,
                            Genre = "Gothic parody"
                        },
                        new Book()
                        {
                            Title = "David Copperfield",
                            Year = 1850,
                            AuthorId = 2,
                            Price = 15,
                            Genre = "Bildungsroman"
                        },
                        new Book()
                        {
                            Title = "Don Quixote",
                            Year = 1617,
                            AuthorId = 3,
                            Price = 8.95M,
                            Genre = "Picaresque"
                        }
                    };

                    context.Books.AddRange(books);
                }

                if (!context.Authors.Any() || !context.Books.Any())
                {
                    context.SaveChanges();
                }
            }
        }
    }
}