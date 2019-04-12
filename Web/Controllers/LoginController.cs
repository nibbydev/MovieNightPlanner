using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class LoginController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        [HttpGet]
        public IActionResult Index(LoginViewModel model = null) {
            // User is already signed in, redirect to front page
            if (HttpContext.Session.GetString("username") != null) {
                return Redirect(Url.Content("~/"));
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return Redirect(Url.Content("~/"));
        }
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (model.VerifyLogin(_ctx, out var msg)) {
                SetSession(model);
            }

            return BadRequest(msg);
        }
        
        [HttpPost]
        public IActionResult Register(LoginViewModel model) {
            if (model.CreateAccount(_ctx, out var msg)) {
                SetSession(model);
            }

            return BadRequest(msg);
        }

        private void SetSession(LoginViewModel model) {
            HttpContext.Session.SetString("username", model.Username);
        }
    }
}