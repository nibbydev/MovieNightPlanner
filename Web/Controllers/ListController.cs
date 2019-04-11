using DAL;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class ListController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        [HttpGet]
        public IActionResult Index() {
            return View(new ListViewModel(_ctx));
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