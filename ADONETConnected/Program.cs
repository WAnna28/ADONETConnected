using ADONETConnected.DataAccess;
using ADONETConnected.Models;
using System;
using System.Collections.Generic;

namespace ADONETConnected
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductDAL productDAL = new ProductDAL();

            // InsertProduct
            for (int i = 1; i < 10; i++)
            {
                productDAL.InsertProduct(
                    new Product()
                    { ProductId = i, ProductName = "demo" + i, IntroductionDate = DateTime.Now, Price = i * 10, Url = $"https://demo/{i}" }
                );

                Console.WriteLine($"ResultText for product {i}: {productDAL.ResultText}");
            }

            // GetProductsCountScalar
            Console.WriteLine($"\n\nProducts count is: {productDAL.GetProductsCountScalar()}");

            // GetProductsAsGenericList
            List<Product> products = productDAL.GetProductsAsGenericList();

            // 'for' loop used here for demonstrate how use index in the list structure
            Console.WriteLine("\n\nProductId ProductName IntroductionDate\t\tUrl\tPrice");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"  {products[i].ProductId}\t\t{products[i].ProductName}\t{products[i].IntroductionDate.ToShortDateString()}\t{products[i].Url}\t{products[i].Price}");
            }

            Console.ReadLine();
        }
    }
}