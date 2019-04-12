using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;

namespace MovieNight.Controllers {
    public class ListController : Controller {
        private readonly MlContext _ctx = new MlContext();
        
        [HttpGet]
        public IActionResult Index() {
            var model = new ListViewModel();
            model.GetSubmissions(_ctx, GetUsername());

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Vote(ListViewModel model) {
            if (!IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            model.AddVoteToDb(_ctx, GetUsername());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(ListNewViewModel model) {
            if (!IsAuthenticated()) {
                return BadRequest();
            }
            
            // Check if input is ok
            if (!model.Verify(out var msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            // Add to database
            if (!model.AddToDb(_ctx, HttpContext.Session.GetString("username"), out msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }

            return RedirectToAction("Status", "Home", new StatusViewModel {Message = msg});
        }
        
        [HttpGet]
        public IActionResult New() {
            if (!IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
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