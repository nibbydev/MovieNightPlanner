using System;
using System.Linq;
using DAL;
using Microsoft.AspNetCore.Mvc;
using NetGroupCV.Models;

namespace NetGroupCV.Controllers {
    public class ListController : Controller {
        private readonly DbContext _ctx = new DbContext();

        // GET
        public IActionResult Index() {
            // Get movies
            var movies = _ctx.Movies.ToList();
            // Get user who added that movie
            movies.ForEach(t => t.User = _ctx.Users.First(u => u.Id == t.UserId));
            // Get tags
            movies.ForEach(t => t.Tags = _ctx.Tags.Where(u => u.MovieId == t.Id).ToList());
            
            // Open view
            return View(movies);
        }
    }
}