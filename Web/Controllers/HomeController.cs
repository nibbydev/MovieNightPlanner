using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;

namespace MovieNight.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Status(StatusViewModel model) {
            return View(model);
        }
    }
}