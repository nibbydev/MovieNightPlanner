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
        public IActionResult Add(ListViewModel model) {
            // Check if input is ok
            if (model.VerifySubmission(out var msg)) {
                // Add to database
                model.AddSubmission(_ctx);
                
                return Ok(msg);
            }

            return BadRequest(msg);
        }
    }
}