using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;

namespace MovieNight.Controllers {
    public class AdminController : Controller {
        private readonly MlContext _ctx = new MlContext();
        
        [HttpGet]
        public IActionResult Index() {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            return View();
        }
        
        [HttpPost]
        public IActionResult ResetVotes(AdminViewModel model) {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            if (!model.ResetVotes(_ctx, out var msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            return RedirectToAction("Status", "Home", new StatusViewModel {Message = msg});
        }
        
        [HttpPost]
        public IActionResult DeleteEntry(AdminViewModel model) {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            if (!model.DeleteSubmission(_ctx, out var msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }

            return RedirectToAction("Status", "Home", new StatusViewModel {Message = msg});
        }
        
        [HttpPost]
        public IActionResult DeleteUser(AdminViewModel model) {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            if (!model.DeleteUser(_ctx, out var msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }

            return RedirectToAction("Status", "Home", new StatusViewModel {Message = msg});
        }
        
        public bool IsAuthenticated() {
            return HttpContext.Session.GetString("username") != null;
        }
        
        public bool IsAdmin() {
            return IsAuthenticated() && HttpContext.Session.GetInt32("is_admin") == 1;
        }
    }
}