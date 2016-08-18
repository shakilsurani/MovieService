using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieService.Models
{
    public class MovieModel
    {
        public IEnumerable<string> Cast { get; set; }
        public string Classification { get; set; }
        public string Genre { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
        public int ReleaseDate { get; set; }
        public string Title { get; set; }
    }
}