using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Driver;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Hql.Ast.ANTLR.Tree;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Database=NHibDemo;Integrated Security=true;";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2012Dialect>();
                x.LogSqlInConsole = true; // writes the SQL to the Console  // use NHibernateProfiler instead
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            // re-create DB
            var schema = new SchemaExport(cfg);
            schema.Create(true, true);

            var sessionFactory = cfg.BuildSessionFactory();
            using(var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction()) {
                ////// criteria
                ////var customers = session.CreateCriteria<Customer>().List<Customer>();
                //LINQ
                /*var customers = from customer in session.Query<Customer>()
                                where customer.LastName.StartsWith("W")   //.Length > 5
                                select customer; 
                foreach (var customer in customers) {
                    Console.WriteLine("{0}{1}", customer.FirstName, customer.LastName);
                 }*/
                var customer = new Customer {FirstName = "James", LastName = "Kovacs"};
                session.Save(customer);
                Console.WriteLine("Niew Id is {0}", customer.Id);
                tx.Commit();
            }
            using (var session = sessionFactory.OpenSession())  
            using (var tx = session.BeginTransaction())
            {
                var query = from customer in session.Query<Customer>()
                    where customer.LastName == "Kovacs"
                    select customer;
                var retrieved = query.Single();
                Console.WriteLine("{0} {1} ({2})", retrieved.FirstName, retrieved.LastName, retrieved.Id);
                tx.Commit();
            }
            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}
