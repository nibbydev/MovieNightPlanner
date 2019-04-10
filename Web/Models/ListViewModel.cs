using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Domain;

namespace NetGroupCV.Models {
    public class ListViewModel {
        public List<Movie> Movies { get; set; }
        
        
        public string SourceUrl { get; set; }
        public string ImageUrl { get; set; }
        public string TagString { get; set; }
        public string UserName { get; set; }
        

        public ListViewModel(DbContext ctx) {
            Movies = GetMovies(ctx);
        }


        public void Add() {
            
        }
        
        private static List<Movie> GetMovies(DbContext ctx) {
            // Get movies
            var movies = ctx.Movies.ToList();
            // Get tags
            movies.ForEach(t => t.Tags = ctx.Tags.Where(u => u.MovieId == t.Id).ToList());

            return movies;
        }
    }
}