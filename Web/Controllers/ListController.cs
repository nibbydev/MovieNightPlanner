using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class ListController : Controller {
        private readonly MlContext _ctx = new MlContext();
        
        [HttpGet]
        public IActionResult Index() {
            if (!IsAuthenticated()) {
                return Redirect(Url.Content("~/"));
            }
            
            var model = new ListViewModel();
            model.GetSubmissions(_ctx);

            return View(model);
        }
        
        
        [HttpPost]
        public IActionResult Vote(ListViewModel model) {
            if (!IsAuthenticated()) {
                return Redirect(Url.Content("~/"));
            }
            
            model.AddVoteToDb(_ctx, GetUsername());
            return RedirectToAction("Index");
        }

        
        
        [HttpPost]
        public IActionResult Add(ListNewViewModel model) {
            if (!IsAuthenticated()) {
                return Redirect(Url.Content("~/"));
            }
            
            // Check if input is ok
            if (!model.Verify(out var msg)) {
                return BadRequest(msg);
            }
            
            // Add to database
            if (!model.AddToDb(_ctx, HttpContext.Session.GetString("username"), out msg)) {
                return BadRequest(msg);
            }

            return Ok(msg);
        }
        
        [HttpGet]
        public IActionResult New() {
            return View();
        }
        
        public bool IsAuthenticated() {
            // I guess normally people would use User.Identity.IsAuthenticated but I
            // didn't really have time to read about and set up the authentication service
            return GetUsername() != null;
        }

        public string GetUsername() {
            return HttpContext.Session.GetString("username");
        }
    }
}