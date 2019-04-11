using DAL;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class LoginController : Controller {
        private readonly DbContext _ctx = new DbContext();
        
        [HttpGet]
        public IActionResult Index() {
            return View(new LoginViewModel());
        }
        
        [HttpPost]
        public IActionResult Login(LoginViewModel model) {
            if (model.VerifyLogin(_ctx, out var msg)) {
                return Ok(msg);
            } 
            
            return BadRequest(msg);
        }
        
        [HttpPost]
        public IActionResult Register(LoginViewModel model) {
            if (model.CreateAccount(_ctx, out var msg)) {
                return Ok(msg);
            } 
            
            return BadRequest(msg);
        }
    }
}