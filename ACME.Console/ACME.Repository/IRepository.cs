using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Repository
{
    public interface IRepository<TData, TKey>
        where TData : class
    {
        TData GetById(TKey id);
        void Save(TData saveObj);
        void Delete(TData delObj);
    }
}
