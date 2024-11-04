using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
    }
}
