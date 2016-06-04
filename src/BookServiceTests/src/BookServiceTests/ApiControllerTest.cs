using BookService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookServiceTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ApiControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            ApiController controller = new ApiController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("CRUD API", result.ViewData["Title"]);
        }
    }
}
