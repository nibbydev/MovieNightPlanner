using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;
using MovieNight.Models.Admin;

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

        [HttpGet]
        public IActionResult Users() {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            var model = new AdminUsersViewModel();
            model.GetUsers(_ctx);

            return View(model);
        }

        [HttpGet]
        public IActionResult Submissions() {
            if (!IsAdmin()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            var model = new AdminSubmissionsViewModel();
            model.GetSubmissions(_ctx);

            return View(model);
        }

        [HttpPost]
        public IActionResult Submissions(AdminSubmissionsViewModel model) {
            if (!IsAdmin()) {
                return BadRequest("Not authenticated");
            }

            if (!model.DoAction(_ctx, out var msg)) {
                return BadRequest(msg);
            }

            return Ok(msg);
        }
        
        [HttpPost]
        public IActionResult Users(AdminUsersViewModel model) {
            if (!IsAdmin()) {
                return BadRequest("Not authenticated");
            }

            if (!model.DoAction(_ctx, out var msg)) {
                return BadRequest(msg);
            }

            return Ok(msg);
        }

        public bool IsAuthenticated() {
            return HttpContext.Session.GetString("username") != null;
        }

        public bool IsAdmin() {
            return IsAuthenticated() && HttpContext.Session.GetInt32("is_admin") == 1;
        }
    }
}