using static System.Console;
using WorkingWithEFCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WorkingWithEFCore
{
    public class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            Console.WriteLine("Hello, World!");
            QueryingCategories();
        }

        static void QueryingCategories()
        {
            using (var db = new Northwind())
            {
                WriteLine("Categories and how many products they have");
                IQueryable<Category> categories = db.Categories.Include(c => c.Products);

                foreach (Category c in categories)
                {
                    WriteLine($"{c.CategoryName} has ${c.Products.Count} products");
                }
            }
        }
    }
}