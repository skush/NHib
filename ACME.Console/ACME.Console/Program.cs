using System;
using ACME.Data;
using ACME.Model;
using ACME.Repository;

namespace ACME.Console
{
    class Program
    {
        static void Main()
        {
            SampleMethod(new ProductRepository());
        }

        private static void WaitForInput(string prompt)
        {
            System.Console.WriteLine(prompt);
            System.Console.ReadKey();
        }

        private static void SampleMethod(IRepository<Product, Int32?> productRepo)
        {
            WaitForInput("to rebuild schema Press any key...");

            SessionProvider.RebuildSchema();

            WaitForInput("schema rebuilt.");

            //Create a Product
            var pNew = new Product { ProductName = "Canned Salmon" };
            productRepo.Save(pNew);

            WaitForInput("CREATED : 'Canned Salmon'");

            //Get a Product
            var pGet = productRepo.GetById(pNew.ProductId);

            //Update a Product
            pGet.ProductName = "Canned Tuna";
            productRepo.Save(pGet);

            WaitForInput("UPDATED to : 'Canned Tuna'");

            //Delete a Product
            productRepo.Delete(pNew);

            WaitForInput("End. Press any key...");
        }
    }
}