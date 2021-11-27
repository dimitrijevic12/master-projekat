using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Interface.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T GetById(Guid id);

        T Save(T obj);

        T Edit(T obj);

        void Delete(T obj);
    }
}