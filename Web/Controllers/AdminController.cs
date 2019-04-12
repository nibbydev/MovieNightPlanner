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

        [HttpPost]
        public IActionResult Users(AdminUsersViewModel model) {
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
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            if (!model.DoAction(_ctx, out var msg)) {
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