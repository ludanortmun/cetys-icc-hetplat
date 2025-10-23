using Cookies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cookies.Controllers
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
            var preference = GetFromCookies();
            return View(preference);
        }

        public IActionResult Preferences()
        {
            var preference = GetFromCookies();
            return View(preference);
        }

        [HttpPost]
        public IActionResult Preferences(UserPreference preference)
        {
            if (ModelState.IsValid)
            {
                // Store the user preference in a cookie
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    IsEssential = true,
                    HttpOnly = true
                };
                Response.Cookies.Append("User.DisplayName", preference.DisplayName ?? string.Empty, cookieOptions);
                Response.Cookies.Append("User.PreferredDateFormat", preference.PreferredDateFormat.ToString(), cookieOptions);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
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

        private UserPreference GetFromCookies()
        {
            var preference = new UserPreference();
            if (Request.Cookies.TryGetValue("User.DisplayName", out var displayName))
            {
                preference.DisplayName = displayName;
            }
            if (Request.Cookies.TryGetValue("User.PreferredDateFormat", out var dateFormatString) &&
                Enum.TryParse<DateFormat>(dateFormatString, out var dateFormat))
            {
                preference.PreferredDateFormat = dateFormat;
            }
            return preference;
        }
    }
}
