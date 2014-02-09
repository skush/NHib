using System;
using ACME.Model;
namespace ACME.Repository
{
    public interface IProductRepository : IRepository<Product, Int32?>
    {
        void SaveUOM(UnitOfMeasure saveObj);
    }
}
