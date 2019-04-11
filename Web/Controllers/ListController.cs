using DAL;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class ListController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        [HttpGet]
        public IActionResult Index() {
            var model = new ListViewModel();
            model.GetSubmissions(_ctx);

            return View(model);
        }
        
        
        [HttpPost]
        public IActionResult Vote(ListViewModel model) {
            model.AddVoteToDb(_ctx);
            return RedirectToAction("Index");
        }

        
        
        [HttpPost]
        public IActionResult Add(ListNewViewModel model) {
            // Check if input is ok
            if (!model.Verify(out var msg)) {
                return BadRequest(msg);
            }
            
            // Add to database
            if (!model.AddToDb(_ctx)) {
                return BadRequest("Could not add to database");
            }

            return Ok(msg);
        }
        
        [HttpGet]
        public IActionResult New() {
            return View();
        }
    }
}