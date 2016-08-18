using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieService;
using MovieService.Controllers;
using System.Web.Http.Results;
using MoviesLibrary;
using System.Web.Http.Controllers;
using MovieService.Models;
using MovieService.Repository;

namespace MovieService.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTest
    {
        private readonly MoviesController _controller;

        public MoviesControllerTest()
        {
            _controller = new MoviesController(new MovieRepository());
        }

        [TestMethod]
        public void MoviesController_GetShouldReturnAllMovies()
        {
            // Act
            var actionResult = _controller.Get();
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<MovieModel>>;

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Count() > 0);
        }

        [TestMethod]
        public void MoviesController_GetByValidId()
        {
            // Act
            var actionResult = _controller.Get(5);
            var response = actionResult as OkNegotiatedContentResult<MovieModel>;

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.MovieId == 5);
        }

        [TestMethod]
        public void MoviesController_GetByInvalidId()
        {
            // Act
            var actionResult = _controller.Get(-1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void MoviesController_GetSearchMoviesValid()
        {
            // Act
            var actionResult = _controller.SearchMovies("action");
            var response = actionResult as OkNegotiatedContentResult<List<MovieModel>>;

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Count() > 0);
        }

        [TestMethod]
        public void MoviesController_GetSearchMoviesInvalid()
        {
            // Act
            var actionResult = _controller.SearchMovies("-notfound-");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void MoviesController_GetSortMoviesValid()
        {
            // Act
            var actionResult = _controller.SortMovies("ReleaseDate");
            var response = actionResult as OkNegotiatedContentResult<List<MovieModel>>;

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content.Count() > 0);
        }

        [TestMethod]
        public void MoviesController_GetSortMoviesInvalid()
        {
            // Act
            var actionResult = _controller.SortMovies("-notfound-");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void MoviesController_GetSortMoviesCastFieldInvalid()
        {
            // Act
            var actionResult = _controller.SortMovies("Cast");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void MoviesController_PostValidMovie()
        {
            // Act
            string[] cast = { "Test1", "Test2" };
            var actionResult = _controller.Post(new MovieModel { Title = "Test", Classification = "Test", Genre = "Test", Rating = 5, ReleaseDate = 2016, Cast = cast });
            var response = actionResult as OkNegotiatedContentResult<int>;

            // Assert
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.Content > 0);
        }

        [TestMethod]
        public void MoviesController_PostInvalidMovie()
        {
            // Act
            string[] cast = { "Test1", "Test2" };
            var actionResult = _controller.Post(new MovieModel { MovieId = 1, Title = "Test", Classification = "Test", Genre = "Test", Rating = 5, ReleaseDate = 2016, Cast = cast });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void MoviesController_PutValidMovie()
        {
            // Act
            string[] cast = { "Test1", "Test2" };
            var actionResult = _controller.Put(1, new MovieModel { MovieId = 1, Title = "Test", Classification = "Test", Genre = "Test", Rating = 5, ReleaseDate = 2016, Cast = cast });
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void MoviesController_PutInvalidMovie()
        {
            // Act
            string[] cast = { "Test1", "Test2" };
            var actionResult = _controller.Put(3, new MovieModel { MovieId = 1, Title = "Test", Classification = "Test", Genre = "Test", Rating = 5, ReleaseDate = 2016, Cast = cast });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }
    }
}
