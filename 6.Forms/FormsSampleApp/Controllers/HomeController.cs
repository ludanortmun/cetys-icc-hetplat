using System.Diagnostics;
using FormsSampleApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormsSampleApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(GreetingViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.From = model.From;
                ViewBag.To = model.To;
                ViewBag.Message = model.Message;
                return View(model);
            }
            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
