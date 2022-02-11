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
            // QueryingCategories();
            // FilteredIncludes();
            // QueryingProducts();
            QueryingWithLike();
        }

        static bool AddProduct(int categoryID,
            string productName, decimal? price)
        {
            using (var db = new Northwind())
            {
                var newProduct = new Product
                {
                    CategoryID = categoryID,
                    ProductName = productName,
                    Cost = price
                };

                db.Products.Add(newProduct);
                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                Write("Enter part of a product name: ");
                string? input = ReadLine();

                IQueryable<Product> products = db.Products
                    .Where(product => EF.Functions.Like(product.ProductName, $"%{input}%"));

                foreach (Product product in products)
                {
                    WriteLine("{0} has {1} units in stock. discontinued? {2}",
                        product.ProductName, product.Stock, product.Discontinued);
                }
            }
        }

        static void FilteredIncludes()
        {
            using (var db = new Northwind())
            {
                Write("Enter a minimum for units in stock:");
                string? unitsInStock = ReadLine();
                int stock = int.Parse(unitsInStock);

                IQueryable<Category> categories = db.Categories
                    .Include(c => c.Products.Where(p => p.Stock >= stock));

                WriteLine($"ToQueryString : {categories.ToQueryString()}");

                foreach (Category category in categories)
                {
                    WriteLine(
                        $"{category.CategoryName} has {category.Products.Count} products with a minimum stock " +
                        $"of {stock} units in stock");
                    foreach (Product product in category.Products)
                    {
                        WriteLine($"        {product.ProductName} has {product.Stock} units in stock.");
                    }
                }
            }
        }

        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                WriteLine("Products that cost more than a price, highest at top.");
                string? input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                } while (!decimal.TryParse(input, out price));

                IQueryable<Product> products = db.Products
                    .Where(product => product.Cost > price)
                    .OrderByDescending(product => product.Cost);

                foreach (Product product in products)
                {
                    WriteLine(
                        "{0}: {1} costs {2:$#,##0.00} has {3} in stock",
                        product.ProductID, product.ProductName, product.Cost, product.Stock);
                }
            }
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