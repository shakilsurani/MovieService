using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesLibrary;
using MovieService.Repository;
using MovieService.Models;

namespace MovieService.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly ModelFactory _modelFactory;
        private readonly IRepository<MovieData> _movieRepository;

        public MoviesController(IRepository<MovieData> movieRepository)
        {
            _modelFactory = new ModelFactory();
            _movieRepository = movieRepository;
        }

        public IHttpActionResult Get()
        {
            try
            {
                var result = _movieRepository.FindAll().Select(m => _modelFactory.Create(m));
                //
                if (result != null && result.Count() > 0)
                    return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = _modelFactory.Create(_movieRepository.FindById(id));
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Movies/SearchMovies/{searchTerm}")]
        public IHttpActionResult SearchMovies(string searchTerm)
        {
            try
            {
                var result = new List<MovieModel>();
                //
                var stringProperties = typeof(MovieModel).GetProperties();
                foreach (var itemMovie in _movieRepository.FindAll().Select(m => _modelFactory.Create(m)).ToList())
                {
                    foreach (var item in stringProperties)
                    {
                        string[] arrayProperty = (item.PropertyType.IsArray ? (string[])item.GetValue(itemMovie, null) : null);
                        if (arrayProperty != null && arrayProperty.Count() > 0 && arrayProperty.Any(x => x.ToLower().Contains(searchTerm.ToLower()))
                            || (item.GetValue(itemMovie).ToString().ToLower().Contains(searchTerm.ToLower())))
                        {
                            result.Add(itemMovie);
                            break;
                        }
                    }
                }
                //
                if (result.Count > 0)
                    return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/Movies/SortMovies/{sortField}")]
        public IHttpActionResult SortMovies(string sortField)
        {
            try
            {
                var result = new List<MovieModel>();
                //
                if (sortField.ToLower().Equals("cast")) return BadRequest("Invalid sort field.");
                var propInfo = typeof(MovieModel).GetProperty(sortField);
                //
                if (propInfo == null)
                    return BadRequest("Invalid sort field.");
                //
                result = _movieRepository.FindAll().Select(m => _modelFactory.Create(m)).OrderBy(x => propInfo.GetValue(x, null)).ToList();
                return Ok(result);
                //
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]MovieModel movie)
        {
            try
            {
                if (movie.MovieId > 0) return BadRequest("Invalid movie");
                //
                return Ok(_movieRepository.Add(_modelFactory.Parse(movie)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [HttpPatch]
        public IHttpActionResult Put(int id, [FromBody]MovieModel movie)
        {
            try
            {
                if (!movie.MovieId.Equals(id)) return BadRequest("Invalid movie");
                _movieRepository.Update(_modelFactory.Parse(movie));
                //
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
               
    }
}
