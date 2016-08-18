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
using MovieService.Repository;

namespace MovieService.Tests.Controllers
{
    [TestClass]
    public class MoviesRepositoryTest
    {
        private readonly MovieRepository _repository;

        public MoviesRepositoryTest()
        {
            _repository = new MovieRepository();
        }

        [TestMethod]
        public void MovieRepository_FindAllMovies()
        {
            // Act
            var result = _repository.FindAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void MovieRepository_FindAllMoviesById()
        {
            // Act
            var result = _repository.FindById(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.MovieId == 5);
        }

        [TestMethod]
        public void MovieRepository_FindAllMoviesByInvalidId()
        {
            // Act
            var result = _repository.FindById(-5);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MovieRepository_SearchMovies()
        {
            // Act
            var result = _repository.Search("action");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void MovieRepository_SearchMoviesInvalid()
        {
            // Act
            var result = _repository.Search("-notfound-");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MovieRepository_SortMovies()
        {
            // Act
            var result = _repository.Sort("ReleaseDate");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void MovieRepository_SortMoviesInvalid()
        {
            // Act
            var result = _repository.Sort("-notfound-");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void MovieRepository_AddMovie()
        {
            // Act
            string[] cast = { "Test1", "Test2" };
            var result = _repository.Add(new MovieData { Title = "Test", Classification = "Test", Genre = "Test", Rating = 5, ReleaseDate = 2016, Cast = cast });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }
    }
}
