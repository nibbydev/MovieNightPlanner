using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieNight.Models;
using MovieNight.Models.List;

namespace MovieNight.Controllers {
    public class ListController : Controller {
        private readonly MlContext _ctx = new MlContext();

        [HttpGet]
        public IActionResult Watched() {
            var model = new ListViewModel();
            model.GetWatchedSubmissions(_ctx, GetUsername());
            return View(model);
        }

        [HttpGet]
        public IActionResult Planned() {
            var model = new ListViewModel();
            model.GetPlannedSubmissions(_ctx, GetUsername());
            return View(model);
        }

        [HttpPost]
        public IActionResult Vote(ListViewModel model) {
            if (!IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            model.AddVoteToDb(_ctx, GetUsername());
            return RedirectToAction("Planned");
        }

        [HttpPost]
        public IActionResult Add(ListNewViewModel model) {
            if (!IsAuthenticated()) {
                return BadRequest("Not authenticated");
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
            if (!IsAuthenticated()) {
                var statusModel = new StatusViewModel {IsError = true, Message = "Not authenticated"};
                return RedirectToAction("Status", "Home", statusModel);
            }

            return View();
        }

        public bool IsAuthenticated() {
            return GetUsername() != null;
        }

        public string GetUsername() {
            return HttpContext.Session.GetString("username");
        }
    }
}