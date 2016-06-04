using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookService.Controllers
{
    public class ApiController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["Title"] = "CRUD API";

            return View();
        }

        public IActionResult BookAdd()
        {
            return PartialView();
        }

        public IActionResult BookEdit()
        {
            return PartialView();
        }

        public IActionResult BookList()
        {
            return PartialView();
        }

        public IActionResult BookView()
        {
            return PartialView();
        }

        public IActionResult BookForm()
        {
            return PartialView();
        }

    }
}
