using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Actors { get; set; }
        public string PosterUrl { get; set; }

    }
    public class MovieData
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public List<Movie> Items { get; set; }
    }

    public class MovieApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public MovieData Data { get; set; }
    }
}
