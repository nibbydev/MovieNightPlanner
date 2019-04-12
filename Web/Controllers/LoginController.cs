using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class LoginController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        [HttpGet]
        public IActionResult Index(LoginViewModel model = null) {
            if (IsAuthenticated()) {
                return Redirect(Url.Content("~/"));
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult Logout() {
            if (!IsAuthenticated()) {
                return BadRequest();
            }
            
            HttpContext.Session.Clear();
            return Redirect(Url.Content("~/"));
        }
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (IsAuthenticated()) {
                return BadRequest();
            }
            
            if (!model.VerifyLogin(_ctx, out var msg)) {
                return BadRequest(msg);
            }
            
            SetSession(model);
            return Redirect(Url.Content("~/"));
        }
        
        [HttpPost]
        public IActionResult Register(LoginViewModel model) {
            if (IsAuthenticated()) {
                return BadRequest();
            }
            
            if (!model.CreateAccount(_ctx, out var msg)) {
                return BadRequest(msg);
            }
            
            SetSession(model);
            return Redirect(Url.Content("~/"));
        }

        private void SetSession(LoginViewModel model) {
            HttpContext.Session.SetString("username", model.Username);
        }

        public bool IsAuthenticated() {
            // I guess normally people would use User.Identity.IsAuthenticated but I
            // didn't really have time to read about and set up the authentication service
            return HttpContext.Session.GetString("username") != null;
        }
    }
}