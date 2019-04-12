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
                return Redirect(Url.Content("~/"));
            }
            
            return View();
        }
        
        [HttpPost]
        public IActionResult ResetVotes(AdminViewModel model) {
            if (!IsAdmin()) {
                return BadRequest();
            }
            
            if (!model.ResetVotes(_ctx, out var msg)) {
                return BadRequest(msg);
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult DeleteEntry(AdminViewModel model) {
            if (!IsAdmin()) {
                return BadRequest();
            }

            if (!model.DeleteSubmission(_ctx, out var msg)) {
                return BadRequest(msg);
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult DeleteUser(AdminViewModel model) {
            if (!IsAdmin()) {
                return BadRequest();
            }

            if (!model.DeleteUser(_ctx, out var msg)) {
                return BadRequest(msg);
            }

            return RedirectToAction("Index");
        }

        public bool IsAuthenticated() {
            return HttpContext.Session.GetString("username") != null;
        }
        
        public bool IsAdmin() {
            return IsAuthenticated() && HttpContext.Session.GetInt32("is_admin") == 1;
        }
    }
}