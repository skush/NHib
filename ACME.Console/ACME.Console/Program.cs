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

        private static void SampleMethod(IProductRepository productRepo)
        {
            WaitForInput("to rebuild schema Press any key...");
            SessionProvider.RebuildSchema();
            WaitForInput("schema rebuilt.");

            //Add some units of measure
            var uomCan = new UnitOfMeasure { UomDescription = "Can" };
            var uomBottle = new UnitOfMeasure { UomDescription = "Bottle" };
            productRepo.SaveUOM(uomCan);
            productRepo.SaveUOM(uomBottle);
            WaitForInput("CREATED Unites of mesure");

            //Create a Product
            var pNew = new Product { ProductName = "Bottled Salmon", UOM = uomBottle };
            productRepo.Save(pNew);
            WaitForInput("CREATED : 'Salmon'");

            //Get a Product
            var pGet = productRepo.GetById(pNew.ProductId);

            //Update a Product
            pGet.ProductName = "Canned Tuna";
            pGet.UOM = uomCan;
            productRepo.Save(pGet);
            WaitForInput("UPDATED to : 'Tuna'");

            //Delete a Product
            productRepo.Delete(pNew);
            WaitForInput("End. Press any key...");
        }
    }
}