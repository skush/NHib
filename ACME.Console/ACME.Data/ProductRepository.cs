using System;
using ACME.Model;
using ACME.Repository;
using NHibernate;

namespace ACME.Data
{
    public class ProductRepository : IRepository<Product, Int32?>
    {
        private static ISession GetSession()
        {
            return SessionProvider.SessionFactory.OpenSession();
        }

        public Product GetById(Int32? id)
        {
            using (var session = GetSession())
            {
                return session.Get<Product>(id);
            }
        }

        public void Save(Product saveObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.SaveOrUpdate(saveObj);
                    trans.Commit();
                }
            }
        }

        public void Delete(Product delObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.Delete(delObj);
                    trans.Commit();
                }
            }
        }
    }
}