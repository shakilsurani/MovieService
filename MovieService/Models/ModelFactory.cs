using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;

namespace MovieService.Models
{
    public class ModelFactory
    {
        public MovieModel Create(MovieData entity)
        {
            if (entity == null) return null;
            return new MovieModel
            {
                Title = entity.Title,
                ReleaseDate = entity.ReleaseDate,
                Rating = entity.Rating,
                MovieId = entity.MovieId,
                Genre = entity.Genre,
                Classification = entity.Classification,
                Cast = entity.Cast.ToList()
            };
        }

        public MovieData Parse(MovieModel model)
        {
            return new MovieData
            {
                Title = model.Title,
                ReleaseDate = model.ReleaseDate,
                Rating = model.Rating,
                MovieId = model.MovieId,
                Genre = model.Genre,
                Classification = model.Classification,
                Cast = model.Cast.ToArray()
            };
        }
    }
}