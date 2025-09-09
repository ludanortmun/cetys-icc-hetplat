using Humanizer;
using Microsoft.AspNetCore.Mvc;
using NumericSeries.Models;

namespace NumericSeries.Controllers
{
    public class SeriesController : Controller
    {
        [HttpGet("/series/{series}/{n:min(0)}")]
        public IActionResult Index(string series, int n = 0)
        {
            return View(new SeriesViewModel()
            {
                Series = series.ApplyCase(LetterCasing.Sentence),
                N = n,
                Result = n
            });
        }
    }
}
