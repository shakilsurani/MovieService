using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Repository
{
    public interface IRepository<T>
    {
        int Add(T entity);
        void Update(T entity);
        T FindById(int Id);
        IEnumerable<T> FindAll();
    }
}
