using DAL;
using DAL.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;

namespace MovieNight.Controllers {
    public class LoginController : Controller {
        private readonly MlContext _ctx = new MlContext();
        
        [HttpGet]
        public IActionResult Index(LoginViewModel model = null) {
            if (IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Logout() {
            if (!IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            HttpContext.Session.Clear();
            var statusModel2 = new StatusViewModel {IsError = false, Message = "Successfully logged out"};
            return RedirectToAction("Status", "Home", statusModel2);
        }
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Already logged in"};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            if (!model.Login(_ctx, out var user, out var msg)) {
                var statusModel = new StatusViewModel {IsError = true, Message = msg};
                return RedirectToAction("Status", "Home", statusModel);
            }
            
            SetSession(user);
            var statusModel2 = new StatusViewModel {IsError = false, Message = msg};
            return RedirectToAction("Status", "Home", statusModel2);
        }

        private void SetSession(User user) {
            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetInt32("is_admin", user.IsAdmin ? 1 : 0);
        }

        public bool IsAuthenticated() {
            // I guess normally people would use User.Identity.IsAuthenticated but I
            // didn't really have time to read about and set up the authentication service
            return HttpContext.Session.GetString("username") != null;
        }
    }
}