using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesLibrary;

namespace MovieService.Repository
{
    public class MovieRepository : IRepository<MovieData>
    {
        MovieDataSource movieDataSource;

        public MovieRepository()
        {
            movieDataSource = new MovieDataSource();
        }

        public int Add(MovieData entity)
        {
            return movieDataSource.Create(entity);
        }

        public void Update(MovieData entity)
        {
            movieDataSource.Update(entity);
        }

        public MovieData FindById(int Id)
        {
            return movieDataSource.GetDataById(Id);
        }

        public IEnumerable<MovieData> FindAll()
        {
            return movieDataSource.GetAllData();
        }
        
        public IEnumerable<MovieData> Sort(string sortField)
        {
            var propInfo = typeof(MovieData).GetProperty(sortField);
            //
            if (propInfo == null)
                return null;
            else
                return movieDataSource.GetAllData().OrderBy(x => propInfo.GetValue(x, null)).ToList();
        }

        public IEnumerable<MovieData> Search(string searchTerm)
        {
            var result = new List<MovieData>();
            var stringProperties = typeof(MovieData).GetProperties();
            //
            foreach (var itemMovie in movieDataSource.GetAllData().ToList())
            {
                foreach (var item in stringProperties)
                {
                    string[] arrayProperty = (item.PropertyType.IsArray ? (string[])item.GetValue(itemMovie, null) : null);
                    if (arrayProperty != null && arrayProperty.Count() > 0 && arrayProperty.Any(x => x.ToLower().Contains(searchTerm.ToLower())) || (item.GetValue(itemMovie).ToString().ToLower().Contains(searchTerm.ToLower())))
                    {
                        result.Add(itemMovie);
                        break;
                    }
                }
            }
            //
            if (result.Count > 0)
                return result;
            else
                return null;
        }
    }
}