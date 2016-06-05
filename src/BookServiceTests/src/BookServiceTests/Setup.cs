using BookService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace BookService.Tests
{
    public class Setup
    {
        public IHostingEnvironment HostingEnvironment { get; set; }

        public IServiceProvider Services { get; set; }

        public IConfigurationRoot Configuration { get; }

        public Setup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services = serviceCollection.BuildServiceProvider();

            SeedData.Initialize(Services);
        }
    }

}

