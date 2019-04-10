using DAL;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class ListController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        public IActionResult Index() {
            return View(new ListViewModel(_ctx));
        }
        
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Add(ListViewModel model) {
            model.Add();
            
            return RedirectToAction("Index");
        }
    }
}